using System;
using System.Collections.Generic;
using System.IO;

namespace UpdateServer
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class Service : IService
    {
        public static string Version = "";
        public static string UpdateDir = "";
        public static List<string> AddSkins = new List<string>();

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public string GetVersion()
        {
            return Version;
        }

        public string[] GetUpdateFiles()
        {
            DirectoryInfo directory = new DirectoryInfo(UpdateDir);
            FileInfo[] files = directory.GetFiles();
            string[] names = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                names[i] = files[i].Name;
            }
            return names;
        }

        public byte[] GetUpdateFile(string name)
        {
            FileStream file = new FileStream(UpdateDir + name, FileMode.Open);
            byte[] bytes = new byte[file.Length];
            file.Read(bytes, 0, bytes.Length);
            file.Close();
            return bytes;
        }

        public Dictionary<string, byte[]> GetSkins()
        {
            Dictionary<string, byte[]> dict = new Dictionary<string, byte[]>();
            foreach (string item in AddSkins)
            {
                using (FileStream stream = new FileStream(item, FileMode.Open))
                {
                    byte[] bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    dict.Add(item.Substring(item.LastIndexOf("\\Skin\\")), bytes);
                }
            }
            return dict;
        }
    }
}
