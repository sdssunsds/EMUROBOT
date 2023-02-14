using AlgorithmLib;
using EMU.Util;
using Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmControl
{
    class Program
    {
        static string taskID = "";
        static void Main(string[] args)
        {
            taskID = args[0];
            string bakPath = Application.StartupPath + "\\bak_img\\";
            try
            {
                AddLog("下载图片：" + args[2]);
                Bitmap bitmap = GW.Function.ImageFunction.Manage.ImageManage.Download(args[2]);
                //Bitmap bitmap = (Bitmap)Image.FromFile(Application.StartupPath + "\\1.jpg");
                //AddLog("下载图片：" + args[3]);
                //Bitmap upBitmap = GW.Function.ImageFunction.Manage.ImageManage.Download(args[3]);
                int width = bitmap.Width;
                int height = bitmap.Height;
                AddLog("缓存完成：" + args[0]);
                if (!Directory.Exists(bakPath))
                {
                    Directory.CreateDirectory(bakPath);
                }
                byte[] imgBytes = Image2Byte(bitmap);
                AddLog("Bitmap转换byte数组完成");
                StringBuilder sb = new StringBuilder();
                for (int i = 4; i < args.Length; i++)
                {
                    sb.Append(args[i]);
                }
                string json = sb.ToString();
                json = json.Replace("&&", "\"");
                AddLog("转换Json：" + json);
                RedisBusiness[] businesses = JsonManager.JsonToObject<RedisBusiness[]>(json);
                AddLog("Json转换完成");
                AddLog("识别类型：" + args[1]);
                Task.Run(() =>
                {
                    bitmap.Save(bakPath + taskID + ".jpg");
                    AddLog("图片备份完成");
                    bitmap.Dispose();
                });
                IntPtr ptr = Algorithm.ExportObjectFactory();
                AddLog("算法初始化成功");
                int r = Algorithm.CallOnInit(ptr, null);
                AddLog("Call: " + r);
                if (r == 0)
                {
                    Dictionary<int, List<Rectangle>> valueDict = new Dictionary<int, List<Rectangle>>();
                    foreach (RedisBusiness item in businesses)
                    {
                        string[] ts = args[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        AddLog("获取任务ID: " + JsonManager.ObjectToJson(ts));
                        List<int> inputTask = new List<int>();
                        for (int i = 0; i < ts.Length; i++)
                        {
                            try
                            {
                                inputTask.Add(int.Parse(ts[i]));
                            }
                            catch (Exception) { }
                        }
                        int len = 0;
                        AddLog($"调用图像算法，并传参: {imgBytes.Length}, {width}, {height}, {JsonManager.ObjectToJson(inputTask)}, {inputTask.Count}, {JsonManager.ObjectToJson(item.TaskList)}, {item.TaskList.Count}");
                        IntPtr _result = Algorithm.NewCallgetres(ptr, imgBytes, width, height, null, 0, 0, inputTask.ToArray(), inputTask.Count, item.TaskList.ToArray(), item.TaskList.Count, ref len);
                        int _length = Marshal.SizeOf(typeof(box_info));
                        long _len = _result.ToInt64();
                        for (int j = 0; j < len; j++)
                        {
                            box_info value = Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + j * _length)));
                            AddLog("装载box_info: " + JsonManager.ObjectToJson(value));
                            if (!valueDict.ContainsKey(value.state_enum))
                            {
                                valueDict.Add(value.state_enum, new List<Rectangle>());
                            }
                            valueDict[value.state_enum].Add(new Rectangle(value.x, value.y, value.w, value.h));
                        }
                    }

                    List<RedisResult> results = new List<RedisResult>();
                    foreach (KeyValuePair<int, List<Rectangle>> item in valueDict)
                    {
                        RedisResult result = new RedisResult()
                        {
                            jclx = item.Key.ChangeCode(),
                            result = item.Value
                        };
                        results.Add(result);
                    }
                    json = JsonManager.ObjectToJson(results);
                    AddLog("算法结果：" + json);
                    string resultFile = Application.StartupPath + "\\bak_result\\" + taskID + ".json";
                    using (StreamWriter sw = new StreamWriter(resultFile))
                    {
                        sw.Write(json);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static void AddLog(string log)
        {
            Console.WriteLine(log);
            string logPath = Application.StartupPath + "\\log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\";
            if (!Directory.Exists(logPath))
            {
                Directory.CreateDirectory(logPath);
            }
            using (StreamWriter sw = new StreamWriter(logPath + "control" + taskID + ".log", true))
            {
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + log);
            }
        }

        static byte[] Image2Byte(Bitmap bitmap)
        {
            Rectangle rectangle = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData data = bitmap.LockBits(rectangle, ImageLockMode.ReadOnly, bitmap.PixelFormat);
            int rowCount = data.Width * Image.GetPixelFormatSize(bitmap.PixelFormat) / 8;
            long length = bitmap.Height * (long)rowCount;
            byte[] bytes = new byte[length];
            IntPtr ptr = data.Scan0; 
            for (int i = 0; i < bitmap.Height; i++)
            {
                Marshal.Copy(ptr, bytes, i * rowCount, rowCount);
                ptr += data.Stride;
            }
            bitmap.UnlockBits(data);
            return bytes;
        }
    }
}
