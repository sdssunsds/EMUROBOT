using EMU.Parameter;
using System;
using System.IO;
using System.Windows.Forms;

namespace EMU.Util
{
    public class FileManager
    {
        /// <summary>
        /// 写入文件
        /// </summary>
        public static bool WriteFile(string path, string text)
        {
            try
            {
                ExistsDirectory(path);
                using (StreamWriter writer = new StreamWriter(path, false))
                {
                    writer.Write(text);
                }
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return false;
            }
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        public static bool WriteFile(string path, byte[] data)
        {
            try
            {
                ExistsDirectory(path);
                using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate))
                {
                    stream.Write(data, 0, data.Length);
                }
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return false;
            }
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        public static string ReadFileToString(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    string text = "";
                    using (StreamReader reader = new StreamReader(path))
                    {
                        text = reader.ReadToEnd();
                    }
                    return text;
                }
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
            }
            return string.Empty;
        }
        /// <summary>
        /// 读取文件
        /// </summary>
        public static byte[] ReadFileToByte(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    byte[] data = null;
                    using (FileStream stream = new FileStream(path, FileMode.Open))
                    {
                        data = new byte[stream.Length];
                        stream.Write(data, 0, data.Length);
                    }
                    return data;
                }
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
            }
            return null;
        }
        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        public static bool ExistsDirectory(string path)
        {
            try
            {
                path = path.Substring(0, path.LastIndexOf("\\"));
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                return true;
            }
            catch (Exception e)
            {
                e.Message.AddLog(LogType.ErrorLog);
                return false;
            }
        }
        /// <summary>
        /// 根据模板导出文档
        /// </summary>
        /// <param name="modelPath">模板路径</param>
        public static void ExportWord(string modelPath)
        {
            if (File.Exists(modelPath))
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "文档|*.docx";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {

                }
            }
            else
            {
                MessageBox.Show("未找到检修记录单模板");
            }
        }

        public static string FindImageExtend()
        {
            return ".bmp";
        }

        public static string GetImageExtend()
        {
            return ".jpg";
        }

        public static string SaveImageExtend()
        {
            return ".bmp";
        }
    }
}
