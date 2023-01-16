using EMU.Parameter;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;

namespace EMU.Util
{
    public class ZipManager
    {
        /// <summary>
        /// 无密码压缩
        /// </summary>
        /// <param name="dirPath">被压缩的文件夹路径</param>
        /// <param name="zipPath">压缩后文件路径</param>
        public static void CreateZipFile(string dirPath, string zipPath)
        {
            FileManager.ExistsDirectory(zipPath);
            if (Directory.Exists(dirPath))
            {
                try
                {
                    string[] filenames = Directory.GetFiles(dirPath);
                    using (ZipOutputStream s = new ZipOutputStream(File.Create(zipPath)))
                    {

                        s.SetLevel(9);
                        byte[] buffer = new byte[4096];
                        foreach (string file in filenames)
                        {
                            ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                            entry.DateTime = DateTime.Now;
                            s.PutNextEntry(entry);
                            using (FileStream fs = File.OpenRead(file))
                            {
                                int sourceBytes;
                                do
                                {
                                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                    s.Write(buffer, 0, sourceBytes);
                                } while (sourceBytes > 0);
                            }
                        }
                        s.Finish();
                        s.Close();
                    }
                }
                catch (Exception e)
                {
                    e.Message.AddLog(LogType.ErrorLog);
                } 
            }
        }
        /// <summary>
        /// 有密码压缩
        /// </summary>
        /// <param name="dirPath">被压缩的文件夹路径</param>
        /// <param name="zipPath">压缩后文件路径</param>
        /// <param name="pwd">压缩密码</param>
        public static void CreateZipFile(string dirPath, string zipPath, string pwd)
        {
            FileManager.ExistsDirectory(zipPath);
            if (Directory.Exists(dirPath))
            {
                try
                {
                    string[] filenames = Directory.GetFiles(dirPath);
                    using (ZipOutputStream s = new ZipOutputStream(File.Create(zipPath)))
                    {

                        s.SetLevel(9);
                        s.Password = pwd;
                        byte[] buffer = new byte[4096];
                        foreach (string file in filenames)
                        {
                            ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                            entry.DateTime = DateTime.Now;
                            s.PutNextEntry(entry);
                            using (FileStream fs = File.OpenRead(file))
                            {
                                int sourceBytes;
                                do
                                {
                                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                    s.Write(buffer, 0, sourceBytes);
                                } while (sourceBytes > 0);
                            }
                        }
                        s.Finish();
                        s.Close();
                    }
                }
                catch (Exception e)
                {
                    e.Message.AddLog(LogType.ErrorLog);
                }
            }
        }
    }
}
