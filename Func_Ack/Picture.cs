using MioBot.Bot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MioBot.Func_Ack
{
    internal class Picture
    {
        public static void Push(string group, string qq, string keyword)
        {
            //应答
            //Qmsg.Group(group, "@at=" + qq + "@ 正在为你寻找"+keyword+"的美图，请稍后~");
            //var httpClient = new HttpClient();
            //var requesturl = "http://bjb.yunwj.top/php/tk/sj.php?mc=" + keyword;
            //var response = httpClient.GetAsync(requesturl).Result.Content;
            //var stream = response.ReadAsStreamAsync().Result;
            //var jpg = Image.FromStream(stream);
            //var str = "@at=" + qq + "@ 喵，喵喵喵！喵喵~";
            //Qmsg.Group(group, msgList[r]);
        }
    }
}
