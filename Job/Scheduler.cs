﻿using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MioBot.Job
{
    internal class Scheduler
    {
        public static async Task Start()
        {
            //初始化日志
            NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

            //创建调度器
            StdSchedulerFactory factory = new();
            IScheduler scheduler = await factory.GetScheduler();
            logger.Debug("正在配置：调度器已创建");

            //注册任务
            await scheduler.ScheduleJob(DailyNewsJob.Detail(), Trigger.D083000());
            logger.Debug("正在配置：任务【每日新闻】已添加");

            await scheduler.ScheduleJob(MoyuCaleJob.Detail(), Trigger.D083000());
            logger.Debug("正在配置：任务【每日新闻】已添加");

            //启动调度器
            await scheduler.Start();
            logger.Debug("正在配置：调度器已启动");
        }
    }
}
