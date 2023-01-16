using System;
using System.Collections.Generic;

namespace EMU.Parameter
{
    /// <summary>
    /// 部件类型
    /// </summary>
    public enum PartsType
    {
        NULL = 0x0000,
        车体排障器 = 0101,
        辅助排障器 = 0102,
        转向架排障器 = 0103,
        BALISE天线 = 0201,
        STM天线 = 0202,
        砂箱 = 0301,
        风管 = 0302,
        撒沙管 = 0303,
        安装托架 = 0304,
        注沙口 = 0305,
        电加热电源线 = 0306,
        撒沙喷嘴 = 0307,
        加热器 = 0308,
        制动轮盘 = 0401,
        制动轴盘 = 0402,
        轴盘定心环 = 0403,
        轮盘踏面 = 0404,
        轮轴 = 0501,
        制动夹钳 = 0601,
        闸片与闸片托 = 0602,
        作用缸 = 0603,
        作用缸及风管 = 0604,
        六棱施封锁 = 0605,
        开口销 = 0606,
        锁簧 = 0607,
        牵引拉杆 = 0701,
        拉杆螺栓及防松铁丝 = 0702,
        牵引杆橡胶节点 = 0703,
        横向止挡 = 0801,
        半主动油压减震器 = 0901,
        横向减震器 = 0902,
        减震器座 = 0903,
        抗侧滚扭杆装置 = 1001,
        差压阀 = 1101,
        转向架构架 = 1201,
        防滑阀 = 1301,
        防滑阀管线 = 1302,
        安装管线 = 1401,
        下底板 = 1501,
        防雪风挡 = 1601,
        内风挡 = 1701,
        端部新风过滤网 = 1801,
        密接车钩 = 1901,
        齿轮箱 = 2001,
        齿轮箱温度传感器 = 2002,
        速度传感器 = 2003,
        接地装置 = 2004,
        呼吸器 = 2005,
        接地线 = 2006,
        排油口 = 2007,
        注油口 = 2008,
        磁栓 = 2009,
        油位窗 = 2010,
        悬吊部件 = 2011,
        牵引电动机 = 2101,
        注油孔堵 = 2102,
        牵引电动机温度传感器 = 2103,
        冷却风道 = 2104,
        出风口 = 2105,
        联轴节 = 2201,
        外风挡 = 2301,
        传感器分线盒 = 2401,
        端板 = 2501,
        车头下底板 = 2601,
        ATP底板 = 2602,
        司机室空调底板 = 2603,
        空调冷凝器底板 = 2604,
        空调蒸发器底板 = 2605,
        辅助整流器箱底板 = 2606,
        牵引变流器底板 = 2607,
        污物箱底板 = 2608,
        换气装置底板 = 2609,
        水箱底板 = 2610,
        蓄电池箱底板 = 2611,
        高压机器箱底板 = 2612,
        牵引变压器底板 = 2613,
        制动单元BCU底板 = 2614,
        辅助空压机底板 = 2615,
        主空压机底板 = 2616,
        接触器箱底板 = 2617,
        车头罩 = 2701
    }

