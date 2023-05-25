using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace MioBot.Job
{
    internal class Trigger
    {
        //每日8:30触发一次
        public static ITrigger D083000()
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("08:30:00", "Daily")
            .WithSchedule(CronScheduleBuilder.DailyAtHourAndMinute(9, 0))
            .Build();
            return trigger;
        }

        //每5秒触发一次 无限循环
        public static ITrigger L000005()
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("5s", "Loop")
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
            .Build();
            return trigger;
        }

        //每30触发一次 无限循环
        public static ITrigger L000030()
        {
            ITrigger trigger = TriggerBuilder.Create()
            .WithIdentity("30s", "Loop")
            .WithSimpleSchedule(x => x.WithIntervalInSeconds(30).RepeatForever())
            .Build();
            return trigger;
        }
    }
}
