using EMU.UI;
using EMUROBOT.ServiceReference;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EMUROBOT
{
    static class Program
    {
        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();
        [DllImport("kernel32.dll")]
        static extern bool FreeConsole();
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //AllocConsole();
            Process application = RunningInstance();
            if (application == null)
            {
                if (args != null && args.Length > 0)
                {
                    Properties.Settings.Default.Version = args[0];
                    Properties.Settings.Default.Save();
                }

                #region 升级服务
                try
                {
                    using (ServiceClient service = new ServiceClient())
                    {
                        if (service.GetVersion() != Properties.Settings.Default.Version)
                        {
                            Process.Start(Application.StartupPath + "\\Update.exe");
                            return;
                        }

                        Dictionary<string, byte[]> skinDict = service.GetSkins();
                        foreach (KeyValuePair<string, byte[]> item in skinDict)
                        {
                            string path = Application.StartupPath + item.Key;
                            if (!File.Exists(path))
                            {
                                string dir = path.Substring(0, path.LastIndexOf("\\"));
                                if (!Directory.Exists(dir))
                                {
                                    Directory.CreateDirectory(dir);
                                }

                                using (FileStream stream = new FileStream(path, FileMode.Create))
                                {
                                    stream.Write(item.Value, 0, item.Value.Length);
                                }
                            }
                        }
                    }
                }
                catch (Exception) { }
                #endregion

                MainForm.Version = Properties.Settings.Default.Version;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Project.AlgorithmInterface algorithmInterface = new Project.AlgorithmInterface();
                Application.Run(new MainForm() { Project = algorithmInterface, MF = algorithmInterface, Args = args });
            }
            else
            {
                HandleRunningInstance(application);
            }
            //FreeConsole();
        }
        #region   只运行一个实例
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "//") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }
        private static void HandleRunningInstance(Process instance)
        {
            MessageBox.Show("上位机软件已经在运行！", "提示信息", MessageBoxButtons.OK);
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL); //调用api函数，正常显示窗口
            SetForegroundWindow(instance.MainWindowHandle); //将窗口放置最前端。
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(System.IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(System.IntPtr hWnd);
        private const int WS_SHOWNORMAL = 1;
        #endregion
    }
}
