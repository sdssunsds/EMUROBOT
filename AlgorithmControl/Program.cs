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
            taskID = args[args.Length - 1];
            string bakPath = Application.StartupPath + "\\bak_img\\";
            try
            {
                AddLog("下载图片：" + args[0]);
#if true
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
                byte[] imgBytes = Image2Byte(bitmap);
                AddLog("Bitmap转换byte数组完成");
                StringBuilder sb = new StringBuilder();
                int length = args.Length - 2;
                for (int i = 1; i < length; i++)
                {
                    sb.Append(args[i]);
                }
                string json = sb.ToString();
                json = json.Replace("&&", "\"");
                AddLog("转换Json：" + json);
                RedisBusiness[] businesses = JsonManager.JsonToObject<RedisBusiness[]>(json);
                AddLog("Json转换完成");
                AddLog("识别类型：" + args[args.Length - 2]);
                Task.Run(() =>
                {
                    bitmap.Save(bakPath + taskID + ".jpg");
                    AddLog("图片备份完成");
                });
                // 调用算法
                List<RedisResult> results = new List<RedisResult>();
                #region 伪结果
                int ma = 0;
                if (taskID == "002")
                {
                    ma = 11;
                }
                for (int i = 0; i < 2; i++)
                {
                    results.Add(new RedisResult()
                    {
                        jclx = "020" + i,
                        result = new List<Rectangle>()
                    {
                        new Rectangle(i * 20 + ma, i * 20, 10, i + 10),
                        new Rectangle(i * 40 + ma, i * 40, 30, 30)
                    }
                    });
                }
                #endregion
                json = JsonManager.ObjectToJson(results);
                AddLog("算法结果：" + json);
                string resultFile = Application.StartupPath + "\\bak_result\\" + taskID + ".json";
                using (StreamWriter sw = new StreamWriter(resultFile))
                {
                    sw.Write(json);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                try
                {
                    Bitmap bitmap = GW.Function.ComputerFunction.Computer.GetScreenImgByteArray();
                    bitmap.Save(bakPath + taskID + ".png");
                }
                catch (Exception) { }
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
