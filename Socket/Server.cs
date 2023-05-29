using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace MioBot.Socket
{
    internal class Server
    {
        public static void Start()
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 80);
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:80.{0}Waiting for a connection...", Environment.NewLine);
            TcpClient client = server.AcceptTcpClient();
            Console.WriteLine("A client connected.");
            NetworkStream stream = client.GetStream();
            while (true)
            {
                while (!stream.DataAvailable) ;

                Byte[] bytes = new Byte[client.Available];

                stream.Read(bytes, 0, bytes.Length);

                Console.WriteLine(System.Text.Encoding.Default.GetString(bytes));
            }
        }
    }
}
