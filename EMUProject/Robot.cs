using Basler;
using EMU.Business;
using EMU.BusinessManager;
using EMU.ImageTransmission;
using EMU.Interface;
using EMU.Util;
using Laser;
using Light;
using Rgv;
using Robot;
using System;
using System.Drawing;
using System.Windows.Forms;
using UploadImageServer;

namespace Project
{
    public class RobotClient : IProject
    {
        private Action<string, object, object> imageReady = null;
        private Action<string, object, object> cameraError = null;
        private Action<string, object, object> cameraStatusChanged = null;

        public IAppServer appServer { get; set; } = HttpServerHelper.Instance;
        public ICameraControl[] cameras { get; set; }
        public IDataBase dataBase { get; set; }
        public ILaserControl laser { get; set; } = LaserManager.Instance;
        public ILightControl light { get; set; } = LightManager.Instance;
        public IRgvControl rgv { get; set; } = RgvModCtrlHelper.Instance;
        public IRobotControl robot { get; set; } = RobotModCtrlHelper.Instance;
        public IService upload { get; set; } = UploadImages.Instance;
        public IHomePage homePage { get; set; }
        public string PathParameter1
        { 
            get { return EMU.Business.Properties.Settings.Default.ImgSavePath; }
            set { EMU.Business.Properties.Settings.Default.ImgSavePath = value; }
        }
        public string PathParameter2
        {
            get { return EMU.Business.Properties.Settings.Default.DataJsonSavePath; }
            set { EMU.Business.Properties.Settings.Default.DataJsonSavePath = value; }
        }
        public string PathParameter3
        {
            get { return EMU.Business.Properties.Settings.Default.TaskJsonSave; }
            set { EMU.Business.Properties.Settings.Default.TaskJsonSave = value; }
        }
        public string PathParameter4
        {
            get { return EMU.Business.Properties.Settings.Default.VppPath; }
            set { EMU.Business.Properties.Settings.Default.VppPath = value; }
        }
        public string PathName1 { get { return "图片缓存"; } }
        public string PathName2 { get { return "数据缓存"; } }
        public string PathName3 { get { return "流程缓存"; } }
        public string PathName4 { get { return "3D开发包"; } }
        public Action SkinChanged { get; set; }
        public Action ColorChanged { get; set; }
        public Action<Action> FormInvoke { get; set; }
        public Action<Action> FormBeginInvoke { get; set; }

        public RobotClient()
        {
            CameraManager.Instance.Initialise();
            for (int i = 0; i < CameraManager.Instance.Cameras.Count; i++)
            {
                ICamera camera = CameraManager.Instance.Cameras[i] as ICamera;
                camera.Initialise();
                if (camera.Name.Contains("camera_xz"))
                {
                    EMU.Business.Global.LineCameraIndex = i;
                    camera.ImageReady += (object o, CameraEventArgs ce) =>
                    {
                        imageReady("camera_xz", o, ce);
                    };
                }
                else if (camera.Name.Contains("camera_front"))
                {
                    EMU.Business.Global.FrontCameraIndex = i;
                    camera.ImageReady += (object o, CameraEventArgs ce) =>
                    {
                        imageReady("camera_front", o, ce);
                    };
                }
                else if (camera.Name.Contains("camera_back"))
                {
                    EMU.Business.Global.BackCameraIndex = i;
                    camera.ImageReady += (object o, CameraEventArgs ce) =>
                    {
                        imageReady("camera_back", o, ce);
                    };
                }
            }
            cameras = CameraManager.Instance.Cameras.ToArray();
        }

        public void InitMenu(ToolStripMenuItem[] menus) { }

        public void SavePathParameter() { EMU.Business.Properties.Settings.Default.Save(); }

        public void SaveParameterObject() { EMU.Parameter.Properties.Settings.Default.Save(); }

        public void SetPower(Address12Type type, bool open)
        {
            ModbusTCP.SetAddress12(type, open ? 1 : 0);
        }

        public void SetBaslerImageReady(Action<string, object, object> action)
        {
            imageReady = action;
        }

        public void SetBaslerCameraError(Action<string, object, object> action)
        {
            cameraError = action;
        }

        public void SetBaslerCameraStatusChanged(Action<string, object, object> action)
        {
            cameraStatusChanged = action;
        }

        public object GetParameterObject() { return EMU.Parameter.Properties.Settings.Default; }

        public PageControl[] InitPages() { return null; }

        public UserControl BaslerUserControl()
        {
            return new CameraTestControl();
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
            return new LaserControl(); 
        }

        public UserControl LidarUserControl()
        {
            return new LidarControl();
        }

        public UserControl LightUserControl()
        {
            return new LightControl();
        }

        public UserControl RgvUserControl(Color color)
        {
            return new RgvTestControl() { RgvColor = color, Dock = DockStyle.Fill };
        }

        public UserControl RobotUserControl()
        {
            return new RobotTestControl();
        }

        public UserControl ServerUserControl()
        {
            return new UploadImgControl();
        }

        public UserControl UploadImageUserControl()
        {
            return new HttpServerControl();
        }
    }
}
