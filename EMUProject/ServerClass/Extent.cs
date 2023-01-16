using AlgorithmLib;
using GW.Function.ExcelFunction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Project.ServerClass
{
    internal static class Extent
    {
        public static void DataListComboBoxExtent(this DataList dataList, string mode, string sn, ComboBox cb_mode, ComboBox cb_sn)
        {
            cb_mode.Items.AddRange(dataList.modeList?.ToArray());
            if (!string.IsNullOrEmpty(mode))
            {
                int? i = dataList.modeList?.FindIndex(m => m == mode);
                if (i != null && i.Value > -1)
                {
                    cb_mode.SelectedIndex = i.Value;
                }
                else
                {
                    if (dataList.modeList == null)
                    {
                        dataList.modeList = new List<string>();
                    }
                    dataList.modeList.Add(mode);
                    if (dataList.snList == null)
                    {
                        dataList.snList = new Dictionary<string, List<string>>();
                    }
                    dataList.snList.Add(mode, new List<string>());
                    cb_mode.Items.Clear();
                    cb_mode.Items.AddRange(dataList.modeList?.ToArray());
                }
                if (!string.IsNullOrEmpty(sn))
                {
                    if (!dataList.snList.ContainsKey(mode))
                    {
                        dataList.snList.Add(mode, new List<string>());
                    }
                    i = dataList.snList[mode]?.FindIndex(s => s == sn);
                    if (i != null && i.Value > -1)
                    {
                        cb_sn.SelectedIndex = i.Value;
                    }
                    else
                    {
                        if (dataList.snList[mode] == null)
                        {
                            dataList.snList[mode] = new List<string>();
                        }
                        dataList.snList[mode].Add(sn);
                        cb_sn.Items.Clear();
                        cb_sn.Items.AddRange(dataList.snList[mode].ToArray());
                    }
                }
                dataList.Save();
            }
        }
        public static int[] GetHeight(this string train, int excelHeightStartRowIndex, ref ExcelModel excel, Action<string> addLog = null)
        {
            string path = Application.StartupPath + @"\data\";
            int[] height = new int[8];
            excel = null;
            if (File.Exists(path + train + ".xls"))
            {
                excel = new ExcelModel(path + train + ".xls");
            }
            else if (File.Exists(path + train + ".xlsx"))
            {
                excel = new ExcelModel(path + train + ".xlsx");
            }
            else if (Directory.Exists(path + train))
            {
                string[] files = Directory.GetFiles(path + train);
                foreach (string file in files)
                {
                    if (file.Contains(".xls"))
                    {
                        excel = new ExcelModel(file);
                        break;
                    }
                }
            }
            else
            {
                return height;
            }
            try
            {
                for (int i = excelHeightStartRowIndex; i < height.Length; i++)
                {
                    height[i] = int.Parse(excel[1][0][i].Value);
                }
            }
            catch (Exception e)
            {
                addLog?.Invoke(e.StackTrace);
            }
            return height;
        }
        public static input_task[] GetInputTask(this ExcelModel excel)
        {
            List<input_task> list = new List<input_task>();
            for (int i = 2; i < excel[0].RowCount; i++)
            {
                if (string.IsNullOrEmpty(excel[0][i][0].Value))
                {
                    break;
                }
                string[] vs = excel[0][i][24].Value.Split(',');
                AlgorithmTaskEnum[] taskEnums = new AlgorithmTaskEnum[vs.Length];
                for (int j = 0; j < vs.Length; j++)
                {
                    taskEnums[j] = (AlgorithmTaskEnum)int.Parse(vs[j]);
                }
                input_task input = new input_task(excel[0][i][3].Value, excel[0][i][4].Value,
                    excel[0][i][6].Value, GetOnlyID(excel[0][i]),
                    int.Parse(excel[0][i][2].Value), int.Parse(excel[0][i][19].Value), int.Parse(excel[0][i][20].Value),
                    int.Parse(excel[0][i][21].Value), int.Parse(excel[0][i][22].Value), taskEnums);
                list.Add(input);
            }
            return list.ToArray();
        }
        public static Dictionary<string, string> GetNameDict(this ExcelModel excel)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            for (int i = 2; i < excel[0].RowCount; i++)
            {
                if (string.IsNullOrEmpty(excel[0][i][0].Value))
                {
                    break;
                }
                string id = GetOnlyID(excel[0][i]);
                if (!dict.ContainsKey(id))
                {
                    dict.Add(id, excel[0][i][29].Value);
                }
            }
            return dict;
        }
        private static string GetOnlyID(ExcelRow row)
        {
            return "6" + row[2].Value + row[3].Value + row[4].Value.Substring(1) + row[6].Value + row[25].Value + row[26].Value + row[27].Value;
        }
    }
}
