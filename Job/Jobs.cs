using MioBot.Bot;
using MioBot.Func;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Job
{
    #region 每日新闻
    internal class DailyNewsJobBuilder : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var json_news = DailyNews.Get();
                string str_news = "@image=" + json_news["data"]!["image"]!.ToString() + "@";
                Qmsg.Group("827859510", str_news);
            });
        }
    }

    internal class DailyNewsJob
    {
        public static IJobDetail Package()
        {
            IJobDetail job = JobBuilder.Create<DailyNewsJobBuilder>()       //获取JobBuilder
                            .WithIdentity("DailyNews", "Daily")       //添加Job的名字和分组
                            .WithDescription("获取每日新闻并推送")  //添加描述
                            .Build();                           //生成IJobDetail
            return job;
        }
    }
    #endregion

    #region 摸鱼日历
    internal class MoyuCaleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var json_news = MoyuCale.Get();
                string str_news = "@image=" + json_news["data"]!["image"]!.ToString() + "@";
                Qmsg.Group("827859510", str_news);
            });
        }
    }
    #endregion

    #region 测试任务
    internal class TestJobBuilder : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                Console.WriteLine("现在时间:{0}", DateTime.Now);
            });
        }
    }

    internal class TestJob
    {
        public static IJobDetail Package()
        {
            IJobDetail job = JobBuilder.Create<TestJobBuilder>()       //获取JobBuilder
                            .WithIdentity("Test", "Loop")       //添加Job的名字和分组
                            .WithDescription("一个简单的任务")  //添加描述
                            .Build();                           //生成IJobDetail
            return job;
        }
    }
    #endregion
}