    /// <summary>
    /// 部件字典
    /// </summary>
    public class PartsDict
    {
        public static Dictionary<PartsType, List<string>> PartDict = new Dictionary<PartsType, List<string>>();
        public static int GetIDint(PartsType type)
        {
            string s = GetID(type);
            return Convert.ToInt32(s, 16);
        }
        public static string GetID(PartsType type)
        {
            switch (type)
            {
                case PartsType.车体排障器: return "0101";
                case PartsType.辅助排障器: return "0102";
                case PartsType.转向架排障器: return "0103";
                case PartsType.BALISE天线: return "0201";
                case PartsType.STM天线: return "0202";
                case PartsType.砂箱: return "0301";
                case PartsType.风管: return "0302";
                case PartsType.撒沙管: return "0303";
                case PartsType.安装托架: return "0304";
                case PartsType.注沙口: return "0305";
                case PartsType.电加热电源线: return "0306";
                case PartsType.撒沙喷嘴: return "0307";
                case PartsType.加热器: return "0308";
                case PartsType.制动轮盘: return "0401";
                case PartsType.制动轴盘: return "0402";
                case PartsType.轴盘定心环: return "0403";
                case PartsType.轮盘踏面: return "0404";
                case PartsType.轮轴: return "0501";
                case PartsType.制动夹钳: return "0601";
                case PartsType.闸片与闸片托: return "0602";
                case PartsType.作用缸: return "0603";
                case PartsType.作用缸及风管: return "0604";
                case PartsType.六棱施封锁: return "0605";
                case PartsType.开口销: return "0606";
                case PartsType.锁簧: return "0607";
                case PartsType.牵引拉杆: return "0701";
                case PartsType.拉杆螺栓及防松铁丝: return "0702";
                case PartsType.牵引杆橡胶节点: return "0703";
                case PartsType.横向止挡: return "0801";
                case PartsType.半主动油压减震器: return "0901";
                case PartsType.横向减震器: return "0902";
                case PartsType.减震器座: return "0903";
                case PartsType.抗侧滚扭杆装置: return "1001";
                case PartsType.差压阀: return "1101";
                case PartsType.转向架构架: return "1201";
                case PartsType.防滑阀: return "1301";
                case PartsType.防滑阀管线: return "1302";
                case PartsType.安装管线: return "1401";
                case PartsType.下底板: return "1501";
                case PartsType.防雪风挡: return "1601";
                case PartsType.内风挡: return "1701";
                case PartsType.端部新风过滤网: return "1801";
                case PartsType.密接车钩: return "1901";
                case PartsType.齿轮箱: return "2001";
                case PartsType.齿轮箱温度传感器: return "2002";
                case PartsType.速度传感器: return "2003";
                case PartsType.接地装置: return "2004";
                case PartsType.呼吸器: return "2005";
                case PartsType.接地线: return "2006";
                case PartsType.排油口: return "2007";
                case PartsType.注油口: return "2008";
                case PartsType.磁栓: return "2009";
                case PartsType.油位窗: return "2010";
                case PartsType.悬吊部件: return "2011";
                case PartsType.牵引电动机: return "2101";
                case PartsType.注油孔堵: return "2102";
                case PartsType.牵引电动机温度传感器: return "2103";
                case PartsType.冷却风道: return "2104";
                case PartsType.出风口: return "2105";
                case PartsType.联轴节: return "2201";
                case PartsType.外风挡: return "2301";
                case PartsType.传感器分线盒: return "2401";
                case PartsType.端板: return "2501";
                case PartsType.车头下底板: return "2601";
                case PartsType.ATP底板: return "2602";
                case PartsType.司机室空调底板: return "2603";
                case PartsType.空调冷凝器底板: return "2604";
                case PartsType.空调蒸发器底板: return "2605";
                case PartsType.辅助整流器箱底板: return "2606";
                case PartsType.牵引变流器底板: return "2607";
                case PartsType.污物箱底板: return "2608";
                case PartsType.换气装置底板: return "2609";
                case PartsType.水箱底板: return "2610";
                case PartsType.蓄电池箱底板: return "2611";
                case PartsType.高压机器箱底板: return "2612";
                case PartsType.牵引变压器底板: return "2613";
                case PartsType.制动单元BCU底板: return "2614";
                case PartsType.辅助空压机底板: return "2615";
                case PartsType.主空压机底板: return "2616";
                case PartsType.接触器箱底板: return "2617";
                case PartsType.车头罩: return "2701";
                default: return "0000";
            }
        }
        public static PartsType GetType(string id)
        {
            return (PartsType)Convert.ToInt32(id, 16);
        }
        public static PartsType GetPartsType(string lengthID)
        {
            PartsType partsType = PartsType.ATP底板;
            foreach (KeyValuePair<PartsType, List<string>> item in PartDict)
            {
                if (item.Value.Contains(lengthID))
                {
                    partsType = item.Key;
                    break;
                }
            }
            return partsType;
        }
    }
}
