using MioBot.Bot;
using MioBot.Helper;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using NLog;

namespace MioBot.Func_Ack
{
    internal class Picture
    {
        //初始化日志
        static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Push(string group, string qq, string msg)
        {
            //_ = qq;
            //拆分关键词
            var keyword = StringHelper.Delete_string(msg, "[", "]").Replace(" ", "").Remove(0, 2).Replace("涩图", "").Replace("。", "").Replace(".", "");
            if (keyword.Contains("大都"))
            {
                Qmsg.Group(group, "没有主人的图喔…在想啥呢？");
            }
            else
            {
                //应答请求
                Qmsg.Group(group, "@at=" + qq + "@请稍后，正在寻找" + keyword + "的图");
                var result = GetPic_Lolicon(keyword.Replace("，",","));
                if (result[0] != null)
                {
                    //推送数据
                    var url = result[0].ToString();
                    var pid = result[1].ToString();
                    Qmsg.Group(group, "@image=" + url + "@PIXIV PID：" + pid);
                }
                else
                {
                    //找不到时推送提示
                    Qmsg.Group(group, "哎呀…好像找不到" + keyword + "的图，换个试试吧？");
                }
                
            }
        }

        //public static void Send(string qq, string msg)
        //{
        //    //拆分关键词
        //    var keyword = msg.Trim()[3..];
        //    if (keyword.Contains("大都"))
        //    {
        //        //暂时不发
        //        Qmsg.Send(qq, "目前还没有喔…明天再来看吧？");
        //    }
        //    else
        //    {
        //        //应答请求
        //        Qmsg.Send(qq, "正在寻找" + keyword + "的美图，请稍后");
        //        //获取图片
        //        var filename = GetPic(keyword);
        //        if (!String.IsNullOrEmpty(filename))
        //        {
        //            //推送数据
        //            Qmsg.Send(qq, "@image=" + ConfigHelper.ReadSetting("imgServer") + DateTime.Now.ToString("yyyyMMdd") + "/" + filename + "@");
        //        }
        //        else
        //        {
        //            //找不到时推送提示
        //            Qmsg.Send(qq, "哎呀…好像找不到" + keyword + "的美图，换个试试吧？");
        //        }
        //    }
        //}

        private static string GetPic(string keyword)
        {
            var httpClient = new HttpClient();
            var requesturl = "http://bjb.yunwj.top/php/tk/sj.php?mc=" + keyword;
            var response = httpClient.GetAsync(requesturl).Result.Content;
            var stream = response.ReadAsStreamAsync().Result;
            if (stream.Length > 0)
            {
                var jpg = Image.FromStream(stream!);
                //创建当日文件夹
                var imgPathToday = ConfigHelper.ReadSetting("imgPath") + "\\" + DateTime.Now.ToString("yyyyMMdd");
                if (!Directory.Exists(imgPathToday))
                {
                    Directory.CreateDirectory(imgPathToday);
                }
                //写入文件
                var filename = Guid.NewGuid().ToString("N") + ".jpg";
                jpg.Save(ConfigHelper.ReadSetting("imgPath") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
                LogHelper.logger.Info("获取到文件：" + filename);
                return filename;
            }
            else
            {
                LogHelper.logger.Warn("未能获取到图片数据");
                return "";
            }
        }

        private static List<string> GetPic_Lolicon(string tag) //从Lolicon_API获取涩图
        {
            //解析tag
            var tags = "&tag=" + string.Join("&tag=", tag.Split(','));
            var requesturl = "https://api.lolicon.app/setu/v2?size=regular" + tags;
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(requesturl).Result.Content;
            var result = JObject.Parse(response.ReadAsStringAsync().Result)["data"];
            
            //组装返回值
            var returnList = new List<string>();
            if (result.ToString().Count() <= 2)
            {
                returnList.Add(null);
            }
            else
            {
                returnList.Add(result[0]["urls"]["regular"].ToString()); //获取图片url
                returnList.Add(result[0]["pid"].ToString()); //获取图片pid
            }
            return returnList;
        }
    }
}
