using MioBot;
using MioBot.Job;
using Quartz;
using Quartz.Impl;

//初始化日志
NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

//初始化调度器
Scheduler.Start().GetAwaiter().GetResult();
logger.Debug("开始运行：定时任务");

//初始化窗体
while (true)
{
    Console.ReadKey(true);
}