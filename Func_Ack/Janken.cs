using MioBot.Bot;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Func_Ack
{
    internal class Janken
    {
        public static void Group(string group, string qq) 
        {
            //添加随机的台词
            var msgList = new List<String>
            {
                "@at=" + qq + "@ 喔？要上了吗？一起出拳吧！石头~剪刀~布——"
            };
            //推送消息
            int r = new Random().Next(msgList.Count);
            //_ = r;
            Qmsg.Group(group, msgList[r]);
            //Qmsg.Group(group, "这个功能暂时停用啦…");
        }

        public static string GroupAck(string group, string qq, string janken)
        {
            //添加猜拳选项
            var jankanlist = new List<String>
            {
                "剪刀",
                "石头",
                "布"
            };
            int r = new Random().Next(jankanlist.Count);
            var jankenmsg = "@at=" + qq + "@ "+ jankanlist[r] + "！";
            //判断输入的内容是否有效
            var jankencheck = 0;
            if (janken.Contains("剪刀")) { jankencheck++; };
            if (janken.Contains("石头")) { jankencheck++; };
            if (janken.Contains("布")) { jankencheck++; };
            if (jankencheck == 0)
            {
                Qmsg.Group(group, "你输入的内容不太对哦…重新用“猜拳”和我来试试看吧？");
            }
            else if (jankencheck > 1)
            {
                Qmsg.Group(group, "没有人会同时出多个拳啦…重新用“猜拳”和我来试试看吧？");
            }
            else
            {
                var result = JankenCheck(jankanlist[r], janken);
                if (result != null && result == "平局") 
                {
                    Qmsg.Group(group, jankenmsg);
                    Thread.Sleep(1500);
                    Qmsg.Group(group, "@at=" + qq + "@ " + "平局！再来！");
                    return "平局";
                }
                else if (result != null && result == "胜利")
                {
                    Qmsg.Group(group, jankenmsg);
                    Thread.Sleep(1500);
                    Qmsg.Group(group, "@at=" + qq + "@ " + "我赢啦！喵哈哈哈！看来还是小都的技巧更胜一筹噢！");
                    return "胜利";
                }
                else if (result != null && result == "失败")
                {
                    Qmsg.Group(group, jankenmsg);
                    Thread.Sleep(1500);
                    Qmsg.Group(group, "@at=" + qq + "@ " + "输了呃呜呜呜…你真强喵…");
                    return "胜利";
                }
            }
            return null;
            //推送消息
        }

        private static string JankenCheck(string self,string rival)
        {
            if (rival.Contains("剪刀"))
            {
                switch (self)
                {
                    case "剪刀":
                        return "平局";
                    case "石头":
                        return "胜利";
                    case "布":
                        return "失败";
                }
            }
            else if (rival.Contains("石头"))
            {
                switch (self)
                {
                    case "剪刀":
                        return "失败";
                    case "石头":
                        return "平局";
                    case "布":
                        return "胜利";
                }
            }
            else if (rival.Contains("布"))
            {
                switch (self)
                {
                    case "剪刀":
                        return "胜利";
                    case "石头":
                        return "失败";
                    case "布":
                        return "平局";
                }
            }
            return null;
        }
    }
}
