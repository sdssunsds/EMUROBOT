using EMU.ApplicationData;
using EMU.Parameter;
using EMU.Util;
using System;
using System.IO;
using System.Threading;

namespace EMU.BusinessManager
{
    public delegate void HttpServerModInfo(AppDeviceFrame httpdata);
    public class MyHttpServer : HttpServer
    {
        public event HttpServerModInfo HttpServerModInfoEvent;

        public MyHttpServer(int port) : base(port) { }

        public override void handleGETRequest(HttpProcessor p)
        {
            Console.WriteLine("request: {0}", p.http_url);
            p.writeSuccess();
            p.outputStream.WriteLine("<html><body><h1>test server</h1>");
            p.outputStream.WriteLine("Current Time: " + DateTime.Now.ToString());
            p.outputStream.WriteLine("url : {0}", p.http_url);

            p.outputStream.WriteLine("<form method=post action=/form>");
            p.outputStream.WriteLine("<input type=text name=foo value=foovalue>");
            p.outputStream.WriteLine("<input type=submit name=bar value=barvalue>");
            p.outputStream.WriteLine("</form>");
        }

        public override void handlePOSTRequest(HttpProcessor p, StreamReader inputData)
        {
            string data = inputData.ReadToEnd();
            AppRspFrame rsp = new AppRspFrame();
            AppDeviceFrame frame = new AppDeviceFrame();
            //解析命令字
            try
            {
                //将收到的数据JSON序列化
                frame = JsonManager.JsonToObject<AppDeviceFrame>(data);
                HttpServerHelper.Instance.Accept(frame);
                //开始任务
                if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_StartWork))
                {
                    //判断当前机器人状态
                    if (GlobalValues.UserInfo.myDeviceStat == UserEntity.key_DEVICE_INIT ||
                        GlobalValues.UserInfo.myDeviceStat == UserEntity.key_DEVICE_IDLE)
                    {
                        HttpServerModInfoEvent(frame);

                        //回复
                        rsp.cmd = frame.cmd;
                        rsp.para.msg_code = UserEntity.key_DEVICE_IDLE;
                        rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                    }
                    else
                    {
                        //回复
                        rsp.cmd = frame.cmd;
                        rsp.para.msg_code = GlobalValues.UserInfo.myDeviceStat;
                        rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                    }
                }
                //结束任务
                else if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_StopWork))
                {
                    //判断当前机器人状态
                    if (GlobalValues.UserInfo.myDeviceStat == UserEntity.key_DEVICE_IDLE)
                    {
                        //得到数据,发送事件(用事件传递任务命令和任务参数)
                        HttpServerModInfoEvent(frame);

                        //回复
                        rsp.cmd = frame.cmd;
                        rsp.para.msg_code = UserEntity.key_DEVICE_BUSY;
                        rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                    }
                    else
                    {
                        //回复
                        rsp.cmd = frame.cmd;
                        rsp.para.msg_code = GlobalValues.UserInfo.myDeviceStat;
                        rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                    }
                }
                //查询状态
                else if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_DeviceQueryStat))
                {
                    //回复
                    rsp.cmd = frame.cmd;
                    rsp.para.msg_code = GlobalValues.UserInfo.myDeviceStat;
                    rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                }
                //紧急停止
                else if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_DeviceStop))
                {
                    //得到数据,发送事件(用事件传递任务命令和任务参数)
                    HttpServerModInfoEvent(frame);

                    //回复
                    rsp.cmd = frame.cmd;
                    rsp.para.msg_code = GlobalValues.UserInfo.myDeviceStat;
                    rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                }
                //清除报警
                else if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_DeviceClearAlarm))
                {
                    //得到数据,发送事件(用事件传递任务命令和任务参数)
                    HttpServerModInfoEvent(frame);

                    //回复
                    rsp.cmd = frame.cmd;
                    rsp.para.msg_code = GlobalValues.UserInfo.myDeviceStat;
                    rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                }
                //自动充电
                else if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_DevicePowerCharge))
                {
                    //得到数据,发送事件(用事件传递任务命令和任务参数)
                    HttpServerModInfoEvent(frame);

                    //回复
                    rsp.cmd = frame.cmd;
                    rsp.para.msg_code = GlobalValues.UserInfo.myDeviceStat;
                    rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                }
                //回归原点
                else if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_DeviceRZSite))
                {
                    //得到数据,发送事件(用事件传递任务命令和任务参数)
                    HttpServerModInfoEvent(frame);

                    //回复
                    rsp.cmd = frame.cmd;
                    rsp.para.msg_code = UserEntity.key_DEVICE_BUSY;
                    rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);

                    for (int i = 0; i < 5; i++)
                    {
                        Thread.Sleep(1000);
                        if (RobotGlobalInfo.Instance.FrontRobotRunStatMonitor != EquipmentStatus.RUN &&
                            RobotGlobalInfo.Instance.BackRobotRunStatMonitor != EquipmentStatus.RUN)
                        {
                            rsp.para.msg_code = GlobalValues.UserInfo.myDeviceStat;
                            rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                            break;
                        }
                    }
                }
                //设备自检
                else if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_DeviceCheckSelf))
                {
                    //得到数据,发送事件(用事件传递任务命令和任务参数)
                    HttpServerModInfoEvent(frame);

                    //回复
                    rsp.cmd = frame.cmd;
                    rsp.para.msg_code = GlobalValues.UserInfo.myDeviceStat;
                    rsp.para.msg_content = GlobalValues.UserInfo.GetState(rsp.para.msg_code);
                }
                //设备关机
                else if (frame.cmd.StartsWith(AppDeviceFrame.HostCmd_DevicePowerOff))
                {
                    //得到数据,发送事件(用事件传递任务命令和任务参数)
                    HttpServerModInfoEvent(frame);
                    return;
                }
            }
            catch (Exception ex)
            {
                ex.Message.AddLog(LogType.ErrorLog);
            }

            //应答客户端
            string msg = JsonManager.ObjectToJson(rsp);
            ("Http应答：" + msg).AddLog(LogType.OtherLog);
            p.writeSuccess();
            p.outputStream.WriteLine("{0}", msg);
            HttpServerHelper.Instance.Send(rsp);
        }
    }
}
