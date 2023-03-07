using System;
using System.Diagnostics;
using System.IO;
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
        }
    }
}
