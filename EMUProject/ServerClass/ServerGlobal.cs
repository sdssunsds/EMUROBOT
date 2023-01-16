using AlgorithmLib;
using EMU.Interface;
using EMU.Util;
using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Project.ServerClass
{
    public class ServerGlobal
    {
        private static AlgorithmSetting[] algorithmSetting = null;

        public readonly static string AlgorithmSettingPath = Application.StartupPath + @"\algorithm.setting";
        public readonly static string BakDir = Application.StartupPath + @"\Bak\" + DateTime.Now.ToString("yyyyMMdd");
        public readonly static string DataDir = Application.StartupPath + @"\Data";
        public readonly static string ImageDir = Application.StartupPath + @"\Image";
        public readonly static string OprationPath = Application.StartupPath + @"\opration.ini";
        public readonly static string PartsPath = Application.StartupPath + @"\parts.json";
        public static string SelectRobotID = "";
        public static IDataBase DataBase = null;
        public static List<string> LinkRobotList = new List<string>();
        public static List<string> StartRobotList = new List<string>();
        public static List<ProjectModel> ProjectModels = new List<ProjectModel>();
        public static Dictionary<string, string> RgvJob = new Dictionary<string, string>();
        public static Dictionary<string, SocketRgvInfo> RgvList = new Dictionary<string, SocketRgvInfo>();
        public static Dictionary<string, ProjectModel> StartProjectDict = new Dictionary<string, ProjectModel>();
        public static Dictionary<string, Action<int, int>> ProgressProjectDict = new Dictionary<string, Action<int, int>>();
        public static Dictionary<string, Dictionary<string, Rectangle>> PartsDict = new Dictionary<string, Dictionary<string, Rectangle>>();
        public static Dictionary<string, List<ResultData>> ResultDict = new Dictionary<string, List<ResultData>>();
        public static event Action<string, bool> RobotTaskEvent;

        public static void AddResultData(string projectID, ResultData data, List<input_task> inputList)
        {
            if (!ResultDict.ContainsKey(projectID))
            {
                ResultDict.Add(projectID, new List<ResultData>());
            }
            ResultDict[projectID].Add(data);
            input_task input = inputList.Find(i => i.only_str.ToString('\0') == data.LengthID);
            data.Index = input.imgNO - 1;
        }

        public static void RobotTaskStart(string robotID)
        {
            RobotTaskEvent?.Invoke(robotID, true);
        }

        public static void RobotTaskEnd(string robotID)
        {
            StartProjectDict[robotID].ProjectState = ProjectState.检修完成;
            StartProjectDict[robotID].TestEnd = DateTime.Now;
            StartProjectDict.Remove(robotID);
            RobotTaskEvent?.Invoke(robotID, false);
        }

        public static void SetDataGridViewHead(DataGridView dgv)
        {
            dgv.Columns[0].HeaderText = "部件名称";
            dgv.Columns[1].Visible = dgv.Columns[2].Visible = false;
        }

        public static string[] GetResultDataArray()
        {
            return Enum.GetNames(typeof(AlgorithmLib.AlgorithmStateEnum));
        }

        public static AlgorithmSetting[] GetAlgorithmSetting()
        {
            if (algorithmSetting == null)
            {
                if (File.Exists(AlgorithmSettingPath))
                {
                    using (StreamReader sr = new StreamReader(AlgorithmSettingPath))
                    {
                        string json = sr.ReadToEnd();
                        algorithmSetting = JsonManager.JsonToObject<AlgorithmSetting[]>(json);
                    }
                }
                else
                {
                    algorithmSetting = new AlgorithmSetting[5];
                    algorithmSetting[0] = new AlgorithmSetting()
                    {
                        FuncName = "screw",
                        NMS_THRESH = 0.45f,
                        CONF_THRESH = 0.25f,
                        INPUT_H_v6 = 320,
                        INPUT_W_v6 = 320,
                        EngineName = Application.StartupPath + @"\model\yolov5m_screw.engine",
                        ClassName = "Philatelic_screws,Hex_screws,bolt,rivet"
                    };
                    algorithmSetting[1] = new AlgorithmSetting()
                    {
                        FuncName = "foreign_body",
                        NMS_THRESH = 0.45f,
                        CONF_THRESH = 0.25f,
                        INPUT_H_v6 = 640,
                        INPUT_W_v6 = 640,
                        EngineName = Application.StartupPath + @"\model\yolov5m_foreign_body.engine",
                        ClassName = "foreign_body"
                    };
                    algorithmSetting[2] = new AlgorithmSetting()
                    {
                        FuncName = "locking_wire",
                        NMS_THRESH = 0.45f,
                        CONF_THRESH = 0.25f,
                        INPUT_H_v6 = 640,
                        INPUT_W_v6 = 640,
                        EngineName = Application.StartupPath + @"\model\yolov5s_locking_wire.engine",
                        ClassName = "locking_wire,locking_wire_break"
                    };
                    algorithmSetting[3] = new AlgorithmSetting()
                    {
                        FuncName = "oil_level",
                        NMS_THRESH = 0.6f,
                        CONF_THRESH = 0.2f,
                        INPUT_H_v6 = 1024,
                        INPUT_W_v6 = 1024,
                        EngineName = Application.StartupPath + @"\model\yolov5n_oil.engine",
                        ClassName = "yellow,scale,oil_guage,liquid_level"
                    };
                    algorithmSetting[4] = new AlgorithmSetting()
                    {
                        FuncName = "scratch",
                        NMS_THRESH = 0.45f,
                        CONF_THRESH = 0.25f,
                        INPUT_H_v6 = 608,
                        INPUT_W_v6 = 608,
                        EngineName = Application.StartupPath + @"\model\yolov5_scratch.engine",
                        ClassName = "scratch"
                    };
                    using (StreamWriter sw = new StreamWriter(AlgorithmSettingPath))
                    {
                        sw.Write(JsonManager.ObjectToJson(algorithmSetting));
                    }
                }
            }
            return algorithmSetting;
        }
    }

    public class DataList
    {
        private static DataList instance;
        public static DataList Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = ReadList();
                }
                return instance;
            }
        }

        public List<string> addressList { get; set; }
        public Dictionary<string, List<string>> rodeList { get; set; }
        public List<string> modeList { get; set; }
        public Dictionary<string, List<string>> snList { get; set; }
        private DataList()
        {
            addressList = new List<string>();
            rodeList = new Dictionary<string, List<string>>();
            modeList = new List<string>();
            snList = new Dictionary<string, List<string>>();
        }
        private static DataList ReadList()
        {
            return ServerGlobal.DataBase.GetT<DataList>(null);
        }
        public void Save()
        {
            ServerGlobal.DataBase.SaveTs<DataList>(null, instance);
        }
    }

    public class AlgorithmSetting
    {
        public int INPUT_H_v6 { get; set; }
        public int INPUT_W_v6 { get; set; }
        public float NMS_THRESH { get; set; }
        public float CONF_THRESH { get; set; }
        public string FuncName { get; set; }
        public string EngineName { get; set; }
        public string ClassName { get; set; }
    }
}
