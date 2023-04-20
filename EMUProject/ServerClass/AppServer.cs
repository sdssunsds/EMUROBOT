﻿#define algorithm2
#define algorithm3

using GW.Function.ExcelFunction;
using EMU.ApplicationData;
using EMU.Interface;
using EMU.Parameter;
using EMU.Util;
using Project.ServerClass.Model;
using UploadImageServer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Windows.Forms;
using AlgorithmLib;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;
using System.Threading;
using System.Diagnostics;
using System.Configuration;

namespace Project.ServerClass
{
    internal class AppServer : IAppServer
    {
        private const int excelHeightStartRowIndex = 0;
        private const int modelFileMax = 10;
        private static bool locationInitComplete = false;
        private static bool puzzleInitComplete = false;
        private static object algorithmLock = new object();
        private static object completeLock = new object();
        private static object runLock = new object();
        private static IntPtr algObj = IntPtr.Zero;
        private static IntPtr imgObj = IntPtr.Zero;
        private static IntPtr objLocation = IntPtr.Zero;
        private int box_length = 0;
        private int progressIndex = 0;
        private string enginePath;
        private string projectID;
        private ServiceHost host = null;
        private string log = "";
        private List<BackDataModel> backList = null;

        public int AlgorithmProgress
        {
            get { return progressIndex; }
        }
        public int AlgorithmProgressMax { get; private set; }
        public string AlgorithmProjectID
        {
            get { return projectID; }
        }

        public event AcceptClient AcceptClient;
        public event SendClient SendClient;

        public const string ImageXz = "Xz";
        public const string ImageMz = "Mz";
        public const string Data3D = "3D";

        public AppServer()
        {
            box_length = Marshal.SizeOf(typeof(box_info));
        }

        public bool CreateServer(string serverPath)
        {
            if (host == null)
            {
                UploadService.Addlog = AddLog;
                UploadService.Location = GetLocation;
                UploadService.XzImage = ImageReadyXz;
                UploadService.MzImage = ImageReadyMz;
                UploadService.LocImage = ImageReadyLoc;
                UploadService._3dData = Accept3dData;
#if algorithm3
                UploadService.CompleteAction = Complete2;
#else
                UploadService.CompleteAction = Complete;
#endif
                enginePath = Application.StartupPath + @"\cutimg.engine";
                host = new ServiceHost(typeof(UploadService));
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxBufferSize = 2147483647;
                binding.MaxReceivedMessageSize = 2147483647;
                host.AddServiceEndpoint(typeof(IService), binding, serverPath);
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri(serverPath + "PicService");
                    host.Description.Behaviors.Add(behavior);
                    host.Open();
                    return true;
                }
                return false; 
            }
            else
            {
                throw new Exception("服务初始化完成");
            }
        }

        private void AddLog(string log, int type)
        {
            AddLog(log);
        }

        private int GetLocation(string id, string picName1, string picName2, string picName3, string robotID, int state)
        {
            AddLog("获取定位信息");
            string dir = UploadService.UploadPath + id + "_" + robotID;
            string path1 = dir + @"\Location\" + picName1;
            string path2 = dir + @"\Location\" + picName2;
            string path3 = dir + @"\Location\" + picName3;
            if (objLocation == IntPtr.Zero)
            {
                objLocation = CheckLocationCdoublePlus.LocationFactory();
                locationInitComplete = CheckLocationCdoublePlus.CallOnInit(objLocation, Application.StartupPath + "\\axis_s_640.engine") == 0;
            }
            if (locationInitComplete)
            {
                int loc = (int)CheckLocationCdoublePlus.Callgetdis(objLocation, path1, path2, state);
                AddLog("得到定位修正：" + loc);
                return loc;
            }
            return 0;
        }

        private void ImageReadyXz(Image img, string mode, string sn, string robotID)
        {
            AppDeviceFrameEx appDeviceFrame = new AppDeviceFrameEx();
            appDeviceFrame.cmd = ImageXz;
            appDeviceFrame.Img = img;
            appDeviceFrame.para = new ClientAppData();
            appDeviceFrame.para.robot_id = robotID;
            appDeviceFrame.para.train_mode = mode;
            appDeviceFrame.para.train_sn = sn;
            AcceptClient?.Invoke(appDeviceFrame);
        }

        private void ImageReadyMz(string parsId, Image img, string mode, string sn, RobotName robotName, string robotID)
        {
            AppDeviceFrameEx1 appDeviceFrame = new AppDeviceFrameEx1();
            appDeviceFrame.cmd = ImageMz;
            appDeviceFrame.ParsID = parsId.Split('_')[0];
            appDeviceFrame.Img = img;
            appDeviceFrame.RobotName = robotName;
            appDeviceFrame.para = new ClientAppData();
            appDeviceFrame.para.robot_id = robotID;
            appDeviceFrame.para.train_mode = mode;
            appDeviceFrame.para.train_sn = sn;
            AcceptClient?.Invoke(appDeviceFrame);
#if algorithm3
            MzComplete2(robotID, mode, sn, parsId, appDeviceFrame.ParsID, robotName, img);
#else
            MzComplete(robotID, mode, sn, parsId, appDeviceFrame.ParsID, robotName, img);
#endif
        }

        private void ImageReadyLoc(Image img, string name, string robotID)
        {

        }

