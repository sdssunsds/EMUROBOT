using System;
using System.Runtime.InteropServices;

namespace AlgorithmLib
{
    public class Algorithm
    {
        [DllImport("yolov5_Trt.dll")]
        public static extern IntPtr ExportObjectFactorycutimg();
        [DllImport("yolov5_Trt.dll")]
        public static extern int CallOnInitcutimg(IntPtr obj, ref byte log, string engine_path);
        [DllImport("yolov5_Trt.dll")]
        public static extern IntPtr Callcutimg(IntPtr obj, config_info inputrect_4, [In, Out] input_task[] input_task, int input_task_len, [In, Out] int[] length, int height_len, ref int len, ref int progress, ref byte log);
        [DllImport("img_func_dll.dll")]
        public static extern IntPtr ExportObjectFactory();
        [DllImport("img_func_dll.dll")]
        public static extern void GetModelConfig(IntPtr obj, string func_name, float NMS_THRESH, float CONF_THRESH, int INPUT_H_v6, int INPUT_W_v6, string engine_name_c, string classname_c);
        [DllImport("img_func_dll.dll")]
        public static extern int CallOnInit(IntPtr obj, ref byte log);
        [DllImport("img_func_dll.dll")]
        public static extern IntPtr Callgetres(IntPtr obj, input_struct input_struct, [In, Out] model_struct[] model_struct, int model_struct_len, ref int len, ref int progress, ref byte log);
        [DllImport("img_func_dll.dll")]
        public static extern IntPtr NewCallgetres(IntPtr obj, string file_path, [In, Out] input_task[] input_task, int input_task_len, [In, Out] model_struct[] model_struct, int model_struct_len, ref int len, ref int progress, ref byte log);
    }
}
