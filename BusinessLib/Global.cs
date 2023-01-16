using EMU.ApplicationData;
using System;
using System.Collections.Generic;

namespace EMU.Business
{
    public class Global
    {
        public static bool RunStat = false;
        public static bool RunBack = false;
        public static bool RunWait = false;
        public static int LineCameraIndex = -1;
        public static int FrontCameraIndex = -1;
        public static int BackCameraIndex = -1;
        public static int HeadDistance = 0;
        public static int ForwardShotCount = 0;
        public static int BackShotCount = 0;

        public const string LinePathName = "Line";
        public const string FrontPathName = "Front";
        public const string BackPathName = "Back";
        public static string Train_Sn = "";
        public static string Train_Mode = "";

        public static ClientAppData TrainPara = new ClientAppData();

        public static List<FowardDataModel> FowardDataModels = null;
        public static List<BackDataModel> BackDataModels = null;

        public static string GetJsonPath()
        {
            return Train_Mode + "_" + Train_Sn;
        }
        public static string GetUploadId()
        {
            return GetJsonPath() + "_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
        }
        public static void SetAlarm(bool isAlarm)
        {
            if (RunBack)
            {
                RunWait = isAlarm;
            }
            else if (isAlarm)
            {
                RunStat = false;
            }
        }
    }
}
