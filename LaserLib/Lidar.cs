using EMU.Util;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace Laser
{
    public class Lidar
    {
        private bool isData = false;
        private object lockObj = new object();
        private SerialPort serialPort;
        private StringBuilder StringBuilder = new StringBuilder();
        private List<string> list = new List<string>();
        private List<string> list1 = new List<string>();
        private Dictionary<int, int> idList = new Dictionary<int, int>();

        public Action<string> ReadValue;
        public Action<object, object, object, object> ReadList;

        public Lidar(string com, int baudRate = 115200, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            try
            {
                serialPort = new SerialPort(com, baudRate, parity, dataBits, stopBits);
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.ErrorReceived += SerialPort_ErrorReceived;
                serialPort.PinChanged += SerialPort_PinChanged;
                serialPort.Open();
            }
            catch (Exception e)
            {
                e.Message.AddLog(EMU.Parameter.LogType.ErrorLog);
            }
        }

        public void Close()
        {
            try
            {
                serialPort.Close();
            }
            catch (Exception e)
            {
                e.Message.AddLog(EMU.Parameter.LogType.ErrorLog);
            }
        }

        public string Start()
        {
            list.Clear();
            list1.Clear();
            idList.Clear();
            StringBuilder.Clear();

            byte[] bytes = "A5 50".HexStringToByteArray();
            serialPort.Write(bytes, 0, bytes.Length);
            bytes = "A5 F0 02 94 02 C1".HexStringToByteArray();
            serialPort.Write(bytes, 0, bytes.Length);
            bytes = "A5 20".HexStringToByteArray();
            serialPort.Write(bytes, 0, bytes.Length);
            return "A5 50\r\nA5 F0 02 94 02 C1\r\nA5 20";
        }

        public string Stop()
        {
            byte[] bytes = "A5 F0 02 00 00 57".HexStringToByteArray();
            serialPort.Write(bytes, 0, bytes.Length);
            return "A5 F0 02 00 00 57";
        }

        public bool IsConnect()
        {
            return serialPort.IsOpen;
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[serialPort.BytesToRead];
            int length = serialPort.Read(buffer, 0, buffer.Length);
            if (length > 0)
            {
                string value = "";
                for (int i = 0; i < buffer.Length; i++)
                {
                    value += Convert.ToString(buffer[i], 16) + " ";
                }

                StringBuilder.Append(value);
                ReadValue?.Invoke(value);

                value = StringBuilder.ToString();
                if (value.Contains("a5 5a 5 0 0 40 81 "))
                {
                    isData = true;
                    StringBuilder.Clear();
                    StringBuilder.Append(value.Substring(value.IndexOf("a5 5a 5 0 0 40 81 ") + 18));
                }

                if (isData)
                {
                    string[] vs = StringBuilder.ToString().Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    if (vs.Length >= 5)
                    {
                        int len = vs.Length % 5;
                        StringBuilder.Clear();
                        for (int i = vs.Length - len; i < vs.Length; i++)
                        {
                            StringBuilder.Append(vs[i] + " ");
                        }

                        Task.Run(() =>
                        {
                            int index = -1;
                            List<float> listR = new List<float>();
                            List<string> tmpList = new List<string>();
                            for (int i = 0; i < 4; i++)
                            {
                                if (Convert.ToInt32(vs[i], 16) % 2 == 0 &&
                                    Convert.ToInt32(vs[i + 1], 16) % 2 == 1)
                                {
                                    index = i;
                                    break;
                                }
                            }
                            if (index < 0)
                            {
                                return;
                            }

                            for (int i = index; i < vs.Length - 5; i += 5)
                            {
                                if (vs[i + 3] != "0" && vs[i + 4] != "0")
                                {
                                    #region 角度
                                    int _i = Convert.ToInt32(vs[i + 1], 16);
                                    string _s = Convert.ToString(_i, 2);
                                    _s = new string('0', 8 - _s.Length) + _s;
                                    _s = _s.Substring(0, 7);
                                    string tmp = Convert.ToString(Convert.ToInt32(vs[i + 2], 16), 2);
                                    tmp = new string('0', 8 - tmp.Length) + tmp;
                                    _s = tmp + _s;
                                    int i1 = Convert.ToInt32(_s.Substring(0, 9), 2), i2 = Convert.ToInt32(_s.Substring(9), 2);
                                    float r = i1 + (float)i2 / 60;
                                    #endregion
                                    #region 距离
                                    _i = Convert.ToInt32(vs[i + 4] + vs[i + 3], 16);
                                    _s = Convert.ToString(_i, 2);
                                    _s = _s.Substring(0, _s.Length - 2);
                                    int l = Convert.ToInt32(_s, 2);
                                    #endregion
                                    lock (lockObj)
                                    {
                                        int id = i1 * 100 + i2;
                                        if (idList.ContainsKey(id) && idList.Count > 0)
                                        {
                                            try
                                            {
                                                list[idList[id]] = id.ToString("00000") + ": " + vs[i] + " " + vs[i + 1] + " " + vs[i + 2] + " " + vs[i + 3] + " " + vs[i + 4];
                                                list1[idList[id]] = id.ToString("00000") + ">> " + i1 + "°" + i2 + "′" + l + " 毫米";
                                            }
                                            catch (Exception)
                                            {
                                                idList.Add(id, list.Count);
                                                list.Add(id.ToString("00000") + ": " + vs[i] + " " + vs[i + 1] + " " + vs[i + 2] + " " + vs[i + 3] + " " + vs[i + 4]);
                                                list1.Add(id.ToString("00000") + ">> " + i1 + "°" + i2 + "′" + l + " 毫米");
                                            }
                                        }
                                        else
                                        {
                                            idList.Add(id, list.Count);
                                            list.Add(id.ToString("00000") + ": " + vs[i] + " " + vs[i + 1] + " " + vs[i + 2] + " " + vs[i + 3] + " " + vs[i + 4]);
                                            list1.Add(id.ToString("00000") + ">> " + i1 + "°" + i2 + "′" + l + " 毫米");
                                        }
                                        try
                                        {
                                            listR.Add(r);
                                            tmpList.Add(list1[idList[id]]);
                                        }
                                        catch (Exception)
                                        {
                                            return;
                                        }
                                    }
                                }
                            }

                            ReadList?.Invoke(list, list1, listR, tmpList);
                        });
                    }
                }
            }
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        private void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {

        }
    }
}
