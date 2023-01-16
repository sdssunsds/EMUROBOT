using EMU.Log;
using EMU.Parameter;
using System;
using System.Collections.Generic;

namespace EMU.Util
{
    public class LogManager
    {
        public static Dictionary<LogType, bool> LogTypeDict = new Dictionary<LogType, bool>()
        {
            { LogType.CameraLog, true }, { LogType.ErrorLog, true },
            { LogType.GeneralLog, true }, { LogType.OtherLog, true },
            { LogType.ProcessLog, true }, { LogType.RgvLog, true },
            { LogType.RobotLog, true }, { LogType.TestLog, true }
        };
        public static event Action<string, LogType> AddLogEvent;
        public static void AddLog(string log, LogType type)
        {
            if (LogTypeDict[type])
            {
                AddLogEvent?.Invoke(log, type); 
            }
            UploadLog.Upload(log, type);
        }
    }
}
