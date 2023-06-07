using MioBot.Bot;
using MioBot.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MioBot.Func_Ack
{
    internal class Picture
    {
        public static void Push(string group, string qq, string msg)
        {
            //拆分关键词
            var keyword = msg.Trim()[3..];
            if (keyword == "大都" || keyword == "大都未央" )
            {
                Qmsg.Group(group, "@at=" + qq + "@ 这个不能在这里发的啦…想看的话私聊我吧？");
            }
            else
            {
                return;
            }
            //应答请求
            Qmsg.Group(group, "@at=" + qq + "@ 正在为你寻找" + keyword + "的美图，请稍后~");
            //获取图片
            var httpClient = new HttpClient();
            var requesturl = "http://bjb.yunwj.top/php/tk/sj.php?mc=" + keyword;
            var response = httpClient.GetAsync(requesturl).Result.Content;
            var stream = response.ReadAsStreamAsync().Result;
            var jpg = Image.FromStream(stream);
            //创建当日文件夹
            var imgPathToday = ConfigHelper.ReadSetting("imgPath") + "\\" + DateTime.Now.ToString("yyyyMMdd");
            if (!Directory.Exists(imgPathToday))
            {
                Directory.CreateDirectory(imgPathToday);
            }
            //写入文件
            var filename = Guid.NewGuid().ToString("N") + ".jpg";
            jpg.Save(ConfigHelper.ReadSetting("imgPath") + "\\" + DateTime.Now.ToString("yyyyMMdd") + "\\" + filename, System.Drawing.Imaging.ImageFormat.Jpeg);
            //推送数据
            Qmsg.Group(group, "@image=" + ConfigHelper.ReadSetting("imgServer") + DateTime.Now.ToString("yyyyMMdd") + "/" + filename + "@");
        }
    }
}
