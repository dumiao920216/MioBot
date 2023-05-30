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

namespace MioBot.Socket
{
    internal class Server
    {
        //初始化日志
        static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static Task Start()
        {
            //构造监听
            ConfigHelper configHelper = new();
            var port = configHelper.ReadSetting("Port");
            IPEndPoint ip = new(IPAddress.Any ,Convert.ToInt32(port));
            TcpListener server = new(ip);

            //启动服务
            server.Start();
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
                stream.Read(bytes, 0, bytes.Length);
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
            //判断请求
            if (str.Contains("打招呼"))
            {
                try
                {
                    Hello.Push(group, qq);
                }
                catch (Exception e)
                {
                    logger.Error(e);
                }
            }
            else return;
        }
    }
}
