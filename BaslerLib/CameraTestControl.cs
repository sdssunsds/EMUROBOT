using EMU.Parameter;
using EMU.Util;
using System;
using System.Windows.Forms;

namespace Basler
{
    public partial class CameraTestControl : UserControl
    {
        public CameraTestControl()
        {
            InitializeComponent();
            LogManager.AddLogEvent += LogManager_AddLogEvent;
        }

        private void CameraTestControl_Load(object sender, EventArgs e)
        {
            CameraManager.Instance.Initialise();
            foreach (ICamera camera in CameraManager.Instance.Cameras)
            {
                camera.Initialise();
                camera.ImageReady += Camera_ImageReady;
                camera.CameraError += Camera_CameraError;
                camera.CameraStatusChanged += Camera_CameraStatusChanged;
                listView1.Items.Add(new ListViewItem()
                {
                    Text = camera.Name,
                    Tag = camera,
                    ImageIndex = camera.Opened ? 0 : 1
                });
            }
        }

        private void btn_open_Click(object sender, EventArgs e)
        {
            try
            {
                ICamera camera = listView1.SelectedItems[0].Tag as ICamera;
                camera.Open();
                listView1.SelectedItems[0].ImageIndex = camera.Opened ? 0 : 1;
                string.Format("相机{0}已打开", camera.Name).AddLog(LogType.CameraLog);
            }
            catch (Exception ex)
            {
                ex.Message.AddLog(LogType.ErrorLog);
            }
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            try
            {
                ICamera camera = listView1.SelectedItems[0].Tag as ICamera;
                camera.Close();
                listView1.SelectedItems[0].ImageIndex = camera.Opened ? 0 : 1;
                string.Format("相机{0}已关闭", camera.Name).AddLog(LogType.CameraLog);
            }
            catch (Exception ex)
            {
                ex.Message.AddLog(LogType.ErrorLog);
            }
        }

        private void btn_oneshot_Click(object sender, EventArgs e)
        {
            try
            {
                ICamera camera = listView1.SelectedItems[0].Tag as ICamera;
                camera.OneShot();
                string.Format("相机{0}已拍摄", camera.Name).AddLog(LogType.CameraLog);
            }
            catch (Exception ex)
            {
                ex.Message.AddLog(LogType.ErrorLog);
            }
        }

        private void btn_continuousshot_Click(object sender, EventArgs e)
        {
            try
            {
                ICamera camera = listView1.SelectedItems[0].Tag as ICamera;
                camera.ContinuousShot();
                string.Format("相机{0}开始连续拍图", camera.Name).AddLog(LogType.CameraLog);
            }
            catch (Exception ex)
            {
                ex.Message.AddLog(LogType.ErrorLog);
            }
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            try
            {
                ICamera camera = listView1.SelectedItems[0].Tag as ICamera;
                camera.Stop();
                string.Format("相机{0}停止连续拍图", camera.Name).AddLog(LogType.CameraLog);
            }
            catch (Exception ex)
            {
                ex.Message.AddLog(LogType.ErrorLog);
            }
        }

        private void LogManager_AddLogEvent(string arg1, LogType arg2)
        {
            BeginInvoke(new Action(() =>
            {
                tb_log.Text += arg1 + "\r\n";
                tb_log.SelectionStart = tb_log.Text.Length - 1;
            }));
        }

        private void Camera_ImageReady(object sender, CameraEventArgs e)
        {
            BaslerCamera camera = sender as BaslerCamera;
            if (camera.Name.ToLower().Contains("camera_xz"))
            {
                cameraControl1.SetImage(e.Image.Bitmap, CameraName.Line);
            }
            else if (camera.Name.ToLower().Contains("camera_front"))
            {
                cameraControl1.SetImage(e.Image.Bitmap, CameraName.Front);
            }
            else if (camera.Name.ToLower().Contains("camera_back"))
            {
                cameraControl1.SetImage(e.Image.Bitmap, CameraName.Back);
            }
        }

        private void Camera_CameraError(object sender, CameraErrorEventArgs e)
        {
            e.Message.AddLog(LogType.ErrorLog);
        }

        private void Camera_CameraStatusChanged(object sender, CameraStatusEventArgs e)
        {
            e.Status.ToString().AddLog(LogType.CameraLog);
        }
    }
}
