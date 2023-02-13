using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using Project.ServerClass;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using UploadImageServer;

namespace Project
{
    public class Server : IProject
    {
        private AndroidServer androidServer = null;
        private IMainTask mainTask = new RobotControl() { Dock = DockStyle.Fill };

        public IAppServer appServer { get; set; } = new AppServer();
        public ICameraControl[] cameras { get; set; }
        public IDataBase dataBase { get; set; }
        public ILaserControl laser { get; set; }
        public ILightControl light { get; set; }
        public IRgvControl rgv { get; set; } = new RgvTask();
        public IRobotControl robot { get; set; } = new RobotTask();
        public IService upload { get; set; }
        public IHomePage homePage { get; set; }
        public string ChineseTitle { get; set; }
        public string EnglishTitle { get; set; }
        public string PathParameter1
        {
            get { return UploadImageServer.Properties.Settings.Default.UploadPath; }
            set { UploadImageServer.Properties.Settings.Default.UploadPath = value; }
        }
        public string PathParameter2
        {
            get { return UploadImageServer.Properties.Settings.Default.PuzzlePath; }
            set { UploadImageServer.Properties.Settings.Default.PuzzlePath = value; }
        }
        public string PathParameter3
        {
            get { return UploadImageServer.Properties.Settings.Default.ZipPath; }
            set { UploadImageServer.Properties.Settings.Default.ZipPath = value; }
        }
        public string PathParameter4
        {
            get { return UploadImageServer.Properties.Settings.Default.PuzzleExeConfig; }
            set { UploadImageServer.Properties.Settings.Default.PuzzleExeConfig = value; }
        }
        public string PathName1 { get { return "上传路径"; } }
        public string PathName2 { get { return "拼图路径"; } }
        public string PathName3 { get { return "压缩路径"; } }
        public string PathName4 { get { return "配置路径"; } }
        public Action SkinChanged { get; set; }
        public Action ColorChanged { get; set; }
        public Action<Action> FormInvoke { get; set; }
        public Action<Action> FormBeginInvoke { get; set; }

        public Server()
        {
            if (!Directory.Exists(ServerGlobal.BakDir))
            {
                Directory.CreateDirectory(ServerGlobal.BakDir);
            }
            if (!Directory.Exists(ServerGlobal.DataDir))
            {
                Directory.CreateDirectory(ServerGlobal.DataDir);
            }
            if (!Directory.Exists(ServerGlobal.ImageDir))
            {
                Directory.CreateDirectory(ServerGlobal.ImageDir);
            }
            if (File.Exists(ServerGlobal.PartsPath))
            {
                using (StreamReader sr = new StreamReader(ServerGlobal.PartsPath))
                {
                    PartsDict.PartDict = JsonManager.JsonToObject<Dictionary<PartsType, List<string>>>(sr.ReadToEnd());
                }
            }
            androidServer = new AndroidServer();
            ServerTask task = new ServerTask()
            {
                Project = this
            };
            task.SetSign += Task_SetSign;
            task.TaskStart(mainTask, appServer, null, null, null, null, null, null, null);
            ThreadManager.BackTask((int i, ThreadEventArgs threadEventArgs) =>
            {
                AppServer app = appServer as AppServer;
                byte[] logByte = app.logBytes;
                string log = System.Text.Encoding.Default.GetString(logByte, 0, logByte.Length).Replace("\0", "");
                log.AddLog(EMU.Parameter.LogType.TestLog);
                if (app.AlgorithmProjectID != null && ServerGlobal.ProgressProjectDict.ContainsKey(app.AlgorithmProjectID))
                {
                    ServerGlobal.ProgressProjectDict[app.AlgorithmProjectID](app.AlgorithmProgress, app.AlgorithmProgressMax); 
                }
            });
        }

        private void Task_SetSign(string txt, bool isStart = true)
        {
            (mainTask as RobotControl).SetSign(txt, isStart);
        }

