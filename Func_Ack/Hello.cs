using MioBot.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Func_Ack
{
    internal class Hello
    {
        public static void Push(string group, string qq)
        {
            //添加打招呼可用的消息列表
            var msgList = new List<String>
            {
                "@at=" + qq + "@ 嗨~我是小都，一个虽然不太聪明但足够可爱的个人助理~请多关照喔！~",
                //"@at=" + qq + "@ 嗨~今天也请多关照喵！",
                //"@at=" + qq + "@ 哈喽哈喽喵~",
                //"@at=" + qq + "@ 喵，喵喵喵！喵喵~"
            };
            //推送消息
            int r = new Random().Next(msgList.Count);
            //_ = r;
            Qmsg.Group(group, msgList[r]);
            //Qmsg.Group(group, "这个功能暂时停用啦…");
        }
    }
}
