using MioBot;
using MioBot.Helper;
using MioBot.Job;
using MioBot.Socket;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

//初始化日志
NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

//初始化调度器
Scheduler.Start();
logger.Info("开始运行：计划任务");

//初始化网络监听
Server.Start();
logger.Info("开始运行：网络监听");

//启动WebApi
Client.Start();
logger.Info("开始运行：WebApi");

//初始化窗体
while (true)
{
    Console.ReadKey(true);
}