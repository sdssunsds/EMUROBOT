using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using System;
using System.IO.Ports;
using System.Threading;

namespace Light
{
    public class CircularLight : ILightControl
    {
        private bool isFront = false;
        private int frontLight = 100;
        private int backLight = 100;
        private int outTime = 2;  // 超时时间，单位秒
        private string frontID = "53";
        private string backID = "63";
        private string writeCMD = "";
        private SerialPort serialPort;
        private object lockObj = new object();

        public bool IsReceived { get; private set; }

        public CircularLight(string com, int baudRate = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
        {
            try
            {
                serialPort = new SerialPort(com, baudRate, parity, dataBits, stopBits);
                serialPort.DataReceived += SerialPort_DataReceived;
                serialPort.ErrorReceived += SerialPort_ErrorReceived;
                serialPort.PinChanged += SerialPort_PinChanged;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
            }
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            byte[] buffer = new byte[serialPort.BytesToRead];
            int length = serialPort.Read(buffer, 0, buffer.Length);
            if (length > 0)
            {
                string cmd = "";
                foreach (byte item in buffer)
                {
                    cmd += item.ToString("X2") + " ";
                }
                IsReceived = cmd.Trim() == writeCMD.ToUpper();
                Extend.AddLog("收到" + (isFront ? "前" : "后") + "光源回复：" + cmd.Trim(), LogType.OtherLog);
            }
        }

        private void SerialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {

        }

        private void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {

        }

        private void Write(string cmd)
        {
            IsReceived = false;
            if (serialPort.IsOpen)
            {
            WriteData:
                writeCMD = cmd;
                byte[] bytes = Extend.HexStringToByteArray(cmd);
                serialPort.Write(bytes, 0, bytes.Length);
                int length = outTime * 10;
                for (int i = 0; i < length; i++)
                {
                    if (IsReceived)
                    {
                        Thread.Sleep(100);
                        return;
                    }
                    Thread.Sleep(100);
                }
                Extend.AddLog("光源指令超时，重新发送指令", LogType.OtherLog);
                goto WriteData;
            }
        }

        public void PowerOn(LightName light)
        {
            ModbusTCP.SetAddress12(Address12Type.RobotMzLedPower, 1);
        }

        public void PowerOff(LightName light)
        {
            ModbusTCP.SetAddress12(Address12Type.RobotMzLedPower, 2);
        }

        public bool LightConnect(LightName light)
        {
            switch (light)
            {
                case LightName.FrontRobotLight:
                    if (!serialPort.IsOpen)
                    {
                        serialPort.Open(); 
                    }
                    return serialPort.IsOpen;
                case LightName.BackRobotLight:
                    if (!serialPort.IsOpen)
                    {
                        serialPort.Open(); 
                    }
                    return serialPort.IsOpen;
                case LightName.LineCameraLight:
                    return true;
            }
            return false;
        }

        public bool LightDisConnect(LightName light)
        {
            switch (light)
            {
                case LightName.FrontRobotLight:
                    serialPort.Close();
                    return true;
                case LightName.BackRobotLight:
                    serialPort.Close();
                    return true;
                case LightName.LineCameraLight:
                    break;
            }
            return false;
        }

        public bool LightOff(LightName light)
        {
            switch (light)
            {
                case LightName.FrontRobotLight:
                    Write(string.Format("5A {0} 00 {0} 5B", frontID));
                    return true;
                case LightName.BackRobotLight:
                    Write(string.Format("5A {0} 00 {0} 5B", backID));
                    return true;
                case LightName.LineCameraLight:
                    break;
            }
            return false;
        }

        public bool LightOn(LightName light)
        {
            switch (light)
            {
                case LightName.FrontRobotLight:
                    Write(string.Format("5A {0} {1} {2} 5B", frontID, Convert.ToString(frontLight, 16), Convert.ToString(Convert.ToInt32(frontID) + frontLight, 16)));
                    return true;
                case LightName.BackRobotLight:
                    Write(string.Format("5A {0} {1} {2} 5B", backID, Convert.ToString(backLight, 16), Convert.ToString(Convert.ToInt32(backID) + backLight, 16)));
                    return true;
                case LightName.LineCameraLight:
                    break;
            }
            return false;
        }

        public bool SetLightBrightness(LightName light, int brightness)
        {
            switch (light)
            {
                case LightName.FrontRobotLight:
                    frontLight = brightness;
                    return true;
                case LightName.BackRobotLight:
                    backLight = brightness;
                    return true;
                case LightName.LineCameraLight:
                    break;
            }
            return false;
        }
    }
}
