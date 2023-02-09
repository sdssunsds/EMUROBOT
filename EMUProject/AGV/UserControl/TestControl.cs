#define mapFlip  // 地图翻转

using EMU.Interface;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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

        private Bitmap map = null;
        private TcpServiceSocket socket = null;
        private List<AGVModel> models = null;
        private List<PointLocation> locations = null;
        private List<SortPointLocation> sortLocations = null;
        private Dictionary<string, int> agvOutTimeDict = null;
        private Dictionary<string, StringBuilder> messageDict = null;

        public IProject Project { get; set; }

        public Action<string> AddLog { get; set; }

        public TestControl()
        {
            InitializeComponent();
            pb_map.MouseWheel += Pb_map_MouseWheel;
            models = new List<AGVModel>();
            locations = new List<PointLocation>();
            agvOutTimeDict = new Dictionary<string, int>();
            messageDict = new Dictionary<string, StringBuilder>();

            ThreadManager.TaskRun((ThreadEventArgs args) =>
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
                                item.IsLink = false;
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
            try
            {
                Graphics mapGraphics = Graphics.FromImage(bitmap);
                mapGraphics.DrawImage(map, new Point(0, 0));
                Brush brush = new SolidBrush(Color.DarkOrange);
                Pen pen = new Pen(Color.DarkOrange);
                foreach (PointLocation location in locations)
                {
                    Size size = TextRenderer.MeasureText(location.Name, this.Font);
                    using (Bitmap strMap = new Bitmap(size.Width, size.Height))
                    {
                        using (Graphics strGraphics = Graphics.FromImage(strMap))
                        {
                            strGraphics.DrawString(location.Name, this.Font, brush, 0, 0);
                        }
#if mapFlip
                        strMap.RotateFlip(RotateFlipType.RotateNoneFlipY);
#endif
                        mapGraphics.DrawImage(strMap, location.Point.X - size.Width / 2, location.Point.Y + 5); 
                    }
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
#if mapFlip
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipY);
#endif
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
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

        private void dgv_car_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void dgv_car_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv_car.SelectedRows.Count > 0)
            {
                selected = dgv_car.SelectedRows[0].Index;
            }
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
#if mapFlip
                y = mapHeight - y;
#endif
                ShowWriteLocation(x, y, 0f, "");
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
                int x = pb_map.Location.X;
                int y = pb_map.Location.Y;
                x += e.X - upLocation.X;
                y += e.Y - upLocation.Y;
                pb_map.Location = new Point(x, y);
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
            }
            else
            {
                mapZoom += 0.02;
            }
            if (mapZoom <= 0)
            {
                mapZoom = 0.02;
            }
            ZoomMap();
        }

        private void 保存点位数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (locations.Count == 0)
            {
                MessageBox.Show("没有可以保存的位置信息");
                return;
            }
            try
            {
                if (Project.dataBase.SaveTs<PointLocation>(locations, sortLocations))
                {
                    MessageBox.Show("保存成功");
                }
                else
                {
                    MessageBox.Show("保存失败");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("保存失败");
            }
        }

        private void 加载点位数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            flp.Controls.Clear();
            locations = Project.dataBase.GetTs<PointLocation>();
            sortLocations = Project.dataBase.GetTs<SortPointLocation>();
            if (locations == null)
            {
                locations = new List<PointLocation>();
            }
            foreach (PointLocation item in locations)
            {
                AddLocationInfo(item);
            }
        }

        private void 记录当前AGV位置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selected >= 0 && selected < models.Count)
            {
                ShowWriteLocation(models[selected].X, models[selected].Y, models[selected].弧度, ""); 
            }
        }

        private void 执行一轮ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunSortLocations(false);
        }

        private void 重复执行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunSortLocations(true);
        }

        private void 配置批量点位ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunAgvLocationForm runAgvLocationForm = new RunAgvLocationForm();
            runAgvLocationForm.Locations = locations;
            runAgvLocationForm.SortLocations = sortLocations;
            runAgvLocationForm.ShowDialog(this);
            sortLocations = runAgvLocationForm.SortLocations;
        }

        private void 加载地图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "地图文件（pgm）|*.pgm";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                map = Extend.PGM2BitMap(path);
#if mapFlip
                map.RotateFlip(RotateFlipType.RotateNoneFlipY);
