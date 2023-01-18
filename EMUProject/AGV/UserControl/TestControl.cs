#define mapFlip  // 地图翻转

using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project.AGV
{
    public partial class TestControl : UserControl
    {
        private bool isMapClick = false;
        private int mapWidth = 0;
        private int mapHeight = 0;
        private int selected = 0;
        private double mapZoom = 1d;
        private object outTimeLock = new object();

        private Point lastLocation = new Point(0, 0);
        private Point upLocation = new Point(0, 0);
        private Point upMoving = new Point(0, 0);

        private Bitmap map = null;
        private TcpServiceSocket socket = null;
        private List<AGVModel> models = null;
        private List<PointLocation> locations = null;
        private Dictionary<string, int> agvOutTimeDict = null;
        private Dictionary<string, StringBuilder> messageDict = null;

        public Action<string> SocketRecvMessage { get; set; }

        public TestControl()
        {
            InitializeComponent();
            pb_map.MouseWheel += Pb_map_MouseWheel;
            models = new List<AGVModel>();
            locations = new List<PointLocation>();
            agvOutTimeDict = new Dictionary<string, int>();
            messageDict = new Dictionary<string, StringBuilder>();

            Task.Run(() =>
            {
                List<AGVModel> removeList = new List<AGVModel>();
                while (true)
                {
                    Thread.Sleep(1000);
                    removeList.Clear();
                    foreach (AGVModel item in models)
                    {
                        if (agvOutTimeDict.ContainsKey(item.IP))
                        {
                            int outTime = 0;
                            lock (outTimeLock)
                            {
                                agvOutTimeDict[item.IP]++;
                                outTime = agvOutTimeDict[item.IP];
                            }

                            if (outTime > 5)
                            {
                                lock (outTimeLock)
                                {
                                    agvOutTimeDict.Remove(item.IP);
                                }
                                if (messageDict.ContainsKey(item.IP))
                                {
                                    messageDict.Remove(item.IP);
                                }
                                removeList.Add(item);
                            }
                        }
                    }

                    if (removeList.Count > 0)
                    {
                        new Thread(new ThreadStart(() =>
                        {
                            foreach (AGVModel item in removeList)
                            {
                                models.Remove(item);
                            }
                            BindingAgvModels();
                        })).Start();
                    }
                }
            });
        }

        public void RefreshMap()
        {
            if (map == null)
            {
                return;
            }
            Bitmap bitmap = new Bitmap(mapWidth, mapHeight);
            Graphics mapGraphics = Graphics.FromImage(bitmap);
            mapGraphics.DrawImage(map, new Point(0, 0));
            Brush brush = new SolidBrush(Color.Gold);
            Pen pen = new Pen(Color.Gold);
            foreach (PointLocation location in locations)
            {
                Size size = TextRenderer.MeasureText(location.Name, this.Font);
                mapGraphics.DrawString(location.Name, this.Font, brush, location.Point.X - size.Width / 2, location.Point.Y - 15);
                mapGraphics.FillRectangle(brush, location.Point.X - 1, location.Point.Y - 1, 3, 3);
                double d = Extend.GetAngle(location.Turn);
                mapGraphics.DrawLine(pen, location.Point, Extend.GetRadianLineEnd(Extend.GetRadian(d), 4, location.Point));
            }
            pen.Color = Color.Blue;
            foreach (AGVModel item in models)
            {
                // w:14 l:19
                mapGraphics.DrawEllipse(pen, item.X - 9, item.Y - 9, 19, 19);
                Point origin = new Point(item.X, item.Y);
                double d = Extend.GetAngle(item.弧度);
                mapGraphics.DrawLine(pen, origin, Extend.GetRadianLineEnd(Extend.GetRadian(d), 9, origin));
            }
            try
            {
                BeginInvoke(new Action(() =>
                {
                    pb_map.Image = bitmap;
                }));
            }
            catch (Exception) { }
        }

        private void btn_start_server_Click(object sender, EventArgs e)
        {
            try
            {
                socket = new TcpServiceSocket(tb_ip.Text, int.Parse(tb_port.Text), 50);
                socket.accpetInfoEvent = SocketAccpetInfoEvent;
                socket.recvMessageEvent = SocketRecvMessageEvent;
                socket.Start();
                btn_start_server.Enabled = false;
                btn_close_server.Enabled = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_close_server_Click(object sender, EventArgs e)
        {
            try
            {
                socket.CloseAllClientSocket();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                models.Clear();
                messageDict.Clear();
                lock (outTimeLock)
                {
                    agvOutTimeDict.Clear();
                }
                dgv_car.DataSource = null;
                btn_start_server.Enabled = true;
                btn_close_server.Enabled = false;
            }
        }

        private void btn_forward_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.forward);
        }

        private void btn_backward_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.backoff);
        }

        private void btn_left_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.turnleft);
        }

        private void btn_right_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.turnright);
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.carstop);
        }

        private void btn_cancel_move_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.cancel_move);
        }

        private void btn_setting_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.set_x_speed, tb_speed.Text);
            Thread.Sleep(500);
            SocketSend(Cmd.set_w_speed, tb_turn.Text);
        }

        private void btn_shutdown_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.shutdown);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            SocketSend(Cmd.hardware_close);
        }

        private void dgv_car_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            selected = e.RowIndex;
        }

        private void dgv_car_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void tb_speed_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(tb_speed.Text, out _))
            {
                tb_speed.Text = "0";
                MessageBox.Show("设置的不是有效速度");
            }
            int speed = int.Parse(tb_speed.Text);
            if (Math.Abs(speed) > 5000)
            {
                tb_speed.Text = (speed < 0 ? "-" : "") + "5000";
            }
        }

        private void tb_turn_TextChanged(object sender, EventArgs e)
        {
            if (!float.TryParse(tb_turn.Text, out _))
            {
                tb_speed.Text = "0";
                MessageBox.Show("设置的不是有效弧度");
            }
            float turn = float.Parse(tb_turn.Text);
            if (Math.Abs(turn) > 100)
            {
                tb_turn.Text = (turn < 0 ? "-" : "") + "100";
            }
        }

        private void pb_map_LocationChanged(object sender, EventArgs e)
        {
            int width = (pb_map.Width - mapWidth) / 2;
            int height = (pb_map.Height - mapHeight) / 2;
            lastLocation = new Point(pb_map.Location.X + width, pb_map.Location.Y + height);
        }

        private void pb_map_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                int x = (int)(e.X / mapZoom);
                int y = (int)(e.Y / mapZoom);
                SetAgvPointForm pointForm = new SetAgvPointForm();
                if (pointForm.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }
                if (string.IsNullOrEmpty(pointForm.NameValue))
                {
                    return;
                }
                PointLocation location = new PointLocation()
                {
                    Name = pointForm.NameValue,
                    Turn = pointForm.TurnValue,
                    Point = new Point(x, y)
                };
                locations.Add(location);

                GroupBox group = new GroupBox() { Text = location.Name, Size = new Size(200, 40), Tag = location };
                Button[] buttons =
                {
                    new Button(){ Text = "运动到点", Size = new Size(100,25), Location = new Point(3, 11), Tag = location },
                    new Button(){ Text = "删除点", Size = new Size(80, 25), Location = new Point(110, 11), Tag = location }
                };
                buttons[0].Click += Move_Location_Click;
                buttons[1].Click += Delete_Location_Click;
                group.Controls.AddRange(buttons);
                flp.Controls.Add(group);
            }
        }

        private void pb_map_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMapClick = true;
                upLocation = e.Location;
            }
        }

        private void pb_map_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMapClick)
            {
                int moveX = e.Location.X - upLocation.X;
                int moveY = e.Location.Y - upLocation.Y;
                if (Math.Abs(upMoving.X - Math.Abs(moveX)) > 0 && Math.Abs(upMoving.Y - Math.Abs(moveY)) > 0)
                {
                    upMoving = new Point(Math.Abs(moveX), Math.Abs(moveY));
                    pb_map.Location = new Point(pb_map.Location.X + moveX, pb_map.Location.Y + moveY);
                    upLocation = e.Location;
                }
            }
        }

        private void pb_map_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (isMapClick)
                {
                    isMapClick = false;
                }
            }
        }

        private void Pb_map_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                mapZoom -= 0.02;
                ZoomMap();
            }
            else
            {
                mapZoom += 0.02;
                ZoomMap();
            }
        }

        private void 加载地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "地图文件（pgm）|*.pgm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                map = Extend.PGM2BitMap(path);
                map.RotateFlip(RotateFlipType.RotateNoneFlipY);
                mapWidth = map.Width;
                mapHeight = map.Height;
                pb_map.Size = new Size(mapWidth, mapHeight);
                pb_map.Location = new Point(0 - mapWidth / 2 + groupBox4.Width / 2, 0 - mapHeight / 2 + groupBox4.Height / 2);
                mapZoom = 1d;
            }
        }

        private void 清空点位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            locations.Clear();
            flp.Controls.Clear();
        }

        private void 还原缩放ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapZoom = 1d;
            ZoomMap();
        }

        private void Move_Location_Click(object sender, EventArgs e)
        {
            PointLocation location = (sender as Button).Tag as PointLocation;
            float x = (float)(location.Point.X - 1000) / 20;
#if mapFlip
            float y = (float)(location.Point.Y - 1000) / 20;
#else
            float y = (float)(1983 - location.Point.Y - 1000) / 20;
#endif
            SocketSend(Cmd.move_point, x, y, location.Turn);
        }

        private void Delete_Location_Click(object sender, EventArgs e)
        {
            Control control = sender as Control;
            PointLocation location = control.Tag as PointLocation;
            locations.Remove(location);
            flp.Controls.Remove(control.Parent);
        }

        private void SocketAccpetInfoEvent(Socket socket)
        {
            string ip = socket.RemoteEndPoint.ToString();
            AddSocket(ip);
        }

        private void SocketRecvMessageEvent(Socket socket, string msg)
        {
            SocketRecvMessage?.Invoke(msg);
            string ip = socket.RemoteEndPoint.ToString();
            AddSocket(ip);
            if (messageDict.ContainsKey(ip))
            {
                messageDict[ip].Append(msg);
                msg = messageDict[ip].ToString();
                int start = msg.IndexOf("<start/");
                int end = msg.IndexOf("/end>");
                if (start >= 0 && end > start)
                {
                    msg = msg.Substring(start + 7, end - start - 7);
                    string[] datas = msg.Split(',');
                    if (datas.Length == 11)
                    {
                        messageDict[ip].Clear();
                        AGVModel model = models.Find(a => a.IP == ip);
                        model.编号 = datas[0];
                        model.行驶速度 = (int)(float.Parse(datas[1]) * 1000);
                        model.转向速度 = float.Parse(datas[2]);
                        model.弧度 = float.Parse(datas[5]);
                        int x = (int)(float.Parse(datas[3]) * 20) + 1000;
                        int y =
#if mapFlip
                            (int)(float.Parse(datas[4]) * 20) + 1000;
#else
                            ((int)(float.Parse(datas[4]) * 20) + 1000);
#endif
                        bool refreshLocation = !(model.Y == y && model.X < x) ||
                            (Extend.GetRadian(45) > model.弧度 && Extend.GetRadian(315) < model.弧度);
                        if (refreshLocation)
                        {
                            model.X = x;
                            model.Y = y;
                        }
                        model.导航状态 = ((Navigation)(int.Parse(datas[6]))).ToString();
                        model.报警 = ((Alarm)(int.Parse(datas[7]))).ToString();
                        model.机器人状态 = ((Status)(int.Parse(datas[8]))).ToString();
                        model.电量 = int.Parse(datas[9]);
                        model.电压 = float.Parse(datas[10]);
                        if (agvOutTimeDict.ContainsKey(ip))
                        {
                            lock (outTimeLock)
                            {
                                agvOutTimeDict[ip] = 0;
                            } 
                        }
                        BindingAgvModels();
                    }
                }
            }
        }

        private void SocketSend(Cmd cmd, params object[] pars)
        {
            string par = "null";
            if (pars != null && pars.Length > 0)
            {
                par = "";
                for (int i = 0; i < pars.Length; i++)
                {
                    if (i > 0)
                    {
                        par += ",";
                    }
                    par += pars[i];
                }
            }
            for (int i = 0; i < socket.clientSockets.Count; i++)
            {
                if (!socket.clientSockets[i].Connected)
                {
                    socket.clientSockets.RemoveAt(i);
                    i--;
                }
            }
            Socket client = socket.clientSockets.Find(s => s.Connected && s.RemoteEndPoint.ToString() == models[selected].IP);
            if (client != null && client.Connected)
            {
                try
                {
                    socket.SendAsync(client, "<start/" + cmd + ":" + par + "/end>");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("发送失败");
            }
        }

        private void AddSocket(string ip)
        {
            if (!messageDict.ContainsKey(ip))
            {
                lock (outTimeLock)
                {
                    agvOutTimeDict.Add(ip, 0);
                }
                messageDict.Add(ip, new StringBuilder());
                models.Add(new AGVModel()
                {
                    IP = ip
                });
                BindingAgvModels();
            }
        }

        private void BindingAgvModels()
        {
            try
            {
                Invoke(new Action(() =>
                {
                    if (dgv_car.DataSource == null)
                    {
                        dgv_car.DataSource = models;
                    }
                    dgv_car.Refresh();
                    if (selected < models.Count)
                    {
                        dgv_car.Rows[selected].Cells[0].Selected = true;
                    }
                }));
            }
            catch (Exception) { }
        }

        private void ZoomMap()
        {
            if (map != null)
            {
                pb_map.Size = new Size((int)(mapWidth * mapZoom), (int)(mapHeight * mapZoom));
                int moveX = Math.Abs(pb_map.Width - mapWidth) / 2;
                int moveY = Math.Abs(pb_map.Height - mapHeight) / 2;
                pb_map.LocationChanged -= pb_map_LocationChanged;
                pb_map.Location = new Point(lastLocation.X + (mapZoom > 1 ? 0 - moveX : moveX), lastLocation.Y + (mapZoom > 1 ? 0 - moveY : moveY));
                pb_map.LocationChanged += pb_map_LocationChanged;
                pb_map.Refresh(); 
            }
        }

        private class AGVModel
        {
            public string 编号 { get; set; }
            public string IP { get; set; }
            public int 行驶速度 { get; set; }
            public float 转向速度 { get; set; }
            public int X { get; set; }
            public int Y { get; set; }
            public float 弧度 { get; set; }
            public string 导航状态 { get; set; }
            public string 报警 { get; set; }
            public string 机器人状态 { get; set; }
            public int 电量 { get; set; }
            public float 电压 { get; set; }
        }

        private class PointLocation
        {
            public string Name { get; set; }
            public float Turn { get; set; }
            public Point Point { get; set; }
        }

        private enum Alarm
        {
            无报警 = 0
        }

        private enum Cmd
        {
            forward,
            backoff,
            turnleft,
            turnright,
            carstop,
            set_x_speed,
            set_w_speed,
            shutdown,
            hardware_close,
            move_point,
            cancel_move
        }

        private enum Navigation
        {
            未导航 = 0, 导航中, 导航完成, 导航异常, 取消导航
        }

        private enum Status
        {
            待机 = 0, 前进, 后退, 左转, 右转, 停车, 导航
        }
    }
}
