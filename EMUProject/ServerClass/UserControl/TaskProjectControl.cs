using EMU.Interface;
using Project.ServerClass.Model;
using System;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class TaskProjectControl : UserControl
    {
        public IMainTask MainTask { get; set; }
        public IProject Project { get; set; }

        public TaskProjectControl()
        {
            InitializeComponent();
        }

        private void btn_insert_Click(object sender, EventArgs e)
        {
            ProjectForm form = new ProjectForm();
            form.ShowDialog(this);
            if (form.ProjectModel != null)
            {
                form.ProjectModel.ProjectDate = DateTime.Now;
                form.ProjectModel.ProjectState = ProjectState.计划中;
                ServerGlobal.ProjectModels.Add(form.ProjectModel);
                ServerGlobal.DataBase.AddT<ProjectModel>(form.ProjectModel);
                AddControl(form.ProjectModel);
                DataCount();
            }
        }

        private void btn_frash_Click(object sender, EventArgs e)
        {
            ServerGlobal.ProjectModels = ServerGlobal.DataBase.GetTs<ProjectModel>(null,
                cb_none.Checked, cb_doing.Checked, cb_complate.Checked, dt_start.Value, dt_end.Value);
            if (ServerGlobal.ProjectModels != null)
            {
                DataCount();
                ShowControl();
            }
        }

        private void StartProjectModel(ProjectModel model)
        {
            ServerGlobal.RobotTaskStart(model.Robot);
#if true
            if (ServerGlobal.StartRobotList.IndexOf(model.Robot) < 0)
            {
                ServerGlobal.StartRobotList.Add(model.Robot); 
            }
            ServerGlobal.StartProjectDict.Add(model.Robot, model);
            ServerGlobal.SelectRobotID = model.Robot;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog(this) == DialogResult.OK)
            {
                string name = folderBrowserDialog.SelectedPath;
                name = name.Substring(name.LastIndexOf("\\") + 1);
                int i = name.LastIndexOf("_");
                string id = name.Substring(0, i);
                string robot = name.Substring(i + 1);
                if (!ServerGlobal.RgvList.ContainsKey(robot))
                {
                    ServerGlobal.RgvList.Add(robot, new SocketRgvInfo()
                    {
                        Job = "模拟分析中", Log = ""
                    });
                }
                AppServer app = Project.appServer as AppServer;
                app.Complete2(id, robot, 0);

                EMU.Util.ThreadManager.TaskRun((EMU.Util.ThreadEventArgs threadEventArgs) =>
                {
                    string[] dirs = System.IO.Directory.GetDirectories(folderBrowserDialog.SelectedPath);
                    if (dirs != null)
                    {
                        while (!ServerGlobal.RgvList[robot].Log.Contains("分析完成"))
                        {
                            System.Threading.Thread.Sleep(5000);
                        }
                        string[] ids = id.Split('_');
                        foreach (string dir in dirs)
                        {
                            string[] files = System.IO.Directory.GetFiles(dir, "*" + EMU.Util.FileManager.GetImageExtend());
                            if (files != null)
                            {
                                EMU.Parameter.RobotName robotName = EMU.Parameter.RobotName.Back;
                                if (dir.Contains("Front"))
                                {
                                    robotName = EMU.Parameter.RobotName.Front;
                                }
                                foreach (string file in files)
                                {
                                    name = file.Substring(file.LastIndexOf("\\") + 1).Replace(EMU.Util.FileManager.GetImageExtend(), "");
                                    app.MzComplete2(robot, ids[0], ids[1], name, name.Split('_')[0], robotName, System.Drawing.Image.FromFile(file));
                                    System.Threading.Thread.Sleep(500);
                                }
                            }
                        }
                        ServerGlobal.RgvList[robot].Job = "上位机等待任务中";
                    }
                });
            }
            else
            {
                ServerGlobal.RobotTaskEnd(model.Robot);
            }
#else
            if (ServerGlobal.StartRobotList.IndexOf(model.Robot) < 0)
            {
                ServerGlobal.StartRobotList.Add(model.Robot); 
            }
            model.ProjectState = ProjectState.检修中;
            model.TestStart = DateTime.Now;
            ServerGlobal.StartProjectDict.Add(model.Robot, model);
            ServerGlobal.SelectRobotID = model.Robot;
            DataCount();
            ServerGlobal.DataBase.UpdT<ProjectModel>(model);
            EMU.Util.ThreadManager.TaskRun(() =>
            {
                try
                {
                    MainTask.RunTask(EMU.Parameter.TaskName.Start, () =>
                    {
                        model.ProjectState = ProjectState.检修完成;
                        model.TestEnd = DateTime.Now;
                    });
                }
                catch (Exception)
                {
                    model.ProjectState = ProjectState.检修异常;
                    model.TestEnd = DateTime.Now;
                }
            });
#endif
        }

        private void ShowProjectResult(ProjectModel model)
        {
            if (!ServerGlobal.ResultDict.ContainsKey(model.ID))
            {
                ServerGlobal.ResultDict.Add(model.ID, ServerGlobal.DataBase.GetTs<ResultData>(null, model));
            }

            DetectionResultInfoForm form = new DetectionResultInfoForm();
            form.detectionResultControl.ProjectModel = model;
            form.detectionResultControl.resultControl.ResultDatas = ServerGlobal.ResultDict[model.ID];
            form.ShowDialog(this);
        }

        private void DeleteProjectModel(ProjectModel model)
        {
            ServerGlobal.ProjectModels.Remove(model);
            ServerGlobal.DataBase.DelT<ProjectModel>(model);
            DataCount();
            ShowControl();
        }

        private void UpdateProjectModel(ProjectModel model)
        {
            ProjectForm form = new ProjectForm();
            form.ProjectModel = model;
            form.ShowDialog(this);
            model.ProjectDate = DateTime.Now;
            model.ProjectState = ProjectState.计划中;
            DataCount();
            ServerGlobal.DataBase.UpdT<ProjectModel>(form.ProjectModel);
        }

        private void AddControl(ProjectModel model)
        {
            ProjectControl control = new ProjectControl();
            control.DeleteProject = DeleteProjectModel;
            control.UpdateProject = UpdateProjectModel;
            control.ShowProjectResult = ShowProjectResult;
            control.StartProject = StartProjectModel;
            flp.Controls.Add(control);
            control.SetModel(model);
        }

        private void DataCount()
        {
            lb_count.Text = ServerGlobal.ProjectModels.Count.ToString();
            lb_none_count.Text = ServerGlobal.ProjectModels.FindAll(p => p.ProjectState == ProjectState.计划中).Count.ToString();
            lb_doing_count.Text = ServerGlobal.ProjectModels.FindAll(p => p.ProjectState == ProjectState.检修中).Count.ToString();
            lb_complate_count.Text = ServerGlobal.ProjectModels.FindAll(p => p.ProjectState == ProjectState.检修完成 || p.ProjectState == ProjectState.检修异常).Count.ToString();
        }

        private void ShowControl()
        {
            flp.Controls.Clear();
            ServerGlobal.ProgressProjectDict.Clear();
            foreach (ProjectModel item in ServerGlobal.ProjectModels)
            {
                AddControl(item);
            }
        }
    }
}
