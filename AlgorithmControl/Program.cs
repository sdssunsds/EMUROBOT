//#define testIntptr

using AlgorithmLib;
using EMU.Util;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace AlgorithmControl
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args != null && args.Length > 0)
            {
                if (args[0] == "del")
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
                        if (Directory.Exists(path))
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
                        }
                    };
                    deleteDir(Application.StartupPath + "\\Bak");
                    deleteDir(Application.StartupPath + "\\bak_redis");
                    deleteDir(Application.StartupPath + "\\bak_result");
                    deleteDir(Application.StartupPath + "\\bak_img");
                    deleteDir(Application.StartupPath + "\\Image");
                    deleteDir(Application.StartupPath + "\\log");
                    return; 
                }
                else
                {
                    do
                    {
                        Process[] process = Process.GetProcessesByName(args[0]);
                        if (process == null || process.Length == 0)
                        {
                            Process.Start(Application.StartupPath + "\\" + args[0] + ".exe", "start");
                        }
                        Thread.Sleep(1000);
                    } while (true);
                }
            }
            #region 多进程算法
#if false
            string appID = "";
            if (args != null && args.Length > 0)
            {
                appID = args[0];
            }

            int box_length = Marshal.SizeOf(typeof(box_info));
            string algorithmParPath = Application.StartupPath + "\\algorithm.pars" + appID;
            string resultPath = Application.StartupPath + "\\algorithm.back" + appID;

#if testIntptr
            int r = 0;
#else
            IntPtr ptr = Algorithm.ExportObjectFactory();
            Console.WriteLine("算法启动");
            int r = Algorithm.CallOnInit(ptr, null);
            Console.WriteLine("算法初始化: " + r);
#endif
            if (r < 0)
            {
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Thread.Sleep(1000);
                if (File.Exists(algorithmParPath))
                {
                    Thread.Sleep(100);
                    int width1, width2, height1, height2;
                    string id;
                    byte[] bytes1, bytes2;
                    int[] taskIds;
                    string[] tmp;
                    model_struct[] models = null;
                    using (StreamReader sr = new StreamReader(algorithmParPath))
                    {
                        id = sr.ReadLine();
                        tmp = sr.ReadLine().Split(',');
                        taskIds = new int[tmp.Length];
                        for (int i = 0; i < taskIds.Length; i++)
                        {
                            taskIds[i] = int.Parse(tmp[i].Trim());
                        }
                        bytes1 = Image.FromFile(sr.ReadLine()).ToBytes2(true);
                        bytes2 = Image.FromFile(sr.ReadLine()).ToBytes2(true);
                        width1 = int.Parse(sr.ReadLine());
                        height1 = int.Parse(sr.ReadLine());
                        width2 = int.Parse(sr.ReadLine());
                        height2 = int.Parse(sr.ReadLine());
                        string s = sr.ReadLine();
                        if (s != "null")
                        {
                            models = JsonManager.JsonToObject<model_struct[]>(s);
                        }
                    }
                    File.Delete(algorithmParPath);
                    Console.WriteLine("已经读取完参数：" + id);

                    int len = 0;
#if testIntptr
                    len = 500;
                    IntPtr result = IntPtr.Zero;
                    box_info[] boxes = new box_info[len];
                    for (int i = 0; i < len; i++)
                    {
                        boxes[i] = new box_info()
                        {
                            class_name = new char[50],
                            state_enum = i % 2,
                            x = i,
                            y = i,
                            w = i % 10 + 5,
                            h = i % 5 + 5
                        };
                        for (int j = 0; j < 50; j++)
                        {
                            boxes[i].class_name[j] = ' ';
                        }
                    }
#else
                    IntPtr result = Algorithm.NewCallgetres(ptr, bytes1, width1, height1, bytes2, width2, height2, taskIds, taskIds.Length, models, models == null ? 0 : models.Length, ref len);
                    Console.WriteLine("算法调用完毕");
                    GC.Collect();
                    box_info[] boxes = new box_info[len];
                    long _len = result.ToInt64();
                    Console.Write("(" + len + ")开始装载数据: ");
                    for (int j = 0; j < len; j++)
                    {
                        boxes[j] = Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + j * box_length)));
                        Console.Write(j + ",");
                    }
#endif
                    models = null;
                    Console.WriteLine("数据装载完成");
                    using (StreamWriter sw = new StreamWriter(resultPath))
                    {
                        sw.WriteLine(JsonManager.ObjectToJson(boxes));
                    }

                    Console.WriteLine("结果已经写入文件");
                    break;
                }
            } 
#endif 
            #endregion
        }
    }
}
