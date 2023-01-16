using EMU.ApplicationData;
using EMU.Interface;

namespace Project.ServerClass
{
    internal class RgvTask : IRgvControl
    {
        public event RgvModInfo GetRgvModInfo;

        public void ActionEvent()
        {

        }

        public bool RgvBackMove()
        {
            RobotServer.Instance.Command(Cmd.backward);
            return true;
        }

        public bool RgvClearAlarm()
        {
            RobotServer.Instance.Command(Cmd.clear_alarm);
            return true;
        }

        public bool RgvConnect()
        {
            return true;
        }

        public bool RgvDisConnect()
        {
            return true;
        }

        public bool RgvEmergeStop()
        {
            RobotServer.Instance.Command(Cmd.rgv_stop);
            return true;
        }

        public bool RgvForwardMove()
        {
            RobotServer.Instance.Command(Cmd.forward);
            return true;
        }

        public bool RgvIntelligentCharging()
        {
            RobotServer.Instance.Command(Cmd.powercharge);
            return true;
        }

        public bool RgvNormalStop()
        {
            RobotServer.Instance.Command(Cmd.rgv_stop);
            return true;
        }

        public bool RgvRunDistance()
        {
            RobotServer.Instance.Command(Cmd.rgv_run);
            return true;
        }

        public bool RgvSetDistance(int distance)
        {
            RobotServer.Instance.Command(Cmd.set_distance);
            return true;
        }

        public bool RgvSetLength(int length)
        {
            RobotServer.Instance.Command(Cmd.set_length);
            return true;
        }

        public bool RgvSetSpeed(int speed)
        {
            RobotServer.Instance.Command(Cmd.set_speed);
            return true;
        }

        public bool RgvTerminateCharging()
        {
            RobotServer.Instance.Command(Cmd.powerstop);
            return true;
        }

        public void ActEvent()
        {
            GetRgvModInfo?.Invoke(RgvGlobalInfo.Instance);
        }
    }
}
