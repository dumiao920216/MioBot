using MioBot;
using MioBot.Job;
using Quartz;
using Quartz.Impl;

//加载日志
NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
logger.Debug("日志记录已开启");

//创建任务调度器
StdSchedulerFactory factory = new();
IScheduler scheduler = factory.GetScheduler().Result;
scheduler.Start();
logger.Debug("任务调度器已创建");

//加载任务
scheduler.ScheduleJob(TestJob.Package(), Trigger.L000005());
logger.Debug("任务已加载");

//初始化窗体
while (true)
{
    Console.ReadKey(true);
}