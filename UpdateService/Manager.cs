using System.IO;
using UpdateServer;

namespace UpdateService
{
    public class Manager
    {
        public static void CopyDirectory(string source, string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new DirectoryInfo(source);
                if (Directory.Exists(path + directory.Name))
                {
                    Directory.Delete(path + directory.Name);
                }
                CopyDirectory(directory, source, path);
            }
        }

        private static void CopyDirectory(DirectoryInfo directory, string source, string path)
        {
            DirectoryInfo[] directories = directory.GetDirectories();
            if (directories != null)
            {
                foreach (DirectoryInfo item in directories)
                {
                    Directory.CreateDirectory(path + item.Name);
                    CopyDirectory(item, source, path + item.Name + "\\");
                }
            }

            FileInfo[] files = directory.GetFiles();
            if (files != null)
            {
                foreach (FileInfo item in files)
                {
                    if (item.Extension.ToLower() == ".gif" ||
                        item.Extension.ToLower() == ".ssk")
                    {
                        File.Copy(item.FullName, path + item.Name);
                        Service.AddSkins.Add(path + item.Name);
                    }
                }
            }
        }
    }
}
