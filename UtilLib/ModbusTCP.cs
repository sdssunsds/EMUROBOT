using PLC;
using System;

namespace EMU.Util
{
    public class ModbusTCP
    {
        private static PLCManager.PLC_Parent mb = null;
        private static PLCManager.PLC_Parent address12 = null;
        private static PLCManager.PLC_Parent address13 = null;
        private static int[] address12Values = new int[16];
        private static int[] address13Values = new int[16];

        public static void InitModbusTCP(string ip)
        {
            if (mb == null)
            {
                mb = PLCManager.GetModbusTcp(ip, 502, 1);
                mb.Address = "2";
            }

            if (address12 == null)
            {
                address12 = PLCManager.GetModbusTcp();
                address12.Address = "12";
            }

            if (address13 == null)
            {
                address13 = PLCManager.GetModbusTcp();
                address13.Address = "13";
            }
        }

        /// <summary>
        /// 获取传感器状态
        /// </summary>
        /// <param name="i">索引，从0开始</param>
        public static bool GetIO(int i)
        {
            short val = 1;
            string bytestring = Convert.ToString(val, 2);
            for (int j = 0; j < 16; j++)
            {
                if (bytestring.Length < 16)
                {
                    bytestring = "0" + bytestring;
                }
            }
            return bytestring[i] == '1';
        }

        public static void SetAddress12(Address12Type type, int val)
        {
            switch (type)
            {
                case Address12Type.RobotMzLedPower:
                    address12Values[15] = val; // 15是协议第一位
                    break;
                case Address12Type.RobotFrontMzPower:
                    address12Values[14] = val;
                    break;
                case Address12Type.RobotXzLedPower:
                    address12Values[13] = val; // 13是协议第三位
                    break;
                case Address12Type.RobotBackMzPower:
                    address12Values[12] = val;
                    break;
                case Address12Type.FrontRobotStepMotorPower:
                    address12Values[11] = val;
                    break;
                case Address12Type.RobotXZPower:
                    address12Values[10] = val;
                    break;
                case Address12Type.BackRobotStepMotorPower:
                    address12Values[9] = val;
                    break;
                case Address12Type.FrontRobotStart:
                    address12Values[7] = val;
                    break;
                case Address12Type.FrontRobotStop:
                    address12Values[6] = val;
                    break;
                case Address12Type.FrontRobotEMGRst:
                    address12Values[5] = val;
                    break;
                case Address12Type.FrontRobotAlmClear:
                    address12Values[4] = val;
                    break;
                case Address12Type.BackRobotStart:
                    address12Values[3] = val;
                    break;
                case Address12Type.BackRobotStop:
                    address12Values[2] = val;
                    break;
                case Address12Type.BackRobotEMGRst:
                    address12Values[1] = val;
                    break;
                case Address12Type.BackRobotAlmClear:
                    address12Values[0] = val;
                    break;
            }
            string s = "";
            for (int i = 0; i < address12Values.Length; i++)
            {
                s += address12Values[i];
            }
            address12.ShortValue = Convert.ToInt16(s, 2);
        }

        public static void SetAddress13(Address13Type type, int val)
        {
            switch (type)
            {
                case Address13Type.Music_Robot_Power_on:
                    address13Values[7] = val;
                    break;
                case Address13Type.Music_Robot_Start_Working:
                    address13Values[6] = val;
                    break;
                case Address13Type.Music_Robot_In_operation:
                    address13Values[7] = val;
                    break;
                case Address13Type.Music_Robot_Work_complete:
                    address13Values[4] = val;
                    break;
            }
            string s = "";
            for (int i = 0; i < address13Values.Length; i++)
            {
                s += address13Values[i];
            }
            address13.ShortValue = Convert.ToInt16(s, 2);
        }
    }

    public enum Address12Type
    {
        /// <summary>
        /// 面阵光源电源
        /// </summary>
        RobotMzLedPower,
        /// <summary>
        /// 前相机电源控制
        /// </summary>
        RobotFrontMzPower,
        /// <summary>
        /// 线阵光源电源
        /// </summary>
        RobotXzLedPower,
        /// <summary>
        /// 后相机电源控制
        /// </summary>
        RobotBackMzPower,
        /// <summary>
        /// 前滑台电源控制
        /// </summary>
        FrontRobotStepMotorPower,
        /// <summary>
        /// 线阵相机电源控制
        /// </summary>
        RobotXZPower,
        /// <summary>
        /// 后滑台电源控制
        /// </summary>
        BackRobotStepMotorPower,
        /// <summary>
        /// 前机械臂启动（通知继电器）
        /// </summary>
        FrontRobotStart,
        /// <summary>
        /// 前机械臂终止
        /// </summary>
        FrontRobotStop,
        /// <summary>
        /// 前机械臂急停复位
        /// </summary>
        FrontRobotEMGRst,
        /// <summary>
        /// 前机械臂清除报警
        /// </summary>
        FrontRobotAlmClear,
        /// <summary>
        /// 后机械臂启动（通知继电器）
        /// </summary>
        BackRobotStart,
        /// <summary>
        /// 后机械臂终止
        /// </summary>
        BackRobotStop,
        /// <summary>
        /// 后机械臂急停复位
        /// </summary>
        BackRobotEMGRst,
        /// <summary>
        /// 后机械臂清除报警
        /// </summary>
        BackRobotAlmClear
    }

    public enum Address13Type
    {
        Music_Robot_Power_on,
        Music_Robot_Start_Working,
        Music_Robot_In_operation,
        Music_Robot_Work_complete
    }
}
