using System.Reflection;
using System.Runtime.InteropServices;

namespace AlgorithmLib
{
    public static class AlgorighmStruct
    {
        public static string ToString(this char[] cs, char end)
        {
            string s = "";
            foreach (char item in cs)
            {
                if (item == end)
                {
                    break;
                }
                s += item;
            }
            return s;
        }
        public static string ChangeCode(this int code)
        {
            AlgorithmStateEnum algorithm = (AlgorithmStateEnum)code;
            switch (algorithm)
            {
                case AlgorithmStateEnum.无法检测:
                    return "";
                case AlgorithmStateEnum.正常:
                    return "0200";
                case AlgorithmStateEnum.异常:
                    return "0115";
                case AlgorithmStateEnum.螺丝丢失:
                    return "0205";
                case AlgorithmStateEnum.螺丝松动:
                    return "0201";
                case AlgorithmStateEnum.铁丝断裂:
                    return "0301";
                case AlgorithmStateEnum.划痕:
                    return "0103";
                case AlgorithmStateEnum.异物:
                    return "0107";
                case AlgorithmStateEnum.油位异常:
                    return "0801";
                case AlgorithmStateEnum.油液浑浊:
                    return "4201";
                case AlgorithmStateEnum.漏油:
                    return "0106";
                case AlgorithmStateEnum.丢失:
                    return "0112";
                case AlgorithmStateEnum.车头中缝过大:
                    return "0113";
                case AlgorithmStateEnum.管接头松脱:
                    return "0114";
                default:
                    return "";
            }
        }
        public static char[] ToCharArray(this string value, int length)
        {
            char[] cs = new char[length];
            for (int i = 0; i < length && i < value.Length; i++)
            {
                cs[i] = value[i];
            }
            return cs;
        }
        public static T Copy<T>(T model)
        {
            object copyT = default(T);
            FieldInfo[] fields = typeof(T).GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.FieldType.Equals(typeof(char[])))
                {
                    char[] cs = (char[])field.GetValue(model);
                    char[] vs = new char[cs.Length];
                    for (int i = 0; i < cs.Length; i++)
                    {
                        vs[i] = cs[i];
                    }
                    field.SetValue(copyT, vs);
                }
                else
                {
                    field.SetValue(copyT, field.GetValue(model)); 
                }
            }
            return (T)copyT;
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct config_info
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] train_type;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] time;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
        public char[] img_path;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
        public char[] save_path;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="mode">车型</param>
        /// <param name="sn">车号</param>
        /// <param name="time">时间</param>
        /// <param name="imgPath">原始图片路径</param>
        /// <param name="savePath">切图后路径</param>
        public config_info(string mode, string sn, string time, string imgPath, string savePath)
        {
            train_type = (mode + "_" + sn).ToCharArray(50);
            this.time = time.ToCharArray(50);
            img_path = imgPath.ToCharArray(200);
            save_path = savePath.ToCharArray(200);
        }
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="train">车型车号</param>
        /// <param name="time">时间</param>
        /// <param name="img">原始图片路径</param>
        /// <param name="save">切图后路径</param>
        public config_info(string img, string train, string time, string save)
        {
            img_path = PuzzleCdoublePlus.GetChar(img, 200);
            train_type = PuzzleCdoublePlus.GetChar(train, 50);
            this.time = PuzzleCdoublePlus.GetChar(time, 50);
            save_path = PuzzleCdoublePlus.GetChar(save, 200);
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct input_task
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] task_list;
        public int imgNO;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] location_str;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] part_location_str;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] part_str;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] only_str;
        public int x;
        public int y;
        public int w;
        public int h;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="location">区域编号</param>
        /// <param name="partLocation">部件位置编号</param>
        /// <param name="part">部件编号</param>
        /// <param name="id">总编号</param>
        /// <param name="number">车厢号</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        /// <param name="tasks">任务编号</param>
        public input_task(string location, string partLocation, string part, string id, int number, int x, int y, int width, int height, params AlgorithmTaskEnum[] tasks)
        {
            task_list = new int[10];
            for (int i = 0; i < task_list.Length; i++)
            {
                task_list[i] = -1;
            }
            if (tasks != null)
            {
                for (int i = 0; i < task_list.Length; i++)
                {
                    if (i < tasks.Length)
                    {
                        task_list[i] = (int)tasks[i];
                    }
                    else
                    {
                        break;
                    }
                }
            }
            imgNO = number;
            this.x = x;
            this.y = y;
            w = width;
            h = height;
            location_str = location.ToCharArray(50);
            part_location_str = partLocation.ToCharArray(50);
            part_str = part.ToCharArray(50);
            only_str = id.ToCharArray(50);
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct input_struct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
        public int[] task_list;
        public int imgNO;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] location_str;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] part_location_str;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] part_str;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] only_str;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 200)]
        public char[] img_path;
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct model_struct
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] class_name;
        public int x;
        public int y;
        public int w;
        public int h;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="name">种类</param>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        /// <param name="width">宽</param>
        /// <param name="height">高</param>
        public model_struct(string name, int x, int y, int width, int height)
        {
            class_name = name.ToCharArray(50);
            this.x = x;
            this.y = y;
            w = width;
            h = height;
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct box_info
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] class_name;
        public int state_enum;
        public int x;
        public int y;
        public int w;
        public int h;
    }
    public enum AlgorithmTaskEnum
    {
        螺丝检测 = 0,
        异物 = 1,
        防松铁丝检测 = 2,
        油位表检测 = 3,
        划痕检测 = 4,
        撒沙管检测 = 5,
        易丢失部件检测 = 6
    }
    public enum AlgorithmStateEnum
    {
        无法检测 = -1,
        正常 = 0,
        异常 = 1,
        螺丝丢失 = 2,
        螺丝松动 = 3,
        铁丝断裂 = 4,
        划痕 = 5,
        异物 = 6,
        油位异常 = 7,
        油液浑浊 = 8,
        漏油 = 9,
        丢失 = 10,
        车头中缝过大 = 11,
        管接头松脱 = 12
    }
}
