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
        public static extern void DestroyExportObject(IntPtr ptr);
        [DllImport("img_func_dll.dll")]
        public static extern void GetModelConfig(IntPtr obj, string func_name, float NMS_THRESH, float CONF_THRESH, int INPUT_H_v6, int INPUT_W_v6, string engine_name_c, string classname_c);
        [DllImport("img_func_dll.dll")]
        public static extern int CallOnInit(IntPtr obj, string log);
        [DllImport("img_func_dll.dll")]
        public static extern IntPtr Callgetres(IntPtr obj, string checkimg_path, string modelimg_path, string historypath,
            [In, Out] int[] input_task, int input_info_len, [In, Out] model_struct[] mode_box, int model_struct_len, ref int res_len);
        [DllImport("img_func_dll.dll")]
        public static extern IntPtr NewCallgetres(IntPtr obj, [In, Out] byte[] img1_info, int w1, int h1, [In, Out] byte[] img2_info, int w2, int h2,
            [In, Out] int[] input_info, int input_info_len, [In, Out] model_struct[] mode_box, int model_struct_len, ref int res_len);
    }
}
