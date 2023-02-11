﻿using EMU.Parameter;
using EMU.Util;
using GW.Function.FileFunction;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using static EMU.Util.LogManager;

namespace Project
{
    public partial class MainPage : UserControl
    {
        private int reportCount = 0;
        private int resultCount = 0;
        private StringBuilder sb = new StringBuilder();

        public AlgorithmInterface Project { get; set; }

        public MainPage()
        {
            InitializeComponent();
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            tb_redis_url.Text = FileSystem.ReadIniFile("Redis", "Url", "", Project.PathParameter1);
            tb_redis_input.Text = FileSystem.ReadIniFile("Redis", "Input", "", Project.PathParameter1);
            tb_redis_output.Text = FileSystem.ReadIniFile("Redis", "OutPut", "", Project.PathParameter1);
            tb_redis_internal.Text = FileSystem.ReadIniFile("Redis", "Internal", "100", Project.PathParameter1);

            tb_redis_url.KeyDown += Tb_redis_url_KeyDown;
            tb_redis_input.KeyDown += Tb_redis_input_KeyDown;
            tb_redis_output.KeyDown += Tb_redis_output_KeyDown;
            tb_redis_internal.KeyDown += Tb_redis_internal_KeyDown;

            AddLogEvent += LogManager_AddLogEvent;

            Project.RunInterface(tb_redis_url.Text, tb_redis_input.Text, tb_redis_output.Text, int.Parse(tb_redis_internal.Text));
            ThreadManager.BackTask((int startIndex, ThreadEventArgs threadEventArgs) =>
            {
                List<ThreadEventArgs> list = ThreadManager.GetThreadEventArgs();
                ThreadEventArgs eventArgs = list.Find(t => t.ThreadName == Project.RedisThreadName);
                if (eventArgs != null)
                {
                    sb.Clear();
                    GetVariable(Project.inParName1, sb, eventArgs);
                    GetVariable(Project.inParName2, sb, eventArgs);
                    GetVariable(Project.inParName3, sb, eventArgs);
                    object o = eventArgs.GetVariableValue(Project.inParObject, null);
                    if (o != null)
                    {
                        RedisBusiness[] businesses = JsonManager.JsonToObject<RedisBusiness[]>(o.ToString());
                        StringBuilder coordinatesBuilder = new StringBuilder();
                        StringBuilder typeBuilder = new StringBuilder();
                        foreach (RedisBusiness business in businesses)
                        {
                            coordinatesBuilder.AppendLine(business.coordinates);
                            typeBuilder.AppendLine(business.type);
                        }
                        string coordinates = coordinatesBuilder.ToString();
                        string type = typeBuilder.ToString();
                        sb.AppendLine("坐标集合: " + coordinates);
                        sb.AppendLine("坐标类型: " + type);
                    }
                    BeginInvoke(new Action(() =>
                    {
                        tb_redis.Text = sb.ToString();
                    })); 
                }
            });
        }

        private void LogManager_AddLogEvent(string arg1, LogType arg2)
        {
            if (arg2 == LogType.ProcessLog)
            {
                reportCount++;
                BeginInvoke(new Action(() =>
                {
                    if (reportCount >= 5000)
                    {
                        reportCount = 0;
                        tb_redis_report.Text = "";
                    }
                    tb_redis_report.Text += arg1 + "\r\n";
                    tb_redis_report.SelectionStart = tb_redis_report.Text.Length - 1;
                    tb_redis_report.ScrollToCaret();
                }));
            }
            else if (arg2 == LogType.GeneralLog)
            {
                resultCount++;
                BeginInvoke(new Action(() =>
                {
                    if (resultCount >= 5000)
                    {
                        resultCount = 0;
                        tb_redis_result.Text = "";
                    }
                    tb_redis_result.Text += arg1 + "\r\n";
                    tb_redis_result.SelectionStart = tb_redis_result.Text.Length - 1;
                    tb_redis_result.ScrollToCaret();
                }));
            }
        }

        private void tb_redis_url_Leave(object sender, EventArgs e)
        {
            SaveOperation("Url", tb_redis_url.Text);
        }

        private void Tb_redis_url_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveOperation("Url", tb_redis_url.Text);
            }
        }

        private void tb_redis_input_Leave(object sender, EventArgs e)
        {
            SaveOperation("Input", tb_redis_input.Text);
        }

        private void Tb_redis_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveOperation("Input", tb_redis_input.Text);
            }
        }

        private void tb_redis_output_Leave(object sender, EventArgs e)
        {
            SaveOperation("OutPut", tb_redis_output.Text);
        }

        private void Tb_redis_output_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveOperation("OutPut", tb_redis_output.Text);
            }
        }

        private void tb_redis_internal_Leave(object sender, EventArgs e)
        {
            SaveOperation("Internal", tb_redis_internal.Text);
        }

        private void Tb_redis_internal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SaveOperation("Internal", tb_redis_internal.Text);
            }
        }

        private void GetVariable(string name, StringBuilder sb, ThreadEventArgs eventArgs)
        {
            sb.AppendLine(name + ": " + eventArgs.GetVariableValue(name, "")?.ToString());
        }

        private void SaveOperation(string key, string value)
        {
            try
            {
                Project.RunInterface(tb_redis_url.Text, tb_redis_input.Text, tb_redis_output.Text, int.Parse(tb_redis_internal.Text));
                FileSystem.WriteIniFile("Redis", key, value, Project.PathParameter1);
            }
            catch (Exception e)
            {
                AddLog(e.Message, LogType.ErrorLog);
            }
        }
    }
}
