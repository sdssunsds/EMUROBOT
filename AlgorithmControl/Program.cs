using EMU.Util;
using Project;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace AlgorithmControl
{
    class Program
    {
        static void Main(string[] args)
        {
            string resultFile = Application.StartupPath + "\\AlgorithmResult.json";
            string bakPath = Application.StartupPath + "\\bak_img\\";
            AddLog("下载图片：" + args[0]);
#if false
            Bitmap bitmap = GW.Function.ImageFunction.Manage.ImageManage.Download(args[0]);
#else
            Bitmap bitmap = new Bitmap(100, 100);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
#endif
            AddLog("缓存完成：" + args[0]);
            if (!Directory.Exists(bakPath))
            {
                Directory.CreateDirectory(bakPath);
            }
            bitmap.Save(bakPath + args[args.Length - 1] + ".jpg");
            OpenCvSharp.Mat mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap);
            AddLog("Bitmap转换Mat完成");
            StringBuilder sb = new StringBuilder();
            int length = args.Length - 2;
            for (int i = 1; i < length; i++)
            {
                sb.Append(args[i]);
            }
            string json = sb.ToString().Replace("&&", "\"");
            AddLog("转换Json：" + json);
            RedisBusiness[] businesses = JsonManager.JsonToObject<RedisBusiness[]>(json);
            AddLog("Json转换完成");
            AddLog("识别类型：" + args[args.Length - 2]);
            // 调用算法
            List<RedisResult> results = new List<RedisResult>();
            #region 伪结果
            for (int i = 0; i < 2; i++)
            {
                results.Add(new RedisResult()
                {
                    jclx = "020" + i,
                    result = new List<Rectangle>()
                    {
                        new Rectangle(i * 20, i * 20, 10, i + 10),
                        new Rectangle(i * 40, i * 40, 30, 30)
                    }
                });
            } 
            #endregion
            json = JsonManager.ObjectToJson(results);
            AddLog("算法结果：" + json);
            using (StreamWriter sw = new StreamWriter(resultFile))
            {
                sw.Write(json);
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
            using (StreamWriter sw = new StreamWriter(logPath + "AlgorithmControl.log", true))
            {
                sw.WriteLine(DateTime.Now.ToString("HH:mm:ss") + "\t" + log);
            }
        }
    }
}
