using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using UploadImageServer;

namespace EMU.Interface
{
    public interface IProject
    {
        IAppServer appServer { get; set; }
        ICameraControl[] cameras { get; set; }
        IDataBase dataBase { get; set; }
        ILaserControl laser { get; set; }
        ILightControl light { get; set; }
        IRgvControl rgv { get; set; }
        IRobotControl robot { get; set; }
        IService upload { get; set; }
        IHomePage homePage { get; set; }
        string PathParameter1 { get; set; }
        string PathParameter2 { get; set; }
        string PathParameter3 { get; set; }
        string PathParameter4 { get; set; }
        string PathName1 { get; }
        string PathName2 { get; }
        string PathName3 { get; }
        string PathName4 { get; }
        Action SkinChanged { get; set; }
        Action ColorChanged { get; set; }
        Action<Action> FormInvoke { get; set; }
        Action<Action> FormBeginInvoke { get; set; }
        void InitMenu(ToolStripMenuItem[] menus);
        void SavePathParameter();
        void SaveParameterObject();
        void SetPower(Address12Type type, bool open);
        void SetBaslerImageReady(Action<string, object, object> action);
        void SetBaslerCameraError(Action<string, object, object> action);
        void SetBaslerCameraStatusChanged(Action<string, object, object> action);
        object GetParameterObject();
        PageControl[] InitPages();
        UserControl BaslerUserControl();
        UserControl CognexUserControl();
        UserControl HikUserControl();
        UserControl LaserUserControl();
        UserControl LidarUserControl();
        UserControl LightUserControl();
        UserControl RgvUserControl(Color color);
        UserControl RobotUserControl();
        UserControl UploadImageUserControl();
        UserControl ServerUserControl();
    }
    public class PageControl
    {
        public string Name { get; set; }
        public UserControl MainControl { get; set; }
    }
}
