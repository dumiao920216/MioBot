using MioBot.Bot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Func_Ack
{
    internal class Tips
    {
        public static void Group(string group)
        {
            //添加打招呼可用的消息列表
            var tips = new StringBuilder();
            tips.AppendLine("目前小都拥有以下功能喔：");
            tips.AppendLine("#美图 {关键字} ： 找点好康的东西");
            tips.AppendLine("#状态 ： 查看当前系统运行状态");
            tips.AppendLine("其他功能陆续添加中，请保持关注~");
            //推送消息
            Qmsg.Group(group, tips.ToString());
        }
    }
}
