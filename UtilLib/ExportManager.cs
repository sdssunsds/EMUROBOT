using System;
using System.Collections.Generic;
using System.IO;

namespace EMU.Util
{
    public class ExportManager
    {
        public event Action ExportInternalEvent;
        public event Action<int> ExportMaxEvent;
        public void ExportDir(string path, params string[] sourcePaths)
        {
            if (sourcePaths != null)
            {
                int fileCount = 0;
                List<DirectoryInfo> rootDirList = new List<DirectoryInfo>();
                var getDir = new Action<DirectoryInfo>((DirectoryInfo dir) => { });
                getDir = new Action<DirectoryInfo>((DirectoryInfo dir) =>
                {
                    DirectoryInfo[] dirs = dir.GetDirectories();
                    foreach (DirectoryInfo item in dirs)
                    {
                        fileCount++;
                        fileCount += item.GetFiles().Length;
                        getDir(item);
                    }
                });
                foreach (string source in sourcePaths)
                {
                    if (Directory.Exists(source))
                    {
                        DirectoryInfo directory = new DirectoryInfo(source);
                        fileCount++;
                        fileCount += directory.GetFiles().Length;
                        rootDirList.Add(directory);
                        getDir(directory); 
                    }
                }
                ExportMaxEvent?.Invoke(fileCount);

                var expDir = new Action<DirectoryInfo, string>((DirectoryInfo dir, string rootPath) => { });
                expDir = new Action<DirectoryInfo, string>((DirectoryInfo dir, string rootPath) =>
                {
                    rootPath += dir.Name + "\\";
                    Directory.CreateDirectory(rootPath);
                    DirectoryInfo[] directories = dir.GetDirectories();
                    FileInfo[] files = dir.GetFiles();
                    foreach (DirectoryInfo directory in directories)
                    {
                        expDir(directory, rootPath);
                        ExportInternalEvent?.Invoke();
                    }
                    foreach (FileInfo file in files)
                    {
                        File.Copy(file.FullName, rootPath + file.Name);
                        ExportInternalEvent?.Invoke();
                    }
                });
                foreach (DirectoryInfo item in rootDirList)
                {
                    expDir(item, path + "\\");
                    ExportInternalEvent?.Invoke();
                }
            }
        }
    }
}
