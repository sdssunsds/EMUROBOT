using EMU.Interface;
using EMU.Util;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using UploadImageServer;

namespace Project
{
    public class AlgorithmInterface : IProject
    {
        public IAppServer appServer { get; set; }
        public ICameraControl[] cameras { get; set; }
        public IDataBase dataBase { get; set; }
        public ILaserControl laser { get; set; }
        public ILightControl light { get; set; }
        public IRgvControl rgv { get; set; }
        public IRobotControl robot { get; set; }
        public IService upload { get; set; }
        public IHomePage homePage { get; set; }
        public string PathParameter1 { get; set; } = Application.StartupPath + "\\operation.ini";
        public string PathParameter2 { get; set; }
        public string PathParameter3 { get; set; }
        public string PathParameter4 { get; set; }

        public string PathName1 { get { return "待定"; } }

        public string PathName2 { get { return "待定"; } }

        public string PathName3 { get { return "待定"; } }

        public string PathName4 { get { return "待定"; } }

        public Action SkinChanged { get; set; }
        public Action ColorChanged { get; set; }
        public Action<Action> FormInvoke { get; set; }
        public Action<Action> FormBeginInvoke { get; set; }

        public UserControl BaslerUserControl()
        {
            return null;
        }

        public UserControl CognexUserControl()
        {
            return null;
        }

        public object GetParameterObject()
        {
            return null;
        }

        public UserControl HikUserControl()
        {
            return null;
        }

        public void InitMenu(ToolStripMenuItem[] menus)
        {
            foreach (ToolStripMenuItem item in menus)
            {
                if (item.Name == "功能ToolStripMenuItem" ||
                    item.Name == "日志ToolStripMenuItem" ||
                    item.Name == "流程ToolStripMenuItem" ||
                    item.Name == "图片ToolStripMenuItem" ||
                    item.Name == "控制ToolStripMenuItem" ||
                    item.Name == "缓存ToolStripMenuItem" ||
                    item.Name == "参数ToolStripMenuItem")
                {
                    item.Visible = false;
                }
                else if (item.Name == "软件ToolStripMenuItem")
                {
                    item.DropDownItems.RemoveByKey("toolStripMenuItem3");
                }
                else if (item.Name == "设置ToolStripMenuItem")
                {
                    item.DropDownItems.RemoveByKey("toolStripMenuItem1");
                }
            }
        }

        public PageControl[] InitPages()
        {
            PageControl[] pages = new PageControl[3];
            pages[0] = new PageControl()
            {
                Name = "服务控制",
                MainControl = new MainPage() { Dock = DockStyle.Fill, Project = this }
            };
            pages[1] = new PageControl()
            {
                Name = "算法接口",
                MainControl = new AlgorithmPage() { Dock = DockStyle.Fill, Project = this }
            };
            pages[2] = new PageControl()
            {
                Name = "日志",
                MainControl = new LogPage() { Dock = DockStyle.Fill }
            };
            return pages;
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

        public void SaveParameterObject()
        {
            
        }

        public void SavePathParameter()
        {
            
        }

        public UserControl ServerUserControl()
        {
            return null;
        }

        public void SetBaslerCameraError(Action<string, object, object> action)
        {
            
        }

        public void SetBaslerCameraStatusChanged(Action<string, object, object> action)
        {
            
        }

        public void SetBaslerImageReady(Action<string, object, object> action)
        {
            
        }

        public void SetPower(Address12Type type, bool open)
        {
            
        }

        public UserControl UploadImageUserControl()
        {
            return null;
        }

        #region 接口专用变量
        private bool isRunInterface = false;
        private int reportSleep = 0;
        private string reportInput = "";
        private string reportOutput = "";
        private string reportUrl = "";

        public string RedisThreadName = "Redis请求线程";
        #endregion

        #region 接口专用方法
        public AlgorithmInterface()
        {
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                threadEventArgs.ThreadName = RedisThreadName;
                while (true)
                {
                    while (isRunInterface)
                    {
                        Thread.Sleep(reportSleep);

                    } 
                }
            });
        }

        public void RunInterface(string url, string inputName, string outputName, int sleep)
        {
            reportUrl = url;
            reportInput = inputName;
            reportOutput = outputName;
            reportSleep = sleep;
            isRunInterface = true;
        }
        #endregion
    }
}
