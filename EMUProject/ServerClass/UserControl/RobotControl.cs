using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public partial class RobotControl : UserControl, IMainTask
    {
        private Dictionary<string, RgvGlobalInfo> rgvInfos;
        private Dictionary<string, RobotGlobalInfo> robotInfos;
        private Dictionary<string, Button> robotButton = null;

        public RobotControl()
        {
            InitializeComponent();
            rgvInfos = new Dictionary<string, RgvGlobalInfo>();
            robotInfos = new Dictionary<string, RobotGlobalInfo>();
            robotButton = new Dictionary<string, Button>();
        }

        private void RobotControl_Load(object sender, EventArgs e)
        {
            tb_log.Visible = Properties.Settings.Default.日志;
            RobotServer.Instance.LinkEvent += Instance_LinkEvent;
            ThreadManager.BackTask((int i, ThreadEventArgs threadEventArgs) =>
            {
                if (i % 2 > 0)
                {
                    return;
                }
                if (robotButton.Count > ServerGlobal.LinkRobotList.Count)
                {
                    List<string> removeID = new List<string>();
                    foreach (KeyValuePair<string, Button> item in robotButton)
                    {
                        if (ServerGlobal.LinkRobotList.IndexOf(item.Key) < 0)
                        {
                            removeID.Add(item.Key);
                        }
                    }
                    foreach (string item in removeID)
                    {
                        DelRobotButton(item);
                    }
                }
            });
        }

        private void Instance_LinkEvent(string obj)
        {
            if (string.IsNullOrEmpty(ServerGlobal.SelectRobotID))
            {
                ServerGlobal.SelectRobotID = obj;
            }
            if (ServerGlobal.LinkRobotList.IndexOf(obj) < 0)
            {
                ServerGlobal.LinkRobotList.Add(obj); 
            }
            AddRobotButton(obj);
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            string mode = "380AL", sn = "2589";
            if (ServerGlobal.StartProjectDict.ContainsKey(ServerGlobal.SelectRobotID))
            {
                mode = ServerGlobal.StartProjectDict[ServerGlobal.SelectRobotID].Mode;
                sn = ServerGlobal.StartProjectDict[ServerGlobal.SelectRobotID].Sn;
            }

            RobotServer.Instance.Command(Cmd.robot_linedata, mode, sn, ServerGlobal.SelectRobotID);
            RobotServer.Instance.GetRecv(Cmd.robot_linedata, () =>
            {
                Thread.Sleep(1000);
                RobotServer.Instance.Command(Cmd.robot_backdata, mode, sn, ServerGlobal.SelectRobotID);
                RobotServer.Instance.GetRecv(Cmd.robot_backdata, () =>
                {
                    Thread.Sleep(1000);
                    RobotServer.Instance.Command(Cmd.start_work, mode, sn);
                }, (string error) =>
                {
                    MessageBox.Show(error);
                });
            }, (string error) =>
            {
                MessageBox.Show(error);
            });
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.stop_work);
            ServerGlobal.RobotTaskEnd(ServerGlobal.SelectRobotID);
        }

        private void btn_home_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.stop_work);
            Thread.Sleep(500);
            RobotServer.Instance.Command(Cmd.home_work);
            ServerGlobal.RobotTaskEnd(ServerGlobal.SelectRobotID);
        }

        private void btn_stop_power_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.powerstop);
        }

        private void btn_clear_alarm_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.clear_alarm);
            if (rgvInfos.ContainsKey(ServerGlobal.SelectRobotID))
            {
                RgvGlobalInfo rgv = rgvInfos[ServerGlobal.SelectRobotID];
                if (ServerGlobal.RgvJob.ContainsKey(rgv.ID) && ServerGlobal.RgvJob[rgv.ID] != "面阵任务执行中")
                {
                    if (ServerGlobal.StartProjectDict.ContainsKey(rgv.ID))
                    {
                        ServerGlobal.StartProjectDict.Remove(rgv.ID);
                    }
                    ServerGlobal.RobotTaskEnd(ServerGlobal.SelectRobotID);
                }
            }
        }

        private void btn_forward_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.forward);
        }

        private void btn_backward_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.backward);
        }

        private void btn_rgv_stop_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.rgv_stop);
        }

        private void btn_zero_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.robot_zero);
        }

        private void btn_power_down_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.powerdown);
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            if (cb_speed.Checked)
            {
                RobotServer.Instance.Command(Cmd.set_speed, tb_speed.Text);
                Thread.Sleep(50);
            }
            if (cb_distacnce.Checked)
            {
                RobotServer.Instance.Command(Cmd.set_distance, tb_distacnce.Text);
                Thread.Sleep(50);
            }
            if (cb_length.Checked)
            {
                RobotServer.Instance.Command(Cmd.set_length, tb_length.Text);
            }
        }

        private void btn_run_Click(object sender, EventArgs e)
        {
            RobotServer.Instance.Command(Cmd.rgv_run);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            foreach (Control item in flp.Controls)
            {
                if (item != button)
                {
                    item.Enabled = true;
                }
            }
            button.Enabled = false;
            ServerGlobal.SelectRobotID = button.Tag.ToString();
            cameraControl1.ClearImage(CameraName.Back);
            cameraControl1.ClearImage(CameraName.Front);
            cameraControl1.ClearImage(CameraName.Line);
            ShowInfo();
        }

        private void AddRobotButton(string id)
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (!robotButton.ContainsKey(id))
                    {
                        Button button = new Button();
                        button.Size = new Size(80, 80);
                        button.Tag = id;
                        button.Text = "机器人\r\n" + id;
                        button.Click += Button_Click;
                        flp.Controls.Add(button);
                        robotButton.Add(id, button);

                        if (robotButton.Count == 1)
                        {
                            button.Enabled = false;
                        }
                    }
                }));
            }
        }

        private void DelRobotButton(string id)
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (robotButton.ContainsKey(id))
                    {
                        flp.Controls.Remove(robotButton[id]);
                        robotButton[id].Dispose();
                        robotButton.Remove(id); 
                    }
                }));
            }
        }

        public void RunTask(TaskName task, Action complete)
        {
            try
            {
                switch (task)
                {
                    case TaskName.Start:
                        RgvGlobalInfo rgv = GetRgv();
                        if (rgv?.RgvCurrentRunDistacnce > 5000)
                        {
                            btn_home_Click(null, null); 
                        }
                        while (rgv?.RgvCurrentRunDistacnce > 5010 ||
                            rgv?.RgvRunStatMonitor == EquipmentStatus.RUN)
                        {
                            Thread.Sleep(1000);
                        }
                        btn_start_Click(null, null);
                        break;
                    case TaskName.Stop:
                        break;
                    case TaskName.Forward:
                        break;
                    case TaskName.Back:
                        break;
                    case TaskName.Clear:
                        break;
                }
                complete?.Invoke();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SetImage(Image image, CameraName camera)
        {
            cameraControl1.SetImage(image, camera);
        }

        public void SetLog(string log)
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (tb_log.Visible)
                    {
                        tb_log.Text += log;
                    }
                }));
            }
        }

        public void SetRgvInfo(RgvGlobalInfo rgv)
        {
            if (rgvInfos.ContainsKey(rgv.ID))
            {
                rgvInfos[rgv.ID] = rgv;
            }
            else
            {
                rgvInfos.Add(rgv.ID, rgv);
            }
            SetRgvState();
            ShowInfo(showRobot: false);
        }

        public void SetRobotInfo(RobotGlobalInfo robot)
        {
            if (robotInfos.ContainsKey(robot.RobotID))
            {
                robotInfos[robot.RobotID] = robot;
            }
            else
            {
                robotInfos.Add(robot.RobotID, robot);
            }
            ShowInfo(false);
        }

        public void SetTaskLocation(string taskContext, int taskProgress)
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (!string.IsNullOrEmpty(taskContext))
                    {
                        if (taskContext.Contains("面阵任务执行中") && !trainBar.RightToLeftLayout)
                        {
                            trainBar.RightToLeft = RightToLeft.Yes;
                            trainBar.RightToLeftLayout = true;
                        }
                        else if (taskContext.Contains("线阵任务执行中") && trainBar.RightToLeftLayout)
                        {
                            trainBar.RightToLeft = RightToLeft.No;
                            trainBar.RightToLeftLayout = false;
                        } 
                    }
                }));
            }
        }

        public void SetSign(string id, bool isStart)
        {
            string[] vs = id.Split(',');
            rgvInfoControl1.SetSign(int.Parse(vs[1]) + 1, int.Parse(vs[2]), vs[0], isStart);
        }

        private void SetRgvState()
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    RgvGlobalInfo rgv = GetRgv();
                    if (rgv != null)
                    {
                        switch (rgv.RgvRunStatMonitor)
                        {
                            case EquipmentStatus.RUN:
                                break;
                            default:
                                break;
                        } 
                    }
                }));
            }
        }

        private void ShowInfo(bool showRgv = true, bool showRobot = true)
        {
            if (!this.IsDisposed)
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (showRgv && rgvInfos.ContainsKey(ServerGlobal.SelectRobotID))
                    {
                        RgvGlobalInfo rgv = rgvInfos[ServerGlobal.SelectRobotID];
                        rgvInfoControl1.RgvGlobalInfo = rgv;
                        trainBar.Maximum = rgv.RgvTrackLength;
                        if (trainBar.RightToLeftLayout)
                        {
                            int value = trainBar.Maximum - rgv.RgvCurrentRunDistacnce;
                            if (value > -1 && value < rgv.RgvTrackLength)
                            {
                                trainBar.Value = value; 
                            }
                        }
                        else
                        {
                            if (rgv.RgvCurrentRunDistacnce <= rgv.RgvTrackLength)
                            {
                                trainBar.Value = rgv.RgvCurrentRunDistacnce; 
                            }
                        }
                        btn_stop.Enabled = btn_rgv_stop.Enabled = rgv.RgvRunStatMonitor == EquipmentStatus.RUN;
                        btn_home.Enabled = !btn_rgv_stop.Enabled && rgv.RgvCurrentRunDistacnce > 5000;
                        btn_start.Enabled = btn_forward.Enabled = btn_backward.Enabled = rgv.RgvRunStatMonitor != EquipmentStatus.RUN;
                        btn_stop_power.Enabled = Math.Abs(rgv.RgvCurrentRunDistacnce - 5000) < 10 && rgv.RgvRunStatMonitor != EquipmentStatus.RUN;
                    }
                    if (showRobot && robotInfos.ContainsKey(ServerGlobal.SelectRobotID))
                    {
                        RobotGlobalInfo robot = robotInfos[ServerGlobal.SelectRobotID];
                        frontRobotControl1.SetConnect(robot.FrontRobotConnStat);
                        frontRobotControl1.SetState(robot.FrontRobotRunStatMonitor);
                        frontRobotControl1.SetInfo(robot.FrontRobotSiteData);
                        backRobotControl1.SetConnect(robot.FrontRobotConnStat);
                        backRobotControl1.SetState(robot.FrontRobotRunStatMonitor);
                        backRobotControl1.SetInfo(robot.FrontRobotSiteData);
                        btn_zero.Enabled = robot.BackRobotRunStatMonitor != EquipmentStatus.RUN && robot.FrontRobotRunStatMonitor != EquipmentStatus.RUN;
                    }
                }));
            }
        }

        private RgvGlobalInfo GetRgv()
        {
            if (rgvInfos.ContainsKey(ServerGlobal.SelectRobotID))
            {
                return rgvInfos[ServerGlobal.SelectRobotID];
            }
            return null;
        }
    }
}