        public void InitMenu(ToolStripMenuItem[] menus)
        {
            foreach (ToolStripMenuItem item in menus)
            {
                if (item.Name == "功能ToolStripMenuItem" ||
                    item.Name == "日志ToolStripMenuItem" ||
                    item.Name == "流程ToolStripMenuItem")
                {
                    item.Visible = false;
                }
                else if (item.Name == "设置ToolStripMenuItem")
                {
                    ToolStripItem[] addMenu = new ToolStripItem[] { new ToolStripSeparator(), new ToolStripMenuItem()
                    {
                        Text = "设置线阵数据"
                    }, new ToolStripMenuItem()
                    {
                        Text = "设置面阵数据"
                    }, new ToolStripMenuItem()
                    {
                        Text = "设置3D检测标准"
                    }, new ToolStripMenuItem()
                    {
                        Text = "设置螺丝定位"
                    }, new ToolStripSeparator(), new ToolStripMenuItem()
                    {
                        Text = "配置车型车号"
                    } };
                    addMenu[1].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        new SettingDataForm() { Text = addMenu[1].Text, DataType = ResultType.Xz }.Show();
                    });
                    addMenu[2].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        new SettingDataForm() { Text = addMenu[2].Text, DataType = ResultType.Mz }.Show();
                    });
                    addMenu[3].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        new SettingDataForm() { Text = addMenu[3].Text, DataType = ResultType.ThrD }.Show();
                    });
                    addMenu[4].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        new SettingScrewLocationForm().Show();
                    });
                    addMenu[6].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        new TrainModeSnForm().Show();
                    });
                    item.DropDownItems.AddRange(addMenu);
                }
            }
        }

        public void SavePathParameter() { UploadImageServer.Properties.Settings.Default.Save(); }

        public void SaveParameterObject() { Properties.Settings.Default.Save(); }

        public void SetPower(Address12Type type, bool open)
        {
            Cmd cmd = Cmd.back_camera_power_on;
            switch (type)
            {
                case Address12Type.RobotMzLedPower:
                    cmd = open ? Cmd.led_power_on : Cmd.led_power_off;
                    break;
                case Address12Type.RobotFrontMzPower:
                    cmd = open ? Cmd.front_camera_power_on : Cmd.front_camera_power_off;
                    break;
                case Address12Type.RobotXzLedPower:
                    break;
                case Address12Type.RobotBackMzPower:
                    cmd = open ? Cmd.back_camera_power_on : Cmd.back_camera_power_off;
                    break;
                case Address12Type.FrontRobotStepMotorPower:
                    cmd = open ? Cmd.front_sliding_power_on : Cmd.front_sliding_power_off;
                    break;
                case Address12Type.RobotXZPower:
                    cmd = open ? Cmd.line_camera_power_on : Cmd.line_camera_power_off;
                    break;
                case Address12Type.BackRobotStepMotorPower:
                    cmd = open ? Cmd.back_sliding_power_on : Cmd.back_sliding_power_off;
                    break;
                case Address12Type.FrontRobotStart:
                    break;
                case Address12Type.FrontRobotStop:
                    break;
                case Address12Type.FrontRobotEMGRst:
                    break;
                case Address12Type.FrontRobotAlmClear:
                    break;
                case Address12Type.BackRobotStart:
                    break;
                case Address12Type.BackRobotStop:
                    break;
                case Address12Type.BackRobotEMGRst:
                    break;
                case Address12Type.BackRobotAlmClear:
                    break;
            }
            RobotServer.Instance.Command(cmd);
        }

        public void SetBaslerCameraError(Action<string, object, object> action) { }

        public void SetBaslerCameraStatusChanged(Action<string, object, object> action) { }

        public void SetBaslerImageReady(Action<string, object, object> action) { }

        public object GetParameterObject() { return Properties.Settings.Default; }

        public PageControl[] InitPages()
        {
            PageControl[] pages = new PageControl[3];
            pages[0] = new PageControl()
            {
                Name = "检修计划",
                MainControl = new TaskProjectControl() { Dock = DockStyle.Fill, MainTask = mainTask, Project = this }
            };
            pages[1] = new PageControl()
            {
                Name = "机器人控制",
                MainControl = mainTask as UserControl
            };
            pages[2] = new PageControl()
            {
                Name = "检测历史",
                MainControl = new DetectionResultHistoryControl() { Dock = DockStyle.Fill }
            };
            return pages;
        }

        public UserControl BaslerUserControl()
        {
            return null;
        }

        public UserControl CognexUserControl()
        {
            return null;
        }

        public UserControl HikUserControl()
        {
            return null;
        }

        public UserControl LaserUserControl()
        {
            return null;
        }

        public UserControl LidarUserControl()
        {
            return null;
        }

        public UserControl LightUserControl()
        {
            return null;
        }

        public UserControl RgvUserControl(Color color)
        {
            return null;
        }

        public UserControl RobotUserControl()
        {
            return null;
        }

        public UserControl ServerUserControl()
        {
            return null;
        }

        public UserControl UploadImageUserControl()
        {
            return null;
        }
    }
}
