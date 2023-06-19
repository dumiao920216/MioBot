using MioBot.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MioBot.Func_Ack
{
    internal class Status
    {
        public static void Group(string group)
        {
            //获取系统参数
            PerformanceCounter cpuOccupied = new("Processor", "% Processor Time", "_Total");
            PerformanceCounter ramCounter = new("Memory", "% Committed Bytes In Use");
            //推送消息
            var msg = String.Format("当前系统CPU使用率{0}%，内存使用率{1}%。", cpuOccupied.NextValue().ToString(),ramCounter.NextValue().ToString());
            Qmsg.Group(group, msg);
        }
    }
}
