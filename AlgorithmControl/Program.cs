//#define noAlgorithm
//#define download2img

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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlgorithmControl
{
    class Program
    {
        static string taskID = "";
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                Action<string[]> deleteFile = (string[] bakFiles) =>
                {
                    if (bakFiles != null)
                    {
                        foreach (string item in bakFiles)
                        {
                            File.Delete(item);
                            Console.WriteLine("删除文件：" + item);
                        }
                    }
                };
                Action<string> deleteDir = (string path) => { };
                deleteDir = (string path) =>
                {
                    deleteFile(Directory.GetFiles(path));
                    string[] dirs = Directory.GetDirectories(path);
                    if (dirs != null)
                    {
                        foreach (string item in dirs)
                        {
                            deleteDir(item);
                            Directory.Delete(item);
                            Console.WriteLine("删除文件夹：" + item);
                        }
                    }
                };
                deleteFile(Directory.GetFiles(Application.StartupPath + "\\bak_redis"));
                deleteFile(Directory.GetFiles(Application.StartupPath + "\\bak_result"));
                deleteDir(Application.StartupPath + "\\bak_img");
                deleteDir(Application.StartupPath + "\\log");
                return;
            }
            taskID = args[0];
            string bakPath = Application.StartupPath + "\\bak_img\\";
            try
            {
                AddLog("下载图片：" + args[2]);
                Bitmap bitmap1 = GW.Function.ImageFunction.Manage.ImageManage.Download(args[2]);
#if download2img
                Bitmap bitmap2 = GW.Function.ImageFunction.Manage.ImageManage.Download(args[3]);
#endif
                int width1 = bitmap1.Width;
                int height1 = bitmap1.Height;
#if download2img
                int width2 = bitmap2.Width;
                int height2 = bitmap2.Height;
#endif
                AddLog("缓存完成：" + args[0]);
                if (!Directory.Exists(bakPath))
                {
                    Directory.CreateDirectory(bakPath);
                }
                byte[] imgBytes1 = Image2Byte(bitmap1);
#if download2img
                byte[] imgBytes2 = Image2Byte(bitmap2);
#endif
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
                    bitmap1.Save(bakPath + taskID + ".jpg");
#if download2img
                    bitmap2.Save(bakPath + taskID + "_up.jpg");
#endif
                    AddLog("图片备份完成");
                    bitmap1.Dispose();
                });
#if noAlgorithm
                int r = 0;
#else
                IntPtr ptr = Algorithm.ExportObjectFactory();
                AddLog("算法初始化成功");
                int r = Algorithm.CallOnInit(ptr, null);
                AddLog("Call: " + r);
#endif
                if (r == 0)
                {
#if noAlgorithm
                    AddLog("装载伪结果");
                    Dictionary<int, List<Rectangle>> valueDict = new Dictionary<int, List<Rectangle>>();
                    Random random = new Random();
                    int length = random.Next(50, 100);
                    for (int i = 0; i < length; i++)
                    {
                        int code = random.Next(0, 13);
                        if (!valueDict.ContainsKey(code))
                        {
                            valueDict.Add(code, new List<Rectangle>());
                        }
                        int x = random.Next(0, i * 10);
                        int y = random.Next(0, i * 10);
                        int w = random.Next(5, 15);
                        int h = random.Next(5, 15);
                        valueDict[code].Add(new Rectangle(x, y, w, h));
                    }
                    AddLog("伪结果装载完成，共计" + length);
#else
                    Dictionary<int, List<Rectangle>> valueDict = new Dictionary<int, List<Rectangle>>();
                    int logI = 0;
                    try
                    {
                        foreach (RedisBusiness item in businesses)
                        {
                            logI = 1;
                            string[] ts = args[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                            AddLog("获取任务ID: " + JsonManager.ObjectToJson(ts));
                            List<int> inputTask = new List<int>();
                            for (int i = 0; i < ts.Length; i++)
                            {
                                try
                                {
                                    logI = 2;
                                    inputTask.Add(int.Parse(ts[i]));
                                }
                                catch (Exception) { }
                            }
                            int len = 0;
                            logI = 3;
#if download2img
                            AddLog($"调用图像算法，并传参: {imgBytes1.Length}, {width1}, {height1}, {imgBytes2.Length}, {width2}, {height2}, {JsonManager.ObjectToJson(inputTask)}, {inputTask.Count}, {JsonManager.ObjectToJson(item.TaskList)}, {item.TaskList.Count}");
                            IntPtr _result = Algorithm.NewCallgetres(ptr, imgBytes1, width1, height1, imgBytes2, width2, height2, inputTask.ToArray(), inputTask.Count, item.TaskList.ToArray(), item.TaskList.Count, ref len);
#else
                            AddLog($"调用图像算法，并传参: {imgBytes1.Length}, {width1}, {height1}, {JsonManager.ObjectToJson(inputTask)}, {inputTask.Count}, {JsonManager.ObjectToJson(item.TaskList)}, {item.TaskList.Count}");
                            IntPtr _result = Algorithm.NewCallgetres(ptr, imgBytes1, width1, height1, null, 0, 0, inputTask.ToArray(), inputTask.Count, item.TaskList.ToArray(), item.TaskList.Count, ref len);
#endif
                            logI = 4;
                            int _length = Marshal.SizeOf(typeof(box_info));
                            logI = 5;
                            long _len = _result.ToInt64();
                            for (int j = 0; j < len; j++)
                            {
                                logI = 6;
                                box_info value = Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + j * _length)));
                                AddLog("装载box_info: " + JsonManager.ObjectToJson(value));
                                logI = 7;
                                if (!valueDict.ContainsKey(value.state_enum))
                                {
                                    valueDict.Add(value.state_enum, new List<Rectangle>());
                                }
                                logI = 8;
                                valueDict[value.state_enum].Add(new Rectangle(value.x, value.y, value.w, value.h));
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        AddLog("[" + logI + "]" + e.Message);
                    }
#endif

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
