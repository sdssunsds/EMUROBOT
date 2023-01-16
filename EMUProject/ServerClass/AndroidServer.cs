using EMU.Util;
using Project.ServerClass.Model;
using System;
using System.Collections.Generic;
using System.Net.Sockets;

namespace Project.ServerClass
{
    public class AndroidServer
    {
        private TcpServiceSocket server = null;
        public AndroidServer()
        {
            string[] vs = Properties.Settings.Default.业务服务地址.Split(':');
            server = new TcpServiceSocket(vs[0], int.Parse(vs[1]), 10);
            server.recvMessageEvent = Recv;
            server.Start();
        }
        private void Recv(Socket socket, string msg)
        {
            /*
             * 协议格式：
             *   指令/内容参数1,内容参数2,...,内容参数n/业务端ID
             *   
             *   获取指令：
             *   get_robot/null/业务端ID
             *   get_project/计划中,检修中,检修完成,开始时间,结束时间/业务端ID
             *   get_list/null/业务端ID
             *   get_result/计划ID/业务端ID
             *   
             *   返回指令：
             *   指令/json
             *   
             * 控制指令：
             *   控制指令/机器人ID/mode,sn
             */
            msg.AddLog(EMU.Parameter.LogType.TestLog);
            string[] vs = msg?.Split('/');
            if (vs != null && vs.Length > 2)
            {
                if (vs[0].Contains("get"))
                {
                    string json = "";
                    AndroidCmd androidCmd = (AndroidCmd)Enum.Parse(typeof(AndroidCmd), vs[0]);
                    switch (androidCmd)
                    {
                        case AndroidCmd.get_robot:
                            json = JsonManager.ObjectToJson(ServerGlobal.LinkRobotList);
                            break;
                        case AndroidCmd.get_project:
                            List<ProjectModel> projectModels = ServerGlobal.DataBase.GetTs<ProjectModel>();
                            if (projectModels != null && projectModels.Count > 0)
                            {
                                string[] pars = vs[1].Split(',');
                                List<ProjectModel> list = projectModels.FindAll((ProjectModel p) =>
                                    {
                                        bool none = p.ProjectState == ProjectState.计划中;
                                        bool doing = p.ProjectState == ProjectState.检修中;
                                        bool end = p.ProjectState == ProjectState.检修完成 || p.ProjectState == ProjectState.检修异常;
                                        bool date =
                                            p.ProjectDate >= DateTime.Parse(pars[3]) &&
                                            p.ProjectDate <= DateTime.Parse(pars[4]);
                                        return (bool.Parse(pars[0]) && none && date) ||
                                               (bool.Parse(pars[1]) && doing && date) ||
                                               (bool.Parse(pars[2]) && end && date);
                                    }
                                );
                                json = JsonManager.ObjectToJson(list);
                            }
                            else
                            {
                                json = "没有计划";
                            }
                            break;
                        case AndroidCmd.get_list:
                            json = JsonManager.ObjectToJson(DataList.Instance);
                            break;
                        case AndroidCmd.get_result:
                            if (ServerGlobal.ResultDict.ContainsKey(vs[1]))
                            {
                                json = JsonManager.ObjectToJson(ServerGlobal.ResultDict[vs[1]]); 
                            }
                            else
                            {
                                json = "暂无结果";
                            }
                            break;
                    }
                    if (string.IsNullOrEmpty(json))
                    {
                        string rev = vs[0] + "/" + json;
                        (vs[2] + " send: " + rev).AddLog(EMU.Parameter.LogType.TestLog);
                        server.SendAsync(socket, rev);
                    }
                    else
                    {
                        (vs[2] + " error").AddLog(EMU.Parameter.LogType.TestLog);
                        server.SendAsync(socket, "error");
                    }
                }
                else
                {
                    Cmd cmd = (Cmd)Enum.Parse(typeof(Cmd), vs[0]);
                    if (cmd == Cmd.start_work)
                    {
                        string[] pars = vs[2].Split(',');
                        RobotServer.Instance.Command(cmd, pars[0], pars[1], vs[1]);
                    }
                    else
                    {
                        RobotServer.Instance.Command(cmd, robotID: vs[1]); 
                    }
                }
            }
        }
        private enum AndroidCmd
        {
            get_robot,
            get_project,
            get_list,
            get_result
        }
    }
}
