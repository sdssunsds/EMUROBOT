using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EMU.UI
{
    public partial class MainControl : UserControl
    {
        public IProject Project { get; set; }

        private IHomePage homePage
        {
            get
            {
                return Project?.homePage;
            }
        }
        private IRgvControl rgv
        {
            get { return Project.rgv; }
        }
        private IRobotControl robot
        {
            get { return Project.robot; }
        }

        public MainControl()
        {
            InitializeComponent();
            流程日志ToolStripMenuItem.Tag = LogType.ProcessLog;
            普通日志ToolStripMenuItem.Tag = LogType.GeneralLog;
            异常日志ToolStripMenuItem.Tag = LogType.ErrorLog;
            相机日志ToolStripMenuItem.Tag = LogType.CameraLog;
            rGV日志ToolStripMenuItem.Tag = LogType.RgvLog;
            机械臂日志ToolStripMenuItem.Tag = LogType.RobotLog;
            调试日志ToolStripMenuItem.Tag = LogType.TestLog;
            其它日志ToolStripMenuItem.Tag = LogType.OtherLog;
        }

        private void MainControl_Load(object sender, EventArgs e)
        {
            版本ToolStripMenuItem.Text += MainForm.Version;

            if (homePage != null)
            {
                UserControl userControl = homePage as UserControl;
                userControl.Dock = DockStyle.Fill;
                OpenPage(homePage.Title, userControl);
            }

            PageControl[] pages = Project?.InitPages();
            if (pages != null)
            {
                foreach (PageControl page in pages)
                {
                    OpenPage(page.Name, page.MainControl);
                }
                tabControl1.SelectedIndex = 0;
            }

            List<ToolStripMenuItem> list = new List<ToolStripMenuItem>();
            foreach (ToolStripMenuItem item in menuStrip1.Items)
            {
                list.Add(item);
                AddMenu(list, item);
            }
            Project?.InitMenu(list.ToArray());
        }

        private void 拼图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PuzzleForm().Show(this.Parent);
        }

        private void 导出线阵ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export(Business.Properties.Settings.Default.ImgSavePath + "\\" + Business.Global.LinePathName);
        }

        private void 导出前臂ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export(Business.Properties.Settings.Default.ImgSavePath + "\\" + Business.Global.FrontPathName);
        }

        private void 导出后臂ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export(Business.Properties.Settings.Default.ImgSavePath + "\\" + Business.Global.BackPathName);
        }

        private void 导出全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export(
                Business.Properties.Settings.Default.ImgSavePath + "\\" + Business.Global.LinePathName,
                Business.Properties.Settings.Default.ImgSavePath + "\\" + Business.Global.FrontPathName,
                Business.Properties.Settings.Default.ImgSavePath + "\\" + Business.Global.BackPathName
                );
        }

        private void 车头检测ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Task.Run(() => { homePage.CheckHeadDetection(); });
        }

        private void 前行流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            homePage.RunTask(TaskName.Forward, null);
        }

        private void 返回流程ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            homePage.RunTask(TaskName.Back, null);
        }

        private void 正向运动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rgv.RgvForwardMove();
        }

        private void 反向运动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rgv.RgvBackMove();
        }

        private void 停车ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rgv.RgvNormalStop();
        }

        private void 充电ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rgv.RgvIntelligentCharging();
        }

        private void 回到原点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            robot.RobotZeroPosition(RobotName.Front);
            robot.RobotZeroPosition(RobotName.Back);
        }

        private void 面阵光源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPower(sender as ToolStripMenuItem, () =>
            {
                Project.SetPower(Address12Type.RobotMzLedPower, true);
            }, () =>
            {
                Project.SetPower(Address12Type.RobotMzLedPower, false);
            });
        }

        private void 线阵相机电源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPower(sender as ToolStripMenuItem, () =>
            {
                Project.SetPower(Address12Type.RobotXZPower, true);
            }, () =>
            {
                Project.SetPower(Address12Type.RobotXZPower, false);
            });
        }

        private void 前面阵相机电源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPower(sender as ToolStripMenuItem, () =>
            {
                Project.SetPower(Address12Type.RobotFrontMzPower, true);
            }, () =>
            {
                Project.SetPower(Address12Type.RobotFrontMzPower, false);
            });
        }

        private void 后面阵相机电源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPower(sender as ToolStripMenuItem, () =>
            {
                Project.SetPower(Address12Type.RobotBackMzPower, true);
            }, () =>
            {
                Project.SetPower(Address12Type.RobotBackMzPower, false);
            });
        }

        private void 前滑台电源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPower(sender as ToolStripMenuItem, () =>
            {
                Project.SetPower(Address12Type.FrontRobotStepMotorPower, true);
            }, () =>
            {
                Project.SetPower(Address12Type.FrontRobotStepMotorPower, false);
            });
        }

        private void 后滑台电源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetPower(sender as ToolStripMenuItem, () =>
            {
                Project.SetPower(Address12Type.BackRobotStepMotorPower, true);
            }, () =>
            {
                Project.SetPower(Address12Type.BackRobotStepMotorPower, false);
            });
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void baslerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("Basler相机", BaslerUserControl());
        }

        private void 海康ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("海康相机", HikUserControl());
        }

        private void 康耐视ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("康耐视相机", CognexUserControl());
        }

        private void rGV车ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("RGV控制", RgvUserControl(Properties.Settings.Default.Color));
        }

        private void 机械臂ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("机械臂控制", RobotUserControl());
        }

        private void 光源ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("光源控制", LightUserControl());
        }

        private void 测距激光ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("测距控制", LaserUserControl());
        }

        private void 激光雷达ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("激光雷达", LidarUserControl());
        }

        private void 图片服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("图片服务", UploadImageUserControl());
        }

        private void 本地服务ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenPage("本地服务", ServerUserControl());
        }

        private void 日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            item.Checked = !item.Checked;
            LogType logType = (LogType)item.Tag;
            LogManager.LogTypeDict[logType] = item.Checked;
        }

        private void 皮肤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Skin.SelectSkin skin = new Skin.SelectSkin();
            skin.ShowDialog(this.Parent);
            if (!string.IsNullOrEmpty(skin.SSK))
            {
                Properties.Settings.Default.SkinFile = skin.SSK;
                Properties.Settings.Default.Save();
                Project.SkinChanged?.Invoke();
            }
        }

        private void 主题色ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeColorForm form = new ChangeColorForm();
            form.SelectColor = Properties.Settings.Default.Color;
            form.FontColor = Properties.Settings.Default.FontColor;
            form.ShowDialog(this.Parent);
            Properties.Settings.Default.Color = form.SelectColor;
            Properties.Settings.Default.FontColor = form.FontColor;
            Properties.Settings.Default.Save();
            Project.ColorChanged?.Invoke();
        }

        private void 缓存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new PathManagerForm() { Project = this.Project }.Show(this.Parent);
        }

        private void 参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ParameterForm() { Project = this.Project }.Show(this.Parent);
        }

        private void 操作文档ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关闭选项卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Text != "首页")
            {
                tabControl1.SelectedTab.Controls[0].Dispose();
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }
        }

        private void 关闭所有选项卡ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = tabControl1.TabPages.Count - 1; i > 0; i--)
            {
                tabControl1.TabPages[i].Controls[0].Dispose();
                tabControl1.TabPages.RemoveAt(i);
            }
        }

        private void AddMenu(List<ToolStripMenuItem> list, ToolStripMenuItem menu)
        {
            if (menu.DropDownItems != null)
            {
                foreach (var item in menu.DropDownItems)
                {
                    if (item is ToolStripMenuItem)
                    {
                        ToolStripMenuItem _menu = item as ToolStripMenuItem;
                        list.Add(_menu);
                        AddMenu(list, _menu);
                    }
                } 
            }
        }

        private void Export(params string[] dir)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                ProgressBarForm progressBar = new ProgressBarForm();
                ExportManager export = new ExportManager();
                export.ExportInternalEvent += progressBar.SetValue;
                export.ExportMaxEvent += progressBar.SetMax;
                progressBar.Run = () =>
                {
                    export.ExportDir(folderBrowserDialog.SelectedPath, dir);
                };
                progressBar.ShowDialog(this.Parent);
                MessageBox.Show("导出完成");
            }
        }

        private void OpenPage(string title, UserControl userControl)
        {
            if (!tabControl1.TabPages.ContainsKey(title))
            {
                TabPage page = new TabPage(title) { AutoScroll = true };
                if (userControl.Dock != DockStyle.Fill)
                {
                    userControl.Size = new Size(userControl.Width, page.Height);
                    userControl.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
                }
                page.Controls.Add(userControl);
                tabControl1.TabPages.Add(page);
            }

            foreach (TabPage page in tabControl1.TabPages)
            {
                if (page.Text == title)
                {
                    tabControl1.SelectTab(page);
                    break;
                }
            }
        }

        private void SetPower(ToolStripMenuItem item, Action on, Action off)
        {
            if (item.Text.Contains("上电"))
            {
                item.Text = item.Text.Replace("上电", "断电");
                on();
            }
            else
            {
                item.Text = item.Text.Replace("断电", "上电");
                off();
            }
        }

        private UserControl BaslerUserControl()
        {
            return Project.BaslerUserControl();
        }

        private UserControl CognexUserControl()
        {
            return Project.CognexUserControl();
        }

        private UserControl HikUserControl()
        {
            return Project.HikUserControl();
        }

        private UserControl LaserUserControl()
        {
            return Project.LaserUserControl();
        }

        private UserControl LidarUserControl()
        {
            return Project.LidarUserControl();
        }

        private UserControl LightUserControl()
        {
            return Project.LightUserControl();
        }

        private UserControl RgvUserControl(Color color)
        {
            return Project.RgvUserControl(color);
        }

        private UserControl RobotUserControl()
        {
            return Project.RobotUserControl();
        }

        private UserControl UploadImageUserControl()
        {
            return Project.UploadImageUserControl();
        }

        private UserControl ServerUserControl()
        {
            return Project.ServerUserControl();
        }
    }
}
