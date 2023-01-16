using System.Collections.Generic;

namespace Project.ServerClass.Model
{
    public class ResultStatisticsModel
    {
        public string ID { get { return ResultDatas[0].ID; } }
        public string OnlyID { get { return ResultDatas[0].LengthID; } }
        public string Name { get { return EMU.Parameter.PartsDict.GetType(ResultDatas[0].ID).ToString(); } }
        public List<ResultData> ResultDatas { get; private set; }
        public Dictionary<string, int> DataCount { get; private set; }
        public ResultStatisticsModel()
        {
            ResultDatas = new List<ResultData>();
            DataCount = new Dictionary<string, int>();
        }
        public void AddResult(ResultData resultData)
        {
            ResultDatas.Add(resultData);
            if (!DataCount.ContainsKey(resultData.Data))
            {
                DataCount.Add(resultData.Data, 0);
            }
            DataCount[resultData.Data]++;
        }
    }
}
