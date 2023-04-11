using System;
using System.Runtime.InteropServices;

namespace AlgorithmLib
{
    public class PuzzleCdoublePlus
    {
        [DllImport("yolov5_Trt.dll")]
        public static extern IntPtr ExportObjectFactory();
        [DllImport("yolov5_Trt.dll")]
        public static extern void DestroyExportObject(IntPtr obj);
        [DllImport("yolov5_Trt.dll")]
        public static extern int CallOnInit(IntPtr obj, string engine_path);
        [DllImport("yolov5_Trt.dll")]
        public static extern int Callcutimg(IntPtr obj, config_info input_info, [In, Out] inputrect_4[] cut_info, int cut_info_length, [In, Out] int[] length, int length_num, int progressIndex);

        public static char[] GetChar(string value, int length)
        {
            char[] cs = new char[length];
            for (int i = 0; i < length && i < value.Length; i++)
            {
                cs[i] = value[i];
            }
            return cs;
        }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct inputrect_4
    {
        /// <summary>
        /// 车厢号
        /// </summary>
        public int imgNO;
        /// <summary>
        /// 区域编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] location_str;
        /// <summary>
        /// 部件位置编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] part_location_str;
        /// <summary>
        /// 部件编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] part_str;
        /// <summary>
        /// 唯一编号
        /// </summary>
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
        public char[] only_str;
        public int x;
        public int y;
        public int w;
        public int h;
        public inputrect_4(int no, string location, string partLocation, string part, string id, int x, int y, int w, int h)
        {
            imgNO = no;
            location_str = PuzzleCdoublePlus.GetChar(location, 50);
            part_location_str = PuzzleCdoublePlus.GetChar(partLocation, 50);
            part_str = PuzzleCdoublePlus.GetChar(part, 50);
            only_str = PuzzleCdoublePlus.GetChar(id, 50);
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
        }
    };
}
