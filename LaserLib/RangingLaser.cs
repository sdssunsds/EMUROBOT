using EMU.Util;
using System;
using System.IO.Ports;
using System.Text;
using System.Threading.Tasks;

namespace Laser
{
    public class RangingLaser
    {
        private SerialPort serialPort;
        public Action<string> ReadValue;

        public RangingLaser(string com, int baudRate = 115200, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
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
                string value = Encoding.ASCII.GetString(buffer, 0, buffer.Length);
                Task.Run(() => { ReadValue?.Invoke(value); });
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
