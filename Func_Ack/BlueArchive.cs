using MioBot.Bot;
using MioBot.Helper;
using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MioBot.Func_Ack
{
    internal class BlueArchive
    {
        //初始化日志
        static readonly Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public static void Push(string group, string qq, string msg)
        {
            //处理关键字
            var keyword = StringHelper.Delete_string(msg, "[", "]").Replace("BA攻略", "").Trim();
            logger.Debug(keyword);
            //应答请求
            var result = GetPic_BlueArchive(keyword);
            if (result.Count > 0)
            {
                //推送数据
                if (result[0] == "101")
                {
                    Qmsg.Group(group, "哎呀，找到好多类似的结果：\r\n" + result[1] + "\r\n是其中哪个呢？");
                }
                else if (result[0] == "200")
                {
                    var url = "https://arona.cdn.diyigemt.com/image"+ HttpUtility.UrlEncode(result[1]);
                    Qmsg.Group(group, "@image=" + url + "@");
                }
            }
            else
            {
                //找不到时推送提示
                Qmsg.Group(group, "输入的关键字是不是有点问题？重新试试吧？");
            }
        }

        private static List<dynamic> GetPic_BlueArchive(string name)
        {
            var requesturl = "https://arona.diyigemt.com/api/v2/image?name=" + name;
            logger.Debug(requesturl);
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync(requesturl).Result.Content;
            var result = JObject.Parse(response.ReadAsStringAsync().Result);
            List<dynamic> list = new();
            //模糊搜索
            if (result["code"].ToString() == "101")
            {
                list.Add(result["code"].ToString());
                list.Add(string.Join("\r\n", result["data"].Select(item => item.SelectToken("name"))));
            }
            //精准匹配
            else if (result["code"].ToString() == "200" && result["data"].ToString() != "")
            {
                list.Add(result["code"].ToString());
                list.Add(result["data"][0]["content"].ToString());
                list.Add(result["data"][0]["hash"].ToString());
                list.Add(result["data"][0]["type"].ToString());
            }
            return list;
        }

        private static void DownloadPic(string url)
        {
            //创建文件夹
            if (!Directory.Exists(".\\filecache\\bluearchive"))
            {
                Directory.CreateDirectory(".\\filecache\\bluearchive");
            }
            //下载图片
            var httpClient = new HttpClient();
            var requesturl = url;
            var response = httpClient.GetAsync(requesturl).Result.Content;
            var stream = response.ReadAsStreamAsync().Result;
            var image = Image.FromStream(stream!);
            var filename = Guid.NewGuid().ToString("N") + ".jpg";
        }
    }
}
