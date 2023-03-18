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
                    deleteDir(Application.StartupPath + "\\bak_error");
                    deleteDir(Application.StartupPath + "\\Image");
                    deleteDir(Application.StartupPath + "\\log");
                    return; 
                }
                else if (args[0] == "muban")
                {
                    if (args.Length > 1)
                    {
                        try
                        {
                            string path = args[1];
                            string[] dirs = Directory.GetDirectories(path);
                            Console.WriteLine(args[0] + "  " + args[1]);
                            foreach (string item in dirs)
                            {
                                Console.WriteLine(item);
                            }
                            Console.ReadKey();
                            foreach (string item in dirs)
                            {
                                MoveMuban(item);
                            }
                            Console.ReadKey();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    do
                    {
                        string arg = "start";
                        for (int i = 1; i < args.Length; i++)
                        {
                            arg += " " + args[i];
                        }
                        Process[] process = Process.GetProcessesByName(args[0]);
                        if (process == null || process.Length == 0)
                        {
                            Process.Start(Application.StartupPath + "\\" + args[0] + ".exe", arg);
                        }
                        Thread.Sleep(1000);
                    } while (true);
                }
            }
        }

        static void MoveMuban(string path, DirectoryInfo directory = null)
        {
            if (directory == null)
            {
                directory = new DirectoryInfo(path); 
            }
            DirectoryInfo[] childDir = directory.GetDirectories();
            foreach (DirectoryInfo item in childDir)
            {
                Console.WriteLine(item.FullName);
            }
            foreach (DirectoryInfo item in childDir)
            {
                MoveMuban(path, item);
            }
            string name = "";
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo item in files)
            {
                Console.WriteLine(item.FullName);
            }
            foreach (FileInfo item in files)
            {
                if (item.Name.Contains("_"))
                {
                    name = item.Name.Replace("_.", ".");
                    name = name.Substring(name.LastIndexOf("_") + 1);
                }
                else
                {
                    name = item.Name;
                }
                name = path + "\\" + name;
                if (item.FullName == name)
                {
                    Console.WriteLine("跳过已存在文件: " + name);
                }
                else if (File.Exists(name))
                {
                    Console.WriteLine("删除已存在文件: " + name);
                    File.Delete(name);
                }
                else
                {
                    Console.WriteLine(item.FullName + " -> " + name);
                    File.Move(item.FullName, name); 
                }
            }
            if (directory.GetFiles().Length == 0 && directory.GetDirectories().Length == 0)
            {
                Console.WriteLine("删除文件夹: " + directory.FullName);
                directory.Delete(); 
            }
        }
    }
}