        private void Accept3dData(string parsId, string data, string mode, string sn, RobotName robotName, string robotID)
        {
            AppDeviceFrameEx2 appDeviceFrame = new AppDeviceFrameEx2();
            appDeviceFrame.cmd = Data3D;
            appDeviceFrame.ParsID = parsId;
            appDeviceFrame.Data = data;
            appDeviceFrame.RobotName = robotName;
            appDeviceFrame.para = new ClientAppData();
            appDeviceFrame.para.robot_id = robotID;
            appDeviceFrame.para.train_mode = mode;
            appDeviceFrame.para.train_sn = sn;
            AcceptClient?.Invoke(appDeviceFrame);
        }

        public void Complete(string id, string robotID, int number)
        {
            lock (completeLock)
            {
                try
                {
                    #region 初始化算法
                    if (imgObj == IntPtr.Zero)
                    {
                        AlgorithmSetting[] algorithmSettings = ServerGlobal.GetAlgorithmSetting();
                        imgObj = Algorithm.ExportObjectFactorycutimg();
                        byte b = 0;
                        puzzleInitComplete = Algorithm.CallOnInitcutimg(imgObj, ref b, enginePath) == 0;
                        if (algObj == IntPtr.Zero)
                        {
                            algObj = Algorithm.ExportObjectFactory();
                            foreach (AlgorithmSetting item in algorithmSettings)
                            {
                                Algorithm.GetModelConfig(algObj, item.FuncName, item.NMS_THRESH, item.CONF_THRESH,
                                    item.INPUT_H_v6, item.INPUT_W_v6, item.EngineName, item.ClassName);
                            }
                            puzzleInitComplete = puzzleInitComplete && Algorithm.CallOnInit(algObj, log) == 0;
                        }
                    }
                    #endregion
                    if (puzzleInitComplete)
                    {
                        string[] ids = id.Split('_');
                        string train = ids[0] + "_" + ids[1];
                        #region 拼图与切图
                        ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
                        {
                            lock (algorithmLock)
                            {
                                try
                                {
                                    #region 读取配置
                                    List<AlgorithmParemeter> paremeters = ServerGlobal.DataBase.GetTs<AlgorithmParemeter>(null, train);
                                    int[] height = null;
                                    List<input_task> inputList = null;
                                    if (paremeters != null)
                                    {
                                        height = paremeters[0].Heights;
                                        inputList = paremeters[0].InputTasks;
                                    }
                                    #endregion
                                    #region 读取Excel配置
                                    else
                                    {
                                        ExcelModel excel = null;
                                        height = train.GetHeight(excelHeightStartRowIndex, ref excel, AddLog);
                                        inputList = new List<input_task>();
                                        inputList.AddRange(excel.GetInputTask());
                                        Dictionary<string, string> nameDict = excel.GetNameDict();
                                        excel = null;
                                        GC.Collect();

                                        paremeters = new List<AlgorithmParemeter>();
                                        paremeters.Add(new AlgorithmParemeter()
                                        {
                                            Heights = height,
                                            InputTasks = inputList,
                                            NameDict = nameDict
                                        });
                                        ServerGlobal.DataBase.SaveTs<AlgorithmParemeter>(paremeters, train);
                                    }
                                    paremeters = null;
                                    #endregion
                                    #region 记录部件总编号
                                    ThreadManager.TaskRun((ThreadEventArgs eventArgs) =>
                                    {
                                        foreach (input_task item in inputList)
                                        {
                                            string key = item.part_str.ToString('\0');
                                            PartsType partsType = PartsDict.GetType(key);
                                            if (!PartsDict.PartDict.ContainsKey(partsType))
                                            {
                                                PartsDict.PartDict.Add(partsType, new List<string>());
                                            }
                                            string value = item.only_str.ToString('\0');
                                            if (!PartsDict.PartDict[partsType].Contains(value))
                                            {
                                                PartsDict.PartDict[partsType].Add(value); 
                                            }
                                            using (StreamWriter sw = new StreamWriter(ServerGlobal.PartsPath))
                                            {
                                                sw.Write(JsonManager.ObjectToJson(PartsDict.PartDict));
                                            }
                                        }
                                        if (!File.Exists(ServerGlobal.PartsPath))
                                        {
                                            using (StreamReader sr = new StreamReader(ServerGlobal.PartsPath))
                                            {
                                                PartsDict.PartDict = JsonManager.JsonToObject<Dictionary<PartsType, List<string>>>(sr.ReadToEnd());
                                            }
                                        }
                                    });
                                    #endregion
                                    #region 清理上次拼切图片
                                    Directory.Delete(UploadService.PuzzlePath, true);
                                    Directory.CreateDirectory(UploadService.PuzzlePath);
                                    #endregion
                                    #region 获取执行计划的计划编号
                                    if (ServerGlobal.StartProjectDict.ContainsKey(robotID))
                                    {
                                        projectID = ServerGlobal.StartProjectDict[robotID].ID;
                                    }
                                    else
                                    {
                                        return;
                                    }
                                    #endregion
                                    #region 生成拼切图用的参数结构体
                                    config_info inputrect = new config_info(ids[0], ids[1], ids[2] + "_" + ids[3] + "_" + ids[4] + "_" + ids[5] + "_" + ids[6],
                                        UploadService.UploadPath + "\\" + id + "_" + robotID, UploadService.PuzzlePath);
                                    #endregion
                                    #region 创建本次计划的部件区域字典
                                    if (!ServerGlobal.PartsDict.ContainsKey(projectID))
                                    {
                                        ServerGlobal.PartsDict.Add(projectID, new Dictionary<string, Rectangle>());
                                    }
                                    foreach (input_task item in inputList)
                                    {
                                        string lengthID = item.only_str.ToString('\0');
                                        if (!ServerGlobal.PartsDict[projectID].ContainsKey(lengthID))
                                        {
                                            ServerGlobal.PartsDict[projectID].Add(lengthID, new Rectangle());
                                        }
                                        ServerGlobal.PartsDict[projectID][lengthID] = new Rectangle(item.x, item.y, item.w, item.h);
                                    }
                                    #endregion
                                    #region 图片存储位置
                                    string imgDataPath = ServerGlobal.DataDir + "\\" + projectID + "\\";
                                    if (!Directory.Exists(imgDataPath))
                                    {
                                        Directory.CreateDirectory(imgDataPath);
                                    }
                                    #endregion
                                    #region 算法：执行拼图与切图算法
                                    AlgorithmProgressMax = 8;
                                    byte b = 0;
                                    int imgLen = 0;
                                    IntPtr result = Algorithm.Callcutimg(imgObj, inputrect, inputList.ToArray(), inputList.Count, height, height.Length, ref imgLen, ref progressIndex, ref b);
                                    #endregion
                                    #region 读取算法结果用的内存信息
                                    int length = Marshal.SizeOf(typeof(input_struct));
                                    long len = result.ToInt64();
                                    AlgorithmProgressMax = imgLen;
                                    #endregion
                                    #region 转存图片，拼好的图片
                                    string[] files = Directory.GetFiles(UploadService.PuzzlePath, "*" + FileManager.FindImageExtend());
                                    string dirPath = ServerGlobal.ImageDir + "\\" + projectID;
                                    if (!Directory.Exists(dirPath))
                                    {
                                        Directory.CreateDirectory(dirPath);
                                    }
                                    string tmpPath = "";
                                    if (files != null)
                                    {
                                        for (int i = 0; i < files.Length; i++)
                                        {
                                            tmpPath = dirPath + "\\" + i + FileManager.GetImageExtend();
                                            try
                                            {
                                                if (File.Exists(tmpPath))
                                                {
                                                    File.Delete(tmpPath);
                                                }
                                                int outCount = 0;
                                            Save:
                                                try
                                                {
                                                    Image img = Image.FromFile(files[i]);
                                                    img.Save(tmpPath, ImageFormat.Jpeg);
                                                }
                                                catch (Exception e)
                                                {
                                                    outCount++;
                                                    if (outCount < 100)
                                                    {
                                                        Thread.Sleep(100);
                                                        goto Save;
                                                    }
                                                    else
                                                    {
                                                        throw e;
                                                    }
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                AddLog("File[" + tmpPath + "]: " + e.Message);
                                            }
                                        }
                                    }
                                    #endregion
#if algorithm2
                                    #region 优化办法，直接使用拼接图片走算法
#if true
                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        #region 线阵图片检测算法
                                        lock (runLock)
                                        {
                                            int resultLen = 0;
                                            List<input_task> tmpList = inputList.FindAll(t => t.imgNO == i + 1);
                                            List<model_struct> model_Structs = ServerGlobal.DataBase.GetTs<model_struct>(null, train, i);
                                            int arrayLen = model_Structs.Count;
                                            IntPtr _result = Algorithm.NewCallgetres(algObj, null, 0, 0, null, 0, 0, null, 0, model_Structs.ToArray(), arrayLen, ref resultLen);
                                            int _length = Marshal.SizeOf(typeof(box_info));
                                            long _len = _result.ToInt64();
                                            for (int j = 0; j < resultLen; j++)
                                            {
                                                box_info value = Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + j * _length)));
                                                for (int k = 0; k < tmpList.Count; k++)
                                                {
                                                    Rectangle rectangle = new Rectangle(tmpList[k].x, tmpList[k].y, tmpList[k].w, tmpList[k].h);
                                                    Rectangle rect = new Rectangle(value.x, value.y, value.w, value.h);
                                                    if (rectangle.Contains(rect))
                                                    {
                                                        string partID = "", onlyID = "";
                                                        partID = tmpList[k].part_str.ToString('\0');
                                                        onlyID = tmpList[k].only_str.ToString('\0');

                                                        ResultData data = new ResultData();
                                                        data.Index = i;
                                                        data.ID = partID;
                                                        data.LengthID = onlyID;
                                                        data.ResultState = value.state_enum == 0 ? ResultState.Normal : ResultState.Abnormal;
                                                        data.Name = value.class_name.ToString('\0');
                                                        data.X = value.x - rectangle.X;
                                                        data.Y = value.y - rectangle.Y;
                                                        data.Width = value.w;
                                                        data.Height = value.h;
                                                        data.Data = ((AlgorithmStateEnum)value.state_enum).ToString();
                                                        data.ImagePath = "";
                                                        data.ResultType = ResultType.Xz;
                                                        if (!ServerGlobal.ResultDict.ContainsKey(projectID))
                                                        {
                                                            ServerGlobal.ResultDict.Add(projectID, new List<ResultData>());
                                                        }
                                                        ServerGlobal.ResultDict[projectID].Add(data);
                                                    }
                                                }
                                            }
                                        }
                                        GC.Collect();
                                        #endregion
                                    }
                                    for (int i = 0; i < imgLen; i++)
                                    {
                                        #region 获取某一位拼切图算法结果
                                        progressIndex = i;
                                        input_struct input_Struct = Marshal.PtrToStructure<input_struct>((IntPtr)((long)(len + i * length)));
                                        #endregion
                                        string imgPath = input_Struct.img_path.ToString('\0');
                                        #region 转存图片，切好的图片
                                        if (!File.Exists(imgPath))
                                        {
                                            return;
                                        }
                                        string imgName = imgPath.Substring(imgPath.LastIndexOf("\\") + 1);
                                        tmpPath = dirPath + "\\" + imgName;
                                        try
                                        {
                                            if (File.Exists(tmpPath))
                                            {
                                                File.Delete(tmpPath);
                                            }
                                            int outCount = 0;
                                        Save:
                                            try
                                            {
                                                Image img = Image.FromFile(imgPath);
                                                img.Save(tmpPath, ImageFormat.Jpeg);
                                            }
                                            catch (Exception e)
                                            {
                                                outCount++;
                                                if (outCount < 100)
                                                {
                                                    Thread.Sleep(100);
                                                    goto Save;
                                                }
                                                else
                                                {
                                                    throw e;
                                                }
                                            }
                                        }
                                        catch (Exception e)
                                        {
                                            AddLog("File[" + tmpPath + "]: " + e.Message);
                                        }
                                        #endregion
                                    }
                                    GC.Collect();
#else
                                    AlgorithmProgressMax += files.Length;
                                    for (int i = 0; i < files.Length; i++)
                                    {
                                        progressIndex = i;
                                        #region 线阵图片检测算法
                                        input_struct input_Struct = new input_struct()
                                        {
                                            imgNO = i + 1,
                                            img_path = files[i].ToCharArray(200),
                                            location_str = "".ToCharArray(50),
                                            only_str = "".ToCharArray(50),
                                            part_location_str = "".ToCharArray(50),
                                            part_str = "".ToCharArray(50),
                                            task_list = new int[] { 0, -1, -1, -1, -1, -1, -1, -1, -1, -1 }
                                        };
                                        List<input_task> tmpList = inputList.FindAll(t => t.imgNO == i + 1);

                                        lock (runLock)
                                        {
                                            int resultLen = 0;
                                            List<model_struct> model_Structs = new List<model_struct>();
                                            foreach (input_task item in tmpList)
                                            {
                                                List<model_struct> list = GetModelStructs(item.only_str.ToString('\0'), train);
                                                for (int j = 0; j < list.Count; j++)
                                                {
                                                    list[j] = new model_struct(list[j].class_name.ToString('\0'), list[j].x + item.x, list[j].y + item.y, list[j].w, list[j].h);
                                                }
                                                model_Structs.AddRange(list);
                                            }
                                            int arrayLen = model_Structs.Count;
                                            IntPtr _result = Algorithm.Callgetres(algObj, input_Struct, model_Structs.ToArray(), arrayLen, ref resultLen, ref tmp, ref logByte[0]);
                                            int _length = Marshal.SizeOf(typeof(box_info));
                                            long _len = _result.ToInt64();
                                            for (int j = 0; j < resultLen; j++)
                                            {
                                                box_info value = Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + j * _length)));
                                                for (int k = 0; k < tmpList.Count; k++)
                                                {
                                                    Rectangle rectangle = new Rectangle(tmpList[k].x, tmpList[k].y, tmpList[k].w, tmpList[k].h);
                                                    Rectangle rect = new Rectangle(value.x, value.y, value.w, value.h);
                                                    if (rectangle.Contains(rect))
                                                    {
                                                        string partID = "", onlyID = "";
                                                        partID = tmpList[k].part_str.ToString('\0');
                                                        onlyID = tmpList[k].only_str.ToString('\0');

                                                        ResultData data = new ResultData();
                                                        data.Index = i;
                                                        data.ID = partID;
                                                        data.LengthID = onlyID;
                                                        data.ResultState = value.state_enum == 0 ? ResultState.Normal : ResultState.Abnormal;
                                                        data.Name = value.class_name.ToString('\0');
                                                        data.X = value.x - rectangle.X;
                                                        data.Y = value.y - rectangle.Y;
                                                        data.Width = value.w;
                                                        data.Height = value.h;
                                                        data.Data = ((AlgorithmStateEnum)value.state_enum).ToString();
                                                        data.ImagePath = "";
                                                        data.ResultType = ResultType.Xz;
                                                        if (!ServerGlobal.ResultDict.ContainsKey(projectID))
                                                        {
                                                            ServerGlobal.ResultDict.Add(projectID, new List<ResultData>());
                                                        }
                                                        ServerGlobal.ResultDict[projectID].Add(data);
                                                    }
                                                }
                                            }
                                        }
                                        GC.Collect();
                                        #endregion
                                    }
                                    for (int i = 0; i < imgLen; i++)
                                    {
                                        #region 获取某一位拼切图算法结果
                                        progressIndex++;
                                        input_struct input_Struct = Marshal.PtrToStructure<input_struct>((IntPtr)((long)(len + i * length)));
                                        #endregion
                                        string imgPath = input_Struct.img_path.ToString('\0');
                                        #region 转存图片，切好的图片
                                        if (!File.Exists(imgPath))
                                        {
                                            return;
                                        }
                                        string imgName = imgPath.Substring(imgPath.LastIndexOf("\\") + 1);
                                        tmpPath = dirPath + "\\" + imgName;
                                        try
                                        {
                                            if (File.Exists(tmpPath))
                                            {
                                                File.Delete(tmpPath);
                                            }
                                            File.Copy(imgPath, tmpPath);
                                        }
                                        catch (Exception e)
                                        {
                                            AddLog("File[" + tmpPath + "]: " + e.Message);
                                        }
                                        #endregion
                                    }
                                    GC.Collect();
#endif
                                    #endregion
#else
                                    #region 接口定义的算法方法
                                    for (int i = 0; i < imgLen; i++)
                                    {
                                        #region 获取某一位拼切图算法结果
                                        progressIndex = i;
                                        input_struct input_Struct = Marshal.PtrToStructure<input_struct>((IntPtr)((long)(len + i * length)));
                                        #endregion
                                        string imgPath = input_Struct.img_path.ToString('\0');
                                        #region 转存图片，切好的图片
                                        if (!File.Exists(imgPath))
                                        {
                                            return;
                                        }
                                        string imgName = imgPath.Substring(imgPath.LastIndexOf("\\") + 1);
                                        tmpPath = dirPath + "\\" + imgName;
                                        try
                                        {
                                            if (File.Exists(tmpPath))
                                            {
                                                File.Delete(tmpPath);
                                            }
                                            File.Copy(imgPath, tmpPath);
                                        }
                                        catch (Exception e)
                                        {
                                            AddLog("File[" + tmpPath + "]: " + e.Message);
                                        }
                                        #endregion
                                        #region 线阵图片检测算法
                                        AlgorithmRun(tmpPath, train, input_Struct, inputList, ref tmp);
                                        #endregion
                                    }
                                    GC.Collect();
                                    #endregion
#endif
                                    if (ServerGlobal.RgvList.ContainsKey(robotID))
                                    {
                                        ServerGlobal.RgvList[robotID].Log += "线阵数据分析完成\r\n";
                                    }
                                    #region 算法结束
                                    ThreadManager.BackTask((int i, ThreadEventArgs eventArgs) =>
                                    {
                                        if (i % 2 == 0)
                                        {
                                            if (ServerGlobal.StartProjectDict.ContainsKey(robotID))
                                            {
                                                if (ServerGlobal.RgvList.ContainsKey(robotID))
                                                {
                                                    if (ServerGlobal.RgvList[robotID].Job != "上位机等待任务中")
                                                    {
                                                        return;
                                                    }
                                                }
                                                ServerGlobal.RobotTaskEnd(robotID);
                                            }
                                        }
                                    }); 
                                    #endregion
                                }
                                catch (Exception e)
                                {
                                    MessageBox.Show(e.Message);
                                }
                            }
                        });
                        #endregion
                    }
                }
                catch (Exception e)
                {
                    AddLog(e.StackTrace);
                } 
            }
        }

        public void Complete2(string id, string robotID, int number)
        {
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                AlgorithmRun2(id, robotID, number);
            });
        }

        public void MzComplete(string robotID, string mode, string sn, string name, string parsId, RobotName robotName, Image img)
        {
            if (ServerGlobal.StartProjectDict.ContainsKey(robotID))
            {
                #region 读取配置
                string train = mode + "_" + sn;
                List<AlgorithmParemeter> paremeters = ServerGlobal.DataBase.GetTs<AlgorithmParemeter>(null, train);
                if (paremeters == null)
                {
                    return;
                }
                int tmp = 0;
                string dirPath = ServerGlobal.ImageDir + "\\" + ServerGlobal.StartProjectDict[robotID].ID + "\\" + robotName.ToString();
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                dirPath += "\\" + name + FileManager.SaveImageExtend();
                List<input_task> inputList = paremeters[0].InputTasks;
                int index = inputList.FindIndex(i => i.only_str.ToString('\0') == parsId);
                input_task task;
                if (index < 0)
                {
                    task = new input_task();
                    task.location_str = "".ToCharArray(50);
                    task.only_str = parsId.ToCharArray(50);
                    task.part_location_str = "".ToCharArray(50);
                    task.part_str = PartsDict.GetID(PartsDict.GetPartsType(parsId)).ToCharArray(50);
                    task.task_list = new int[10];
                    for (int i = 0; i < task.task_list.Length; i++)
                    {
                        task.task_list[i] = i < 2 ? i : -1;
                    }
                }
                else
                {
                    task = inputList[index]; 
                }
                List<char> dirList = new List<char>();
                dirList.AddRange(dirPath.ToCharArray());
                for (int i = dirList.Count; dirList.Count < 200; i++)
                {
                    dirList.Add('\0');
                }
                input_struct input = new input_struct();
                input.imgNO = int.Parse(name[4].ToString()) - 1;
                input.img_path = dirList.ToArray();
                input.location_str = task.location_str;
                input.only_str = task.only_str;
                input.part_location_str = task.part_location_str;
                input.part_str = task.part_str;
                input.task_list = task.task_list;
                if (backList == null || backList.Count == 0 || backList[0].Mode != mode || backList[0].Sn != sn)
                {
                    backList = ServerGlobal.DataBase.GetTs<BackDataModel>(null, train);
                }
                #endregion
                img.Save(dirPath);
                ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
                {
                    AlgorithmRun(dirPath, train, input, inputList, ref tmp, false);
                });
                Thread.Sleep(500);
            }
        }

        public void MzComplete2(string robotID, string mode, string sn, string name, string parsId, RobotName robotName, Image img)
        {
            string dirPath = ServerGlobal.ImageDir + "\\" + ServerGlobal.StartProjectDict[robotID].ID + "\\" + robotName.ToString();
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            dirPath += "\\" + name + FileManager.SaveImageExtend();
            img.Save(dirPath);
            ThreadManager.TaskRun((ThreadEventArgs threadEventArgs) =>
            {
                AlgorithmRun2(mode + "_" + sn + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm"), robotID, 0, dirPath, parsId);
            });
        }

        private void AddLog(string log)
        {
            using (StreamWriter sw = new StreamWriter(UploadService.PuzzlePath + "Log.txt", true))
            {
                sw.WriteLine(log);
            }
        }

        private void AlgorithmRun(string imgPath, string train, input_struct input_Struct, List<input_task> inputList, ref int tmp, bool isXz = true)
        {
            lock (runLock)
            {
                int resultLen = 0;
                string partID = input_Struct.part_str.ToString('\0');
                string onlyID = input_Struct.only_str.ToString('\0');
                List<model_struct> model_Structs = GetModelStructs(onlyID, train);
                IntPtr _result = Algorithm.Callgetres(algObj, imgPath, input_Struct.img_path.ToString('\0'), "", input_Struct.task_list, input_Struct.task_list.Length, model_Structs.ToArray(), model_Structs.Count, ref resultLen);
                int _length = Marshal.SizeOf(typeof(box_info));
                long _len = _result.ToInt64();
                for (int j = 0; j < resultLen; j++)
                {
                    box_info value = Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + j * _length)));
                    ResultData data = new ResultData();
                    data.Index = -1;
                    data.ID = partID;
                    data.LengthID = onlyID;
                    data.ResultState = value.state_enum == 0 ? ResultState.Normal : ResultState.Abnormal;
                    data.Name = value.class_name.ToString('\0');
                    data.X = value.x;
                    data.Y = value.y;
                    data.Width = value.w;
                    data.Height = value.h;
                    data.Data = ((AlgorithmStateEnum)value.state_enum).ToString();
                    data.ImagePath = imgPath;
                    data.ResultType = isXz ? ResultType.Xz : ResultType.Mz;
                    ServerGlobal.AddResultData(projectID, data, inputList);
                    BackDataModel model = backList.Find(l => l.OnlyID == onlyID);
                    int index;
                    if (model != null && int.TryParse(model.Location.Split('_')[0], out index))
                    {
                        data.Index = index; 
                    }
                }
                #region 记录螺丝位置
                if (model_Structs.Count == 0 && isXz)
                {
                    SetModelStructs(onlyID, train);
                }
                #endregion 
            }
        }

        private void AlgorithmRun2(string id, string robotID, int number, string partIdPath = "", string parsId = "")
        {
            lock (completeLock)
            {
                #region 清理上次拼切图片
                Directory.Delete(UploadService.PuzzlePath, true);
                Directory.CreateDirectory(UploadService.PuzzlePath);
                #endregion
                #region 拼切图
                Process process = null;
                if (string.IsNullOrEmpty(partIdPath))
                {
                    process = new Process();
                    process.StartInfo.FileName = Application.StartupPath + "\\PuzzleConsole.exe";
                    process.StartInfo.Arguments = id + " " + robotID + " " + number;
                    process.Start();
                } 
                #endregion
                if (algObj == IntPtr.Zero)
                {
                    algObj = Algorithm.ExportObjectFactory();
                    puzzleInitComplete = puzzleInitComplete && Algorithm.CallOnInit(algObj, null) == 0;
                }
                if (puzzleInitComplete)
                {
                    if (process != null)
                    {
                        process.WaitForExit();
                        progressIndex = 0;
                    }
                    string[] ids = id.Split('_');
                    string mode = ids[0];
                    string sn = ids[1];
                    string train = mode + "_" + sn;
                    #region 读取配置
                    ExeConfigurationFileMap config = new ExeConfigurationFileMap();
                    config.ExeConfigFilename = Application.StartupPath + "\\PicturePuzzleService.dll.config";
                    Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(config, ConfigurationUserLevel.None);
                    string puzzlePath = configuration.AppSettings.Settings["PuzzlePath"].Value;

                    List<AlgorithmParemeter> paremeters = ServerGlobal.DataBase.GetTs<AlgorithmParemeter>(null, train);
                    int[] height = null;
                    List<input_task> inputList = null;
                    if (paremeters != null)
                    {
                        height = paremeters[0].Heights;
                        inputList = paremeters[0].InputTasks;
                    }
                    #endregion
                    #region 读取Excel配置
                    else
                    {
                        ExcelModel excel = null;
                        height = train.GetHeight(excelHeightStartRowIndex, ref excel, AddLog);
                        inputList = new List<input_task>();
                        inputList.AddRange(excel.GetInputTask());
                        Dictionary<string, string> nameDict = excel.GetNameDict();
                        excel = null;
                        GC.Collect();

                        paremeters = new List<AlgorithmParemeter>();
                        paremeters.Add(new AlgorithmParemeter()
                        {
                            Heights = height,
                            InputTasks = inputList,
                            NameDict = nameDict
                        });
                        ServerGlobal.DataBase.SaveTs<AlgorithmParemeter>(paremeters, train);
                    }
                    paremeters = null;
                    #endregion
                    AlgorithmProgressMax = inputList.Count;
                    int index = inputList.FindIndex(i => i.only_str.ToString('\0') == parsId);
                    if (string.IsNullOrEmpty(partIdPath))
                    {
                        Dictionary<string, string[]> parts = new Dictionary<string, string[]>();
                        foreach (input_task input in inputList)
                        {
                            AlgorithmRun2(mode, sn, partIdPath, input, puzzlePath, parts);
                            progressIndex++;
                        }
                        parts.Clear(); 
                    }
                    else if (index > -1)
                    {
                        AlgorithmRun2(mode, sn, partIdPath, inputList[index]);
                        progressIndex++;
                    }
                    else
                    {
                        return;
                    }
                    if (!File.Exists(ServerGlobal.PartsPath))
                    {
                        using (StreamReader sr = new StreamReader(ServerGlobal.PartsPath))
                        {
                            PartsDict.PartDict = JsonManager.JsonToObject<Dictionary<PartsType, List<string>>>(sr.ReadToEnd());
                        }
                    }
                }
            }
        }

        private void AlgorithmRun2(string mode, string sn, string imgPath, input_task input, string puzzlePath = null, Dictionary<string, string[]> parts = null)
        {
            string part = input.part_str.ToString('\0');
            string partId = input.only_str.ToString('\0');
            #region 记录部件总编号
            PartsType partsType = PartsDict.GetType(part);
            if (!PartsDict.PartDict.ContainsKey(partsType))
            {
                PartsDict.PartDict.Add(partsType, new List<string>());
            }
            if (!PartsDict.PartDict[partsType].Contains(partId))
            {
                PartsDict.PartDict[partsType].Add(partId);
            }
            using (StreamWriter sw = new StreamWriter(ServerGlobal.PartsPath))
            {
                sw.Write(JsonManager.ObjectToJson(PartsDict.PartDict));
            }
            #endregion
            #region 验证部件图片是否存在
            if (string.IsNullOrEmpty(imgPath))
            {
                string partPath = puzzlePath + "\\" + part;
                if (!Directory.Exists(partPath))
                {
                    Console.WriteLine("未找到部件文件夹：" + partPath);
                    return;
                }
                if (!parts.ContainsKey(partPath))
                {
                    parts.Add(partPath, Directory.GetFiles(partPath));
                }
                imgPath = Array.Find(parts[partPath], (string file) => { return file.Contains(partId); });
                if (string.IsNullOrEmpty(imgPath))
                {
                    Console.WriteLine("未找到部件图片：" + imgPath);
                    return;
                }
            }
            #endregion
            #region 验证往次结果目录
            string modelPath = Application.StartupPath + "\\model\\" + mode + "\\" + sn + "\\" + partId + "\\";
            if (!Directory.Exists(modelPath))
            {
                Directory.CreateDirectory(modelPath);
            }
            #endregion
            #region 参数解析
            List<int> taskIds = new List<int>();
            for (int i = 0; i < input.task_list.Length; i++)
            {
                if (input.task_list[i] < 0)
                {
                    break;
                }
                taskIds.Add(input.task_list[i]);
            }

            string mubanPath = Application.StartupPath + "\\muban\\";
            string upImgPath = mubanPath + sn + "\\" + partId + ".jpg";
            if (!File.Exists(upImgPath))
            {
                upImgPath = "";
            }

            string csvPath = mubanPath + mode + "_" + sn + ".csv";
            List<model_struct> models = new List<model_struct>();
            if (File.Exists(csvPath))
            {
                using (StreamReader sr = new StreamReader(csvPath))
                {
                    while (true)
                    {
                        string row = sr.ReadLine();
                        if (string.IsNullOrEmpty(row))
                        {
                            break;
                        }
                        if (row.IndexOf(partId) == 0)
                        {
                            string pars = row.Substring(row.IndexOf(",") + 1);
                            string name = pars.Substring(0, pars.IndexOf(","));
                            string loc = pars.Substring(pars.IndexOf(",") + 1);
                            loc = loc.Replace("\"", "");
                            string[] locs = loc.Split(';');
                            Console.WriteLine("找到模板参数: " + row);
                            if (locs != null)
                            {
                                foreach (string item in locs)
                                {
                                    string[] s = item.Split(',');
                                    if (s != null && s.Length > 3)
                                    {
                                        model_struct model = new model_struct();
                                        SetErrorLocation(
                                            int.TryParse(s[0], out model.x) &&
                                            int.TryParse(s[1], out model.y) &&
                                            int.TryParse(s[2], out model.w) &&
                                            int.TryParse(s[3], out model.h), partId, mode, sn, item,
                                            () =>
                                            {
                                                model.class_name = new char[50];
                                                for (int i = 0; i < name.Length; i++)
                                                {
                                                    model.class_name[i] = name[i];
                                                }
                                                models.Add(model);
                                            });
                                    }
                                }
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("缺失的配置文件: " + csvPath);
            }

            string uncorrectPath = mubanPath + "uncorrect.txt";
            if (File.Exists(uncorrectPath))
            {
                string uncorrect = "";
                using (StreamReader sr = new StreamReader(uncorrectPath))
                {
                    uncorrect = sr.ReadToEnd();
                }
                if (!uncorrect.Contains(partId))
                {
                    List<int> tmpTask = new List<int>();
                    tmpTask.AddRange(taskIds);
                    tmpTask.Add(100);
                    taskIds = tmpTask;
                    tmpTask = null;
                    Console.WriteLine("部件编号[" + partId + "]需要图像校正");
                }
            }
            else
            {
                Console.WriteLine("缺失的配置文件: " + uncorrectPath);
            }
            #endregion
            #region 算法调用
            int len = 0;
            IntPtr rPtr = Algorithm.Callgetres(algObj, imgPath, upImgPath, modelPath, taskIds.ToArray(), taskIds.Count, models.ToArray(), models.Count, ref len);
            models = null;
            taskIds = null;
            GC.Collect();
            #endregion
            #region 算法返回值解析
            bool isNormal = true;
            if (rPtr == IntPtr.Zero)
            {
                Console.WriteLine("未能获得算法结果");
                return;
            }
            long _len = rPtr.ToInt64();
            for (int i = 0; i < len; i++)
            {
                box_info value = Marshal.PtrToStructure<box_info>((IntPtr)((long)(_len + i * box_length)));
                Rectangle rectangle = new Rectangle(input.x, input.y, input.w, input.h);
                Rectangle rect = new Rectangle(value.x, value.y, value.w, value.h);
                if (rectangle.Contains(rect))
                {
                    ResultData data = new ResultData();
                    data.Index = i;
                    data.ID = part;
                    data.LengthID = partId;
                    data.ResultState = value.state_enum == 0 ? ResultState.Normal : ResultState.Abnormal;
                    data.Name = value.class_name.ToString('\0');
                    data.X = value.x - rectangle.X;
                    data.Y = value.y - rectangle.Y;
                    data.Width = value.w;
                    data.Height = value.h;
                    data.Data = ((AlgorithmStateEnum)value.state_enum).ToString();
                    data.ImagePath = "";
                    data.ResultType = ResultType.Xz;
                    if (!ServerGlobal.ResultDict.ContainsKey(projectID))
                    {
                        ServerGlobal.ResultDict.Add(projectID, new List<ResultData>());
                    }
                    ServerGlobal.ResultDict[projectID].Add(data);
                }
                if (isNormal && value.state_enum != 0)
                {
                    isNormal = false;
                }
            }
            #endregion
            #region 记录结果
            if (isNormal)
            {
                string[] files = Directory.GetFiles(modelPath);
                if (files != null && files.Length >= modelFileMax)
                {
                    int d = files.Length - modelFileMax + 1;
                    string name = "";
                    for (int i = 0; i < d; i++)
                    {
                        name = modelPath + i.ToString("00") + ".jpg";
                        if (File.Exists(name))
                        {
                            File.Delete(name);
                        }
                    }
                    files = Directory.GetFiles(modelPath);
                    for (int i = 0; i < files.Length; i++)
                    {
                        File.Move(files[i], modelPath + i.ToString("00") + ".jpg");
                        File.Delete(files[i]);
                    }
                }
                int index = files != null ? files.Length : 0;
                File.Copy(imgPath, modelPath + index.ToString("00") + ".jpg");
            }
            #endregion
        }

        private void SetErrorLocation(bool b, string partId, string mode, string sn, string loc, Action act)
        {
            if (b)
            {
                act();
            }
            else
            {
                Console.WriteLine($"模板参数解析失败: {mode}.{sn}.{partId} >> {loc}");
            }
        }

        private void SetModelStructs(string id, string train)
        {
            if (ServerGlobal.ResultDict.ContainsKey(id))
            {
                List<ResultData> list = ServerGlobal.ResultDict[id];
                List<model_struct> structs = new List<model_struct>();
                for (int i = 0; i < list.Count; i++)
                {
                    model_struct model_Struct = new model_struct();
                    model_Struct.class_name = list[i].Name.ToCharArray(50);
                    model_Struct.x = list[i].X;
                    model_Struct.y = list[i].Y;
                    model_Struct.w = list[i].Width;
                    model_Struct.h = list[i].Height;
                    structs.Add(model_Struct);
                }
                ServerGlobal.DataBase.SaveTs<model_struct>(structs, Application.StartupPath + @"\data\" + train, id);
            }
        }

        private List<model_struct> GetModelStructs(string id, string train)
        {
            List<model_struct> list = ServerGlobal.DataBase.GetTs<model_struct>(null, train, id);
            return list ?? new List<model_struct>();
        }
    }

    internal class AppDeviceFrameEx : AppDeviceFrame
    {
        public Image Img { get; set; }
    }

    internal class AppDeviceFrameEx1 : AppDeviceFrameEx
    {
        public string ParsID { get; set; }
        public RobotName RobotName { get; set; }
    }

    internal class AppDeviceFrameEx2 : AppDeviceFrameEx1
    {
        public string Data { get; set; }
    }
}
