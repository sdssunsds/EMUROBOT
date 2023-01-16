using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;

namespace Project.ServerClass
{
    internal class RobotTask : IRobotControl
    {
        public event RobotModInfo GetRobotModInfo;

        public bool RobotConnect(RobotName robot)
        {
            return true;
        }

        public bool RobotDisConnect(RobotName robot)
        {
            return true;
        }

        public bool RobotMovePosition(RobotName robot, RobotDataPack robotDataPack)
        {
            return true;
        }

        public bool RobotSelected(RobotName robot)
        {
            return true;
        }

        public bool RobotZeroPosition(RobotName robot)
        {
            RobotServer.Instance.Command(Cmd.robot_zero);
            return true;
        }
    }
}
