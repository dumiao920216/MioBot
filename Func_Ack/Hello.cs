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
            var msg = "@at=" + qq + "@ 你好喵~";
            Qmsg.Group(group, msg);
        }
    }
}
