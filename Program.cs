using MioBot;
using MioBot.Job;
using MioBot.Socket;

//初始化日志
NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

//初始化调度器
Scheduler.Start().GetAwaiter().GetResult();
logger.Debug("开始运行：计划任务");

//初始化网络监听
Server.Start();

//初始化窗体
while (true)
{
    Console.ReadKey(true);
}