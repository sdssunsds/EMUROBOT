using System;
using System.Runtime.InteropServices;

namespace AlgorithmLib
{
    public class CheckLocationCdoublePlus
    {
        [DllImport("axis_check.dll")]
        public static extern IntPtr LocationFactory();
        [DllImport("axis_check.dll")]
        public static extern void DestroyLocation(IntPtr obj);
        [DllImport("axis_check.dll")]
        public static extern int CallOnInit(IntPtr obj, string engine_path);
        [DllImport("axis_check.dll")]
        public static extern float Callgetdis(IntPtr obj, string bmp_path, string png_path, int state);
    }
}
