using MioBot.Bot;
using MioBot.Func;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Test
{
    internal class Test
    {
        public static void Test1()
        {
            var json_news = DailyNews.Get();
            string str_news = "@image=" + json_news["data"]!["image"]!.ToString() + "@";
            Qmsg.Send("381681652", str_news);
        }
    }
}
