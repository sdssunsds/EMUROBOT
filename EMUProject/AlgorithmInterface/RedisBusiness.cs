using AlgorithmLib;
using EMU.Parameter;
using System;
using System.Collections.Generic;
using static EMU.Util.LogManager;

namespace Project
{
    public class RedisBusiness
    {
        public string coordinates { get; set; }
        public string type { get; set; }
        private List<model_struct> taskList = null;
        public List<model_struct> TaskList
        {
            get
            {
                if (taskList == null)
                {
                    taskList = new List<model_struct>();
                    string[] vs = coordinates.Split(';');
                    if (vs != null)
                    {
                        string[] loc = null;
                        foreach (string item in vs)
                        {
                            loc = item.Split(',');
                            if (loc != null && loc.Length > 3)
                            {
                                try
                                {
                                    taskList.Add(new model_struct(type, int.Parse(loc[0]), int.Parse(loc[1]), int.Parse(loc[2]), int.Parse(loc[3])));
                                }
                                catch (Exception)
                                {
                                    AddLog("坐标异常：" + item, LogType.ErrorLog);
                                }
                            }
                            else
                            {
                                AddLog("坐标异常：" + item, LogType.ErrorLog);
                            }
                        }
                    }
                }
                return taskList;
            }
        }
    }
}
