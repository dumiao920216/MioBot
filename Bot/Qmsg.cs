using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using MioBot.Helper;

namespace MioBot.Bot
{
    internal class Qmsg
    {
        public static JsonObject Private(string qq, string msg)
        {
            var confighelper = new ConfigHelper();
            var httpClient = new HttpClient();
            //拼装链接
            var token = confighelper.ReadSetting("token");
            var url = "https://qmsg.zendee.cn:443/send/" + token;
            //构造返回
            var response = httpClient.PostAsync(url, new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("qq",qq),
                new KeyValuePair<string, string>("msg",msg)
            })).Result.Content.ReadAsStringAsync().Result;
            //推送请求
            return JsonNode.Parse(response)!.AsObject();
        }
        //Task<HttpResponseMessage>
        public static JsonObject Group(string qq, string msg)
        {
            var confighelper = new ConfigHelper();
            var httpClient = new HttpClient();
            //拼装链接
            var token = confighelper.ReadSetting("token");
            var url = "https://qmsg.zendee.cn:443/group/" + token;
            //构造返回
            var response = httpClient.PostAsync(url, new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("qq",qq),
                new KeyValuePair<string, string>("msg",msg)
            })).Result.Content.ReadAsStringAsync().Result;
            //推送请求
            return JsonNode.Parse(response)!.AsObject();
        }
    }
}
