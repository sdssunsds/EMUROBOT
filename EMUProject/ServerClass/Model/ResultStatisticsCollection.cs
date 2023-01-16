using GW4;
using System.Collections.Generic;

namespace Project.ServerClass.Model
{
    public class ResultStatisticsCollection
    {
        private object lockObj = new object();
        private List<ResultStatisticsModel> list = null;
        private Dictionary<string, int> idIndex = null;
        public ResultStatisticsCollection(List<ResultData> datas)
        {
            list = new List<ResultStatisticsModel>();
            idIndex = new Dictionary<string, int>();
            if (datas != null)
            {
                foreach (ResultData item in datas)
                {
                    if (!idIndex.ContainsKey(item.ID))
                    {
                        lock (lockObj)
                        {
                            idIndex.Add(item.ID, list.Count);
                            list.Add(new ResultStatisticsModel());
                        }
                    }
                    list[idIndex[item.ID]].AddResult(item);
                } 
            }
        }
        public ResultStatisticsModel this[int i]
        {
            get { return list[i]; }
            set { list[i] = value; }
        }
        public ResultStatisticsModel this[string id]
        {
            get
            {
                if (idIndex.ContainsKey(id))
                {
                    return list[idIndex[id]];
                }
                return null;
            }
            set
            {
                if (!idIndex.ContainsKey(id))
                {
                    idIndex.Add(id, 0);
                }
                lock (lockObj)
                {
                    idIndex[id] = list.Count;
                    list.Add(value); 
                }
            }
        }
        public List<object> BindResultDatas
        {
            get
            {
                List<dynamic> dynamics = new List<dynamic>();
                List<string> names = new List<string>();
                foreach (ResultStatisticsModel item in list)
                {
                    foreach (KeyValuePair<string, int> keyValuePair in item.DataCount)
                    {
                        if (names.IndexOf(keyValuePair.Key) < 0)
                        {
                            names.Add(keyValuePair.Key);
                        }
                    }
                }
                foreach (ResultStatisticsModel item in list)
                {
                    dynamic model = new TrendsModel();
                    model.Name = item.Name;
                    model.ID = item.ID;
                    model.OnlyID = item.OnlyID;
                    foreach (string name in names)
                    {
                        TrendsModel tm = model as TrendsModel;
                        if (item.DataCount.ContainsKey(name))
                        {
                            tm.Propertys.Add(name, item.DataCount[name]);
                        }
                        else
                        {
                            tm.Propertys.Add(name, 0);
                        }
                    }
                    dynamics.Add(model);
                }
                return dynamics.ToObjects();
            }
        }
    }
}
