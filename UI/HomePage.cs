using Basler;
using EMU.ApplicationData;
using EMU.Business;
using EMU.BusinessManager;
using EMU.ImageTransmission;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class HomePage : UserControl, IHomePage, IMainTask
    {
        private Dictionary<int, ITask> tasks = null;
        private Dictionary<TaskName, ITask> taskDict = null;
        public string Title { get; set; } = "控制";
        public Iinit InitObject { get; set; }
        public IProject Project { get; set; }

        private bool ButtonRunEnable
        {
            set
            {
                btn_start.Enabled = btn_forward.Enabled = btn_back.Enabled = btn_head.Enabled = btn_check.Enabled = value;
                btn_stop.Enabled = !value;
            }
        }

        public HomePage()
        {
            InitializeComponent();
            LogManager.AddLogEvent += LogManager_AddLogEvent;
            UIManager.ClearUIEvent += UIManager_ClearUIEvent;
        }

        private void LogManager_AddLogEvent(string arg1, LogType arg2)
        {
            SetLog(arg1);
        }

        private void UIManager_ClearUIEvent(string obj)
        {
            if (obj == "HomePage")
            {
                btn_start.Click -= btn_start_Click;
                btn_head.Click -= btn_head_Click;
                btn_stop.Click -= btn_stop_Click;
                btn_forward.Click -= btn_forward_Click;
                btn_back.Click -= btn_back_Click;
                btn_clear.Click -= btn_clear_Click;
                btn_check.Click -= btn_check_Click; 
            }
        }

        public void CheckHeadDetection()
        {
            signControl.ClearData();
            tasks[HeadDetection.taskIndex].TaskStart(this, Project.appServer, Project.cameras, Project.laser, Project.light, Project.rgv, Project.robot, Project.upload, null);
            while (!tasks[HeadDetection.taskIndex].TaskComplete())
            {
                Thread.Sleep(100);
            }
        }

        public void RunTask(TaskName task, Action complete)
        {
            Task.Run(() =>
            {
                UserControl userControl = null;
                switch (task)
                {
                    case TaskName.Start:
                        break;
                    case TaskName.Stop:
                        break;
                    case TaskName.Forward:
                        signControl.ClearData();
                        break;
                    case TaskName.Back:
                        signControl.SetEnd();
                        break;
                    case TaskName.Clear:
                        break;
                }
                taskDict[task].TaskStart(this, Project.appServer, Project.cameras, Project.laser, Project.light, Project.rgv, Project.robot, Project.upload, userControl, GetTasks().ToArray());
                while (!taskDict[task].TaskComplete())
                {
                    Thread.Sleep(100);
                }
                complete?.Invoke();
            });
        }

        public void SetImage(Image image, CameraName camera)
        {
            cameraControl.SetImage(image, camera);
        }

        public void SetLog(string log)
        {
            if (!IsDisposed)
            {
                BeginInvoke(new Action(() =>
                {
                    tb_log.Text += log + "\r\n";
                    tb_log.SelectionStart = tb_log.Text.Length;
                    tb_log.ScrollToCaret();
                })); 
            }
        }

        public void SetRgvInfo(RgvGlobalInfo rgv)
        {
            RgvGlobalInfo.Instance.ModWorkCode = rgv.ModWorkCode;
            RgvGlobalInfo.Instance.ModWorkMsg = rgv.ModWorkMsg;
            RgvGlobalInfo.Instance.RgvCurrentCmdSetStat = rgv.RgvCurrentCmdSetStat;
            RgvGlobalInfo.Instance.RgvCurrentMode = rgv.RgvCurrentMode;
            RgvGlobalInfo.Instance.RgvCurrentParaSetStat = rgv.RgvCurrentParaSetStat;
            RgvGlobalInfo.Instance.RgvCurrentPowerCurrent = rgv.RgvCurrentPowerCurrent;
            RgvGlobalInfo.Instance.RgvCurrentPowerElectricity = rgv.RgvCurrentPowerElectricity;
            RgvGlobalInfo.Instance.RgvCurrentPowerStat = rgv.RgvCurrentPowerStat;
            RgvGlobalInfo.Instance.RgvCurrentPowerTempture = rgv.RgvCurrentPowerTempture;
            RgvGlobalInfo.Instance.RgvCurrentRunDistacnce = rgv.RgvCurrentRunDistacnce;
            RgvGlobalInfo.Instance.RgvCurrentRunSpeed = rgv.RgvCurrentRunSpeed;
            RgvGlobalInfo.Instance.RgvCurrentStat = rgv.RgvCurrentStat;
            RgvGlobalInfo.Instance.RgvIsAlarm = rgv.RgvIsAlarm;
            RgvGlobalInfo.Instance.RgvTargetRunDistance = rgv.RgvTargetRunDistance;
            RgvGlobalInfo.Instance.RgvTargetRunSpeed = rgv.RgvTargetRunSpeed;
            RgvGlobalInfo.Instance.RgvTrackLength = rgv.RgvTrackLength;
            rgvInfoControl.SetRgvInfo(rgv);
        }

        public void SetRobotInfo(RobotGlobalInfo robot)
        {
            frontRobotControl.SetInfo(robot.FrontRobotSiteData);
            backRobotControl.SetInfo(robot.BackRobotSiteData);
        }

        public void SetTaskLocation(string taskContext, int taskProgress)
        {
            BeginInvoke(new Action(() =>
            {
                lb.Text = "当前任务：" + taskContext;
            }));
        }

        private void HomePage_Load(object sender, EventArgs e)
        {
            InitObject.Setup(Project, this);
            InitObject.Setup(out tasks);
            InitObject.Setup(out taskDict);

            ISign[] signs = new ISign[]
            {
                tasks[HeadDetection.taskIndex] as ISign,
                tasks[ForwardTask.taskIndex] as ISign,
                tasks[BackTask.taskIndex] as ISign
            };
            foreach (ISign sign in signs)
            {
                sign.SetAlarm += signControl.ShowAlarm;
                sign.ClearSign += signControl.ClearData;
                sign.EndSign += signControl.SetEnd;
            }
            (tasks[HeadDetection.taskIndex] as ISign).SetSign += HomePage_SetSign;
            (tasks[ForwardTask.taskIndex] as ISign).SetSign += HomePage_SetSign;
            (tasks[BackTask.taskIndex] as ISign).SetSign += HomePage_SetSign_Back;

            btn_check_Click(null, null);
        }

        private void HomePage_SetSign(string txt, bool isStart = true)
        {
            signControl.SetData(isStart, RgvGlobalInfo.Instance.RgvCurrentRunDistacnce, RgvGlobalInfo.Instance.RgvTrackLength, txt);
        }

        private void HomePage_SetSign_Back(string txt, bool isStart = true)
        {
            signControl.SetEnd();
            signControl.SetData(isStart, RgvGlobalInfo.Instance.RgvCurrentRunDistacnce, RgvGlobalInfo.Instance.RgvTrackLength, txt);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            ButtonRunEnable = false;
            RunTask(TaskName.Start, () =>
            {
                ButtonRunEnable = true;
            });
        }

        private void btn_head_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                CheckHeadDetection();
                Project.rgv.RgvNormalStop();
            });
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            ButtonRunEnable = false;
            RunTask(TaskName.Stop, () =>
            {
                ButtonRunEnable = true;
            });
        }

        private void btn_forward_Click(object sender, EventArgs e)
        {
            ButtonRunEnable = false;
            RunTask(TaskName.Forward, () =>
            {
                ButtonRunEnable = true;
            });
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            ButtonRunEnable = false;
            RunTask(TaskName.Back, () =>
            {
                ButtonRunEnable = true;
            });
        }

        private void btn_wait_Click(object sender, EventArgs e)
        {
            Project.rgv.RgvNormalStop();
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            RunTask(TaskName.Clear, null);
        }

        private void btn_check_Click(object sender, EventArgs e)
        {
            tasks[InitTask.taskIndex].TaskStart(this, Project.appServer, Project.cameras, Project.laser, Project.light, Project.rgv, Project.robot, Project.upload, this);
            Task.Run(() =>
            {
                while (!tasks[InitTask.taskIndex].TaskComplete())
                {
                    Thread.Sleep(500);
                }
                GlobalValues.UserInfo.myDeviceStat = UserEntity.key_DEVICE_INIT;
            });
        }

        private List<ITask> GetTasks()
        {
            List<ITask> list = new List<ITask>();
            int count = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                if (tasks == null)
                {
                    break;
                }
                if (count >= tasks.Count)
                {
                    break;
                }
                if (tasks.ContainsKey(i))
                {
                    list.Add(tasks[i]);
                    count++;
                }
                else
                {
                    list.Add(null);
                }
            }
            return list;
        }
    }
}
