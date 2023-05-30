using HtmlAgilityPack;
using NLog;
using NLog.Layouts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MioBot.Func
{
    internal class PublicOffer
    {
        public static List<string> Get()
        {
            //构造日志类
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
            //构造返回对象
            var list = new List<string>();
            //初始化Html客户端
            HtmlWeb hw = new() { OverrideEncoding = System.Text.Encoding.Default };
            //载入网页
            HtmlDocument hd = hw.Load("http://gzw.cq.gov.cn/gqzp/");
            //分析节点
            hd.DocumentNode.SelectNodes("//ul[@class='tab-item']").AsParallel<HtmlNode>().ToList().ForEach((Action<HtmlNode>)(hd =>
            {
                //判断发布时间
                if (hd.SelectSingleNode(".//span").InnerText == DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"))
                {
                    //获取发布内容
                    HtmlWeb chw = new() { OverrideEncoding = System.Text.Encoding.Default };
                    HtmlAttribute cha = hd.SelectSingleNode(".//a").Attributes["href"];
                    string cha_url = "http://gzw.cq.gov.cn/gqzp/" + cha.Value.Replace("./", "");
                    HtmlDocument chd = chw.Load(cha_url);
                    StringBuilder sb = new(128);
                    //读取内容节点
                    chd.DocumentNode.SelectNodes("/html/body/div[2]/div[2]/div[2]/div[2]//p")
                        .AsParallel<HtmlNode>().ToList().ForEach((Action<HtmlNode>)(chd =>
                        {
                            if (chd.SelectSingleNode(".//span") != null)
                            {
                                sb.AppendLine(chd.SelectSingleNode(".//span").InnerText);
                            }
                        }));
                    //判读发布内容
                    if (sb.ToString().Contains("开发") || sb.ToString().Contains("数据"))
                    {
                        //汇总数据
                        list.Add("新获取到合适的岗位：" + hd.SelectSingleNode(".//a").InnerText + "，请注意查看：" + cha_url);
                        logger.Info("获取到条目：" + hd.SelectSingleNode(".//a").InnerText);
                    }
                }
            }));
            if (list.Count == 0 ) 
            {
                list.Add("今天暂时没有新的岗位消息，明天再来看吧~");
            }
            return list;
        }
    }
}
