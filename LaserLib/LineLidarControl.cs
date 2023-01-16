//#define test

using AnyCAD.Platform;
using AnyCAD.Presentation;
using EMU.Util;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Laser
{
    public partial class LineLidarControl : UserControl
    {
        private UdpServer udpServer;
        private PointStyle ps;
        private RenderWindow3d renderView;
        private List<string> lineData;
        private Dictionary<string, LidarData> ramData;
        private Dictionary<string, List<string>> dataDict;
        private Dictionary<double, Dictionary<int, LidarData[]>> datas;

        public LineLidarControl()
        {
            InitializeComponent();
            this.Disposed += LineLidarControl_Disposed;

            renderView = new RenderWindow3d();
            renderView.Dock = DockStyle.Fill;
            renderView.TabIndex = 1;
            renderView.ContextMenuStrip = contextMenuStrip1;
            GlobalInstance.EventListener.OnSelectElementEvent += EventListener_OnSelectElementEvent;
            panel1.Controls.Add(renderView);

            lineData = new List<string>();
            ramData = new Dictionary<string, LidarData>();
            dataDict = new Dictionary<string, List<string>>();
            datas = new Dictionary<double, Dictionary<int, LidarData[]>>();

            lineData.Add("全部数据");
            for (int i = 0; i < 16; i++)
            {
                string s = "第" + (i + 1) + "线数据";
                lineData.Add(s);
                dataDict.Add(s, new List<string>());
            }
        }

        private void LineLidarControl_Disposed(object sender, EventArgs e)
        {
            udpServer.Close();
        }

        private void LineLidarControl_Load(object sender, EventArgs e)
        {
            listBox1.DataSource = lineData;

            ps = new PointStyle();
            ps.SetMarker("plus");
            ps.SetPointSize(5);

            udpServer = new UdpServer("192.168.1.198", 2368);
            udpServer.ReciveData += UdpServer_ReciveData;

#if test
            for (int i = 0; i < 360; i++)
            {
                datas.Add(i, new Dictionary<int, LidarData[]>());
                datas[i].Add(0, new LidarData[2]);
                datas[i][0][0] = datas[i][0][1] = new LidarData() { Length = 100, Rote = i, LineNum = 0, Type = 1 };
            }
            刷新ToolStripMenuItem_Click(null, null); 
#endif
        }

        private void EventListener_OnSelectElementEvent(SelectionChangeArgs args)
        {
            listBox3.Items.Clear();
            SceneManager sceneManager = renderView.View3d.GetSceneManager();
            foreach (ElementId item in args.GetIds())
            {
                string id = item.AsInt().ToString();
                if (ramData.ContainsKey(id))
                {
                    listBox3.Items.Add((ramData[id].LineNum + 1) + " " + ramData[id].Rote + " " + ramData[id].Length);
                }
            }
        }

        private void UdpServer_ReciveData(string arg1, System.Net.EndPoint arg2)
        {
            arg1 = arg1.ToUpper();
            if (arg1.Contains("EE FF"))
            {
                arg1 = arg1.Substring(arg1.IndexOf("EE FF"));
                arg1 = arg1.Substring(36);

                int length = (16 * 4 + 2) * 3;
                string pointLabel = "近点: ";
                for (int i = 0; i < 8; i++)
                {
                    if (arg1.Length < length)
                    {
                        break;
                    }
                    string data = arg1.Substring(0, length);
                    arg1 = arg1.Substring(length);

                    string[] datas = data.Split(' ');
                    double rote = Convert.ToInt32(datas[1] + datas[0], 16) / 100;

                    int lineIndex = 0;
                    for (int j = 2; j < datas.Length; j += 4)
                    {
                        if (lineIndex >= lineData.Count)
                        {
                            break;
                        }
                        if (j + 4 >= datas.Length)
                        {
                            break;
                        }

                        int len = Convert.ToInt32(datas[j + 1] + datas[j], 16);
                        if (len > 0)
                        {
                            int type = Convert.ToInt32(datas[j + 2], 16);

                            LidarData lidarData = new LidarData();
                            lidarData.Rote = rote;
                            lidarData.Length = len;
                            lidarData.Type = type;
                            lidarData.LineNum = lineIndex;
                            if (!this.datas.ContainsKey(lidarData.Rote))
                            {
                                this.datas.Add(lidarData.Rote, new Dictionary<int, LidarData[]>());
                            }
                            if (!this.datas[lidarData.Rote].ContainsKey(lidarData.LineNum))
                            {
                                this.datas[lidarData.Rote].Add(lidarData.LineNum, new LidarData[2]);
                            }
                            if (pointLabel == "近点: ")
                            {
                                this.datas[lidarData.Rote][lidarData.LineNum][0] = lidarData;
                            }
                            else
                            {
                                this.datas[lidarData.Rote][lidarData.LineNum][1] = lidarData;
                            }
                        }
                        lineIndex++;
                    }

                    if (pointLabel == "近点: ")
                    {
                        pointLabel = "远点: ";
                    }
                    else
                    {
                        pointLabel = "近点: ";
                    }
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                listBox2.DataSource = dataDict[lineData[listBox1.SelectedIndex]];
            }
            else
            {
                List<string> allData = new List<string>();
                foreach (KeyValuePair<string, List<string>> item in dataDict)
                {
                    allData.AddRange(item.Value);
                }
                listBox2.DataSource = allData;
            }
        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataDict.Clear();
            renderView.ClearScene();
            listBox2.DataSource = null;

            foreach (KeyValuePair<double, Dictionary<int, LidarData[]>> item in datas)
            {
                foreach (KeyValuePair<int, LidarData[]> _item in item.Value)
                {
                    if (!dataDict.ContainsKey(lineData[_item.Key + 1]))
                    {
                        dataDict.Add(lineData[_item.Key + 1], new List<string>());
                    }
                    dataDict[lineData[_item.Key + 1]].Add("近点：" + item.Key + " " + _item.Value[0].Length + " " + _item.Value[0].Type);
                    dataDict[lineData[_item.Key + 1]].Add("远点：" + item.Key + " " + _item.Value[1].Length + " " + _item.Value[1].Type);
                    SetPoint(_item.Value[0], SetRam(_item.Value[0], 0));
                    SetPoint(_item.Value[1], SetRam(_item.Value[0], 1));
                }
            }

            listBox1_SelectedIndexChanged(null, null);
        }

        private void 点选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("Pick");
            点选ToolStripMenuItem.Checked = true;
            框选ToolStripMenuItem.Checked = false;
        }

        private void 框选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            renderView.ExecuteCommand("RectPick");
            点选ToolStripMenuItem.Checked = false;
            框选ToolStripMenuItem.Checked = true;
        }

        private string SetRam(LidarData data, int i)
        {
            string id = "1" + data.LineNum + data.Rote + i;
            if (ramData.ContainsKey(id))
            {
                ramData[id] = data;
            }
            else
            {
                ramData.Add(id, data);
            }
            return id;
        }

        private void SetPoint(LidarData data, string id)
        {
            double lineR = 15 - data.LineNum * 2;
            double z = Math.Sin(Math.PI / 180 * Math.Abs(lineR)) * data.Length * (lineR > 0 ? 1 : -1);
            double xy = Math.Sqrt(data.Length * data.Length - z * z);
            var _xy = GetXY(data.Rote, xy);
            double x = _xy.x;
            double y = _xy.y;

            BeginInvoke(new Action(() =>
            {
                PointNode pn = new PointNode();
                pn.SetId(ElementId.Parse(id));
                pn.SetPoint(new Vector3(x, y, z));
                pn.SetPointStyle(ps);
                renderView.ShowSceneNode(pn);
            }));
        }

        private PointD GetXY(double r, double l)
        {
            double x, y;
            x = l * Math.Cos((r + 270) * Math.PI / 180);
            y = l * Math.Sin((r + 270) * Math.PI / 180);
            return new PointD() { x = x, y = y };
        }

        private struct PointD
        {
            public double x;
            public double y;
        }
    }
}
