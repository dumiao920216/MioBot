using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using MioBot.Helper;
using System.Web;
using System.ComponentModel;
using MioBot.Func_Ack;
using NLog;
using System.Security.AccessControl;
using System.Drawing.Printing;
using System.Reflection.Metadata;
using MioBot.Bot;

namespace MioBot.Socket
{
    internal class Server
    {
        //初始化日志
        static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();

        //初始化应答列表
        static List<(string qq, string func)> acklist = new();

        public static async void Start()
        {
            //构造监听
            //ConfigHelper configHelper = new();
            var port = ConfigHelper.ReadSetting("Port");
            IPEndPoint ip = new(IPAddress.Any, Convert.ToInt32(port));
            TcpListener server = new(ip);

            //启动服务
            await Task.Run(() => server.Start());
            logger.Info("Socket服务已启动");

            //获取连接
            TcpClient client = server.AcceptTcpClient();
            logger.Info("MioBotApi已连接");

            //接收数据
            NetworkStream stream = client.GetStream();
            while (true)
            {
                while (!stream.DataAvailable) ;
                Byte[] bytes = new Byte[client.Available];
                stream.Read(bytes);
                logger.Info("收到请求：" + System.Text.Encoding.Default.GetString(bytes));
                //调用路由
                FuncRouter(System.Text.Encoding.Default.GetString(bytes));
                //应答
                stream.Write(System.Text.Encoding.ASCII.GetBytes("ACK"));
            }
        }
        private static void FuncRouter(string msg)
        {
            //分割请求
            var msgArray = msg.Split('|');
            var str = msgArray[0];
            var qq = msgArray[1];
            var group = msgArray[2];
            //构造对话列表
            //List<(string qq, string type)> qqlist = new();
            //判断请求
            if (!string.IsNullOrEmpty(group)) //处理群消息
            {
                if (str.Contains("打招呼"))
                {
                    Hello.Push(group, qq);
                }
                else if (str.Contains("涩图"))
                {
                    Picture.Push(group, qq, str);
                    //Qmsg.Group(group, "这个功能暂时被停用了喔…");
                }
                else if (str.Contains("状态"))
                {
                    Status.Group(group);
                }
                else if (str.Contains("帮助"))
                {
                    Tips.Group(group);
                }
                else if (str.Contains("BA攻略"))
                {
                    BlueArchive.Push(group, qq, str);
                }
                else if (str.Contains("猜拳"))
                {
                    acklist.Add((qq,"猜拳"));
                    logger.Debug(qq + "已添加队列");
                    Janken.Group(group, qq);
                }
                //当产生过消息实例之后监听该用户的对话内容
                else if (acklist.Any(qqlist => qqlist.qq.Contains(qq)))
                {
                    logger.Debug(qq + "已进入队列");
                    var acktype = acklist.First(qqlist => qqlist.qq == qq).func;
                    if (acktype == "猜拳")
                    {
                        var result = Janken.GroupAck(group, qq, str);
                        if (result != null && result == "胜利") 
                        {
                            acklist.Remove(acklist.First(qqlist => qqlist.qq == qq));
                            logger.Debug(qq + "已移除队列");
                        }
                    }
                }
                else logger.Debug("当前队列无法找到对应QQ");
            }
            else //处理私聊消息
            {
                if (str.Contains("美图"))
                {
                    //Picture.Send(qq, str);
                    Qmsg.Send(qq, "这个功能暂时被停用了喔…");
                }
            }
        }
    }
}
