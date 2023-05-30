using MioBot.Bot;
using MioBot.Func;
using MioBot.Helper;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Job
{
    #region 每日新闻
    internal class DailyNewsBuilder : IJob
    {
        readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        readonly ConfigHelper confighelper = new();
        public Task Execute(IJobExecutionContext context)
        {
            //获取群号
            var group_number = confighelper.ReadSetting("group").Split(",");

            return Task.Factory.StartNew(() =>
            {
                try
                {
                    var json_news = DailyNews.Get();
                    string str_news = "@image=" + json_news["data"]!["image"]!.ToString() + "@";
                    foreach (var item in group_number) 
                    {
                        Qmsg.Group(item, str_news);
                        Task.Delay(1000).Wait();
                    }
                    logger.Info("【每日新闻】消息已推送");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            });
        }
    }
    internal class DailyNewsJob
    {
        public static IJobDetail Job()
        {
            IJobDetail job = JobBuilder.Create<DailyNewsBuilder>() //获取JobBuilder
                            .WithIdentity("DailyNews", "Daily")       //添加Job的名字和分组
                            .WithDescription("获取每日新闻并推送")    //添加描述
                            .Build();                                 //生成IJobDetail
            return job;
        }
        public static ITrigger Trigger()
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("DailyNews", "Daily")
            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(8, 30))
            .Build();
            return trigger;
        }
    }
    #endregion

    #region 摸鱼日历
    internal class MoyuCaleJobBuilder : IJob
    {
        readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        readonly ConfigHelper confighelper = new();
        public Task Execute(IJobExecutionContext context)
        {
            var group_number = confighelper.ReadSetting("group").Split(",");

            return Task.Factory.StartNew(() =>
            {
                try
                {
                    var json_moyu = MoyuCale.Get();
                    string str_news = "@image=" + json_moyu["url"]!.ToString() + "@";
                    foreach (var item in group_number)
                    {
                        Qmsg.Group(item, str_news);
                        Task.Delay(1000).Wait();
                    }
                    logger.Info("【摸鱼日历】消息已推送");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            });
        }
    }
    internal class MoyuCaleJob
    {
        public static IJobDetail Job()
        {
            IJobDetail job = JobBuilder.Create<MoyuCaleJobBuilder>()    //获取JobBuilder
                            .WithIdentity("MoyuCale", "Daily")          //添加Job的名字和分组
                            .WithDescription("获取摸鱼日历并推送")      //添加描述
                            .Build();                                   //生成IJobDetail
            return job;
        }
        public static ITrigger Trigger()
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("MoyuCale", "Daily")
            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(8, 45))
            .Build();
            return trigger;
        }
    }
    #endregion

    #region 工作推送
    internal class PublicOfferBuilder : IJob
    {
        readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    var list_offer = PublicOffer.Get();
                    list_offer.ForEach(x =>
                    {
                        Qmsg.Send("913682980", x);
                        Task.Delay(1000).Wait();
                    });
                    logger.Info("【工作推送】消息已推送");
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                }
            });
        }
    }
    internal class PublicOfferJob
    {
        public static IJobDetail Job()
        {
            IJobDetail job = JobBuilder.Create<PublicOfferBuilder>() //获取JobBuilder
                            .WithIdentity("PublicOffer", "Daily")       //添加Job的名字和分组
                            .WithDescription("爬取事业单位招聘信息并推送")    //添加描述
                            .Build();                                 //生成IJobDetail
            return job;
        }
        public static ITrigger Trigger()
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("PublicOffer", "Daily")
            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(18, 0))
            .Build();
            return trigger;
        }
    }
    #endregion
}
