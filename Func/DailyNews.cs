using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace MioBot.Func
{
    internal class DailyNews
    {
        public static JsonObject Get()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://api.jun.la/60s.php?format=image").Result.Content;
            var str = response.ReadAsStringAsync().Result.ToString();
            var jsobj = JsonNode.Parse(str)!.AsObject();
            return jsobj;
        }
    }
}