#endif
                mapWidth = map.Width;
                mapHeight = map.Height;
                pb_map.Size = new Size(mapWidth, mapHeight);
                pb_map.Location = new Point(721 - mapWidth / 2 + groupBox2.Width / 2, 80 - mapHeight / 2 + groupBox2.Height / 2);
                groupBox4.Visible = false;
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
            MoveLocation(location);
        }

        private void Update_Location_Click(object sender, EventArgs e)
        {
            PointLocation location = (sender as Button).Tag as PointLocation;
            ShowWriteLocation(location.Point.X, location.Point.Y, location.Turn, location.Name);
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
                        model.OriginalLocation.X = float.Parse(datas[3]);
                        model.OriginalLocation.Y = float.Parse(datas[4]);
                        int x = (int)(model.OriginalLocation.X * 20) + 1000;
                        int y =
#if mapFlip
                            (int)(model.OriginalLocation.Y * 20) + 1000;
#else
                            ((int)(model.OriginalLocation.Y * 20) + 1000);
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
            SocketSend(cmd, selected, par);
        }

        private void SocketSend(Cmd cmd, int selected, string par)
        {
            Socket client = socket.clientSockets.Find(s => s.Connected && s.RemoteEndPoint.ToString() == models[selected].IP);
            if (client != null && client.Connected)
            {
                try
                {
                    socket.SendAsync(client, "<start/" + cmd + ":" + par + "/end>");
                    AddLog?.Invoke("<start/" + cmd + ":" + par + "/end>");
                }
                catch (Exception e)
                {
                    AddLog?.Invoke(e.Message);
                }
            }
            else
            {
                AddLog?.Invoke("发送失败");
            }
        }

        private void AddLocationInfo(PointLocation location)
        {
            GroupBox group = new GroupBox() { Text = location.Name, Size = new Size(280, 40), Tag = location };
            Button[] buttons =
            {
                    new Button(){ Text = "运动到点", Size = new Size(100,25), Location = new Point(5, 11), Tag = location },
                    new Button(){ Text = "修改点", Size = new Size(80,25), Location = new Point(110, 11), Tag = location },
                    new Button(){ Text = "删除点", Size = new Size(80, 25), Location = new Point(195, 11), Tag = location }
            };
            buttons[0].Click += Move_Location_Click;
            buttons[1].Click += Update_Location_Click;
            buttons[2].Click += Delete_Location_Click;
            group.Controls.AddRange(buttons);
            flp.Controls.Add(group);
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
                    IP = ip,
                    IsLink = true
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
                        dgv_car.Columns[dgv_car.Columns.Count - 1].Visible = false;
                        dgv_car.Columns[dgv_car.Columns.Count - 2].Visible = false;
                    }
                    dgv_car.Refresh();
                    if (selected < dgv_car.Rows.Count)
                    {
                        dgv_car.Rows[selected].Cells[0].Selected = true;
                    }
                }));
            }
            catch (Exception) { }
        }

        private void MoveLocation(PointLocation location)
        {
            float x = (float)(location.Point.X - 1000) / 20;
#if mapFlip
            float y = (float)(location.Point.Y - 1000) / 20;
#else
            float y = (float)(1983 - location.Point.Y - 1000) / 20;
#endif
            SocketSend(Cmd.move_point, x, y, location.Turn);
        }

        private void RunSortLocations(bool isRepeat)
        {
            if (sortLocations != null)
            {
                int index = selected;
                AGVModel model = models[index];
                ThreadManager.TaskRun((ThreadEventArgs args) =>
                {
                    do
                    {
                        foreach (SortPointLocation item in sortLocations)
                        {
                            AddLog?.Invoke("前往" + item.Name + "点");
                            DateTime start = DateTime.Now;
                            MoveLocation(item);
                            while (model.导航状态 != "导航中")
                            {
                                if (!model.IsLink)
                                {
                                    MessageBox.Show("AGV丢失[" + model.IP + "]");
                                    return;
                                }
                                Thread.Sleep(50);
                            }
                            while (model.导航状态 == "导航中")
                            {
                                if (!model.IsLink)
                                {
                                    MessageBox.Show("AGV丢失[" + model.IP + "]");
                                    return;
                                }
                                Thread.Sleep(50);
                            }
                            if (model.导航状态 == "导航异常" || model.导航状态 == "取消导航")
                            {
                                if (!model.IsLink)
                                {
                                    MessageBox.Show("AGV丢失[" + model.IP + "]");
                                    return;
                                }
                                return;
                            }
                            DateTime end = DateTime.Now;
                            AddLog?.Invoke("完成 " + item.Name + " 点，用时：" + (end - start).TotalSeconds + "秒");
                            double mistake = Math.Sqrt(Math.Pow(model.OriginalLocation.X - (item.Point.X - 1000) / 20f, 2) + Math.Pow(model.OriginalLocation.Y - (item.Point.Y - 1000) / 20f, 2));
                            AddLog?.Invoke("误差：" + mistake + "米");
                            AddLog?.Invoke("等待 " + item.Internal + " 毫秒开始下一个导航");
                            Thread.Sleep(item.Internal);
                        } 
                    } while (isRepeat);
                });
            }
            else
            {
                MessageBox.Show("未配置批量执行的点位数据");
            }
        }

        private void ShowWriteLocation(int x, int y, float turn, string name)
        {
            SetAgvPointForm pointForm = new SetAgvPointForm();
#if mapFlip
            pointForm.Flip = true;
#endif
            pointForm.NameValue = name;
            pointForm.TurnValue = turn;
            if (pointForm.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            if (string.IsNullOrEmpty(pointForm.NameValue))
            {
                return;
            }
            PointLocation location = locations.Find(l => l.Point.X == x && l.Point.Y == y);
            if (location == null)
            {
                location = new PointLocation()
                {
                    Name = pointForm.NameValue,
                    Turn = pointForm.TurnValue,
                    Point = new Point(x, y)
                };
                locations.Add(location);
                AddLocationInfo(location);
            }
            else
            {
                foreach (Control item in flp.Controls)
                {
                    if (item is GroupBox && (item.Tag as PointLocation).Name == location.Name)
                    {
                        item.Text = pointForm.NameValue;
                    }
                }
                location.Name = pointForm.NameValue;
                location.Turn = pointForm.TurnValue;
            }
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
    }

    public class AGVModel
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

        public bool IsLink { get; set; }
        public OriginalLocation OriginalLocation { get; set; }
        public AGVModel()
        {
            OriginalLocation = new OriginalLocation();
        }
    }

    public class OriginalLocation
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class PointLocation
    {
        public string Name { get; set; }
        public float Turn { get; set; }
        public Point Point { get; set; }
    }

    public enum Alarm
    {
        无报警 = 0
    }

    public enum Cmd
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

    public enum Navigation
    {
        未导航 = 0, 导航中, 导航完成, 导航异常, 取消导航
    }

    public enum Status
    {
        待机 = 0, 前进, 后退, 左转, 右转, 停车, 导航
    }
}
