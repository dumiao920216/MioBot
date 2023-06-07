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

namespace MioBot.Socket
{
    internal class Server
    {
        //初始化日志
        static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

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
            //判断请求
            if (str.Contains("打招呼"))
            {
                Hello.Push(group, qq);
            }
            else if (str.Contains("美图"))
            {
                Picture.Push(group, qq, str);
            }
            else return;
        }
    }
}
