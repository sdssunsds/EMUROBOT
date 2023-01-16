using EMU.Util;
using System;
using System.Collections.Generic;

namespace Laser
{
    public class LineLidar
    {
        private UdpServer udpServer;
        private List<string> lineData;
        public Dictionary<double, Dictionary<int, LidarData[]>> datas { get; set; }

        public LineLidar(string ipPort, List<string> lineData)
        {
            this.lineData = lineData;
            datas = new Dictionary<double, Dictionary<int, LidarData[]>>();

            string[] vs = ipPort.Split(':');
            udpServer = new UdpServer(vs[0], int.Parse(vs[1]));
            udpServer.ReciveData += UdpServer_ReciveData;
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
    }
}
