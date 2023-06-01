using MioBot.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Socket
{
    internal class Client
    {
        public static async void Start()
        {
            Process webApi = new();
            webApi.StartInfo.CreateNoWindow = false;
            webApi.StartInfo.WorkingDirectory = ConfigHelper.ReadSetting("apiPath");
            webApi.StartInfo.UseShellExecute = true;
            webApi.StartInfo.FileName = "MioBotApi.exe";
            await Task.Run(() =>webApi.Start());
        }
    }
}
