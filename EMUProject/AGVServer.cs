using EMU.Interface;
using EMU.Util;
using Project.AGV;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UploadImageServer;

namespace Project
{
    public class AGVServer : IProject
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
        public string PathParameter1 { get; set; }
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
                    ToolStripItem[] addMenu = new ToolStripItem[] { new ToolStripMenuItem()
                    {
                        Text = "AGV 控制"
                    }, new ToolStripMenuItem()
                    {
                        Text = ""
                    }, new ToolStripMenuItem()
                    {
                        Text = ""
                    }, new ToolStripMenuItem()
                    {
                        Text = ""
                    }, new ToolStripMenuItem()
                    {
                        Text = ""
                    } };
                    addMenu[0].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        new TestAGVForm() { Text = addMenu[0].Text }.Show();
                    });
                    addMenu[1].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        
                    });
                    addMenu[2].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        
                    });
                    addMenu[3].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        
                    });
                    addMenu[4].Click += new EventHandler((object sender, EventArgs e) =>
                    {
                        
                    });
                    item.DropDownItems.Insert(0, addMenu[0]);
                }
            }
        }

        public PageControl[] InitPages()
        {
            PageControl[] pages = new PageControl[1];
            pages[0] = new PageControl()
            {
                Name = "AGV测试",
                MainControl = new global::Project.AGV.TestAGVControl() { Dock = DockStyle.Fill }
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
    }
}
