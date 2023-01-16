using System;

namespace Rgv
{
    public class RgvModCtrlFun
    {
        public static byte[] RgvModCtrlCmd_NormalStop()
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();
            return pack.ToAraayWithModbus_06SendPack(0x0001, 0x0000);
        }
        public static byte[] RgvModCtrlCmd_ForwardMotor()
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();
            return pack.ToAraayWithModbus_06SendPack(0x0001, 0x0001);
        }
        public static byte[] RgvModCtrlCmd_BackwardMotor()
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();
            return pack.ToAraayWithModbus_06SendPack(0x0001, 0x0002);
        }
        public static byte[] RgvModCtrlCmd_ClearAlarm()
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();
            return pack.ToAraayWithModbus_06SendPack(0x0001, 0x0007);
        }
        public static byte[] RgvModCtrlCmd_StartPowerCharge()
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();
            return pack.ToAraayWithModbus_06SendPack(0x0001, 0x000b);
        }
        public static byte[] RgvModCtrlCmd_StopPowerCharge()
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();
            return pack.ToAraayWithModbus_06SendPack(0x0001, 0x000c);
        }

        //查询运行状态
        public static byte[] RgvModCtrlCmd_QureyStat()
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();
            return pack.ToAraayWithModbus_03SendPack(0x0012, 0x000b);
        }

        //运动到指定距离
        public static byte[] RgvModCtrlCmd_RunAppointDistance()
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();
            return pack.ToAraayWithModbus_06SendPack(0x0001, 0x0005);
        }

        //设置目标运行速度
        public static byte[] RgvModCtrlCmd_SetTargetSpeed(int speed)
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();

            byte[] databuf = BitConverter.GetBytes(speed);
            return pack.ToAraayWithModbus_10SendPack(0x000a, 0x0002, databuf, 4);
        }

        //设置目标运行距离
        public static byte[] RgvModCtrlCmd_SetTargetDistance(int diatance)
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();

            byte[] databuf = BitConverter.GetBytes(diatance);
            return pack.ToAraayWithModbus_10SendPack(0x0004, 0x0002, databuf, 4);
        }

        //设置轨道长度
        public static byte[] RgvModCtrlCmd_SetTrackLength(int length)
        {
            MoubusProtocolFuncPack pack = new MoubusProtocolFuncPack();

            byte[] databuf = BitConverter.GetBytes(length);
            return pack.ToAraayWithModbus_10SendPack(0x0002, 0x0002, databuf, 4);
        }
    }
}
