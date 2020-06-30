using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;
using System;
using System.Collections.Generic;
using Learun.Util;

namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 16:07
    /// 描 述：任务调度操作类
    /// </summary>
    public class QuartzHelper
    {
        private static ISchedulerFactory schedFact = new StdSchedulerFactory();//构建一个调度工厂

        #region 新增任务
        /// <summary>
        /// 添加只执行一次的任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="starTime">任务开始时间</param>
        /// <param name="endTime">任务结束时间</param>
        /// <param name="keyValue">任务主键</param>
        public static void AddRepeatOneJob(string jobName, string starTime, string endTime, string keyValue)
        {
            try
            {
                IScheduler sched = schedFact.GetScheduler(); //得到一个调度程序

                IJobDetail job = JobBuilder.Create<SchedulingHelper>()//新建任务执行类
                    .WithIdentity(jobName, "lrts") // name "myJob", group "group1"
                    .UsingJobData("keyValue", keyValue)//传递参数
                    .Build();
                ///开始时间处理
                DateTime StarTime = DateTime.Now;
                if (!string.IsNullOrEmpty(starTime))
                {
                    StarTime = Convert.ToDateTime(starTime);
                }
                DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(StarTime, 1);
                ///结束时间处理
                DateTime EndTime =DateTime.Now;
                if (!string.IsNullOrEmpty(endTime))
                {
                    EndTime = Convert.ToDateTime(endTime);
                }
                DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
                // 创建一个时间触发器
                SimpleTriggerImpl trigger = new SimpleTriggerImpl();
                trigger.Name = jobName;
                trigger.Group = "lrts";
                trigger.StartTimeUtc = starRunTime;
                trigger.EndTimeUtc = endRunTime;
                trigger.RepeatCount = 0;
                sched.ScheduleJob(job, trigger);
                // 启动
                if (!sched.IsStarted)
                {
                    sched.Start();
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        /// <summary>
        /// 添加简单重复执行任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="keyValue">任务主键</param>
        /// <param name="value">间隔值</param>
        /// <param name="type">间隔类型</param>
        public static void AddRepeatJob(string jobName, string starTime, string endTime, string keyValue, int value, string type)
        {
            try
            {
                IScheduler sched = schedFact.GetScheduler(); //得到一个调度程序

                IJobDetail job = JobBuilder.Create<SchedulingHelper>()//新建任务执行类
                    .WithIdentity(jobName, "lrts") // name "myJob", group "group1"
                    .UsingJobData("keyValue", keyValue)//传递参数
                    .Build();
                ///开始时间处理
                DateTime StarTime = DateTime.Now;
                if (!string.IsNullOrEmpty(starTime))
                {
                    StarTime = Convert.ToDateTime(starTime);
                }
                DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(StarTime, 1);
                ///结束时间处理
                DateTime EndTime = DateTime.MaxValue.AddDays(-1);
                if (!string.IsNullOrEmpty(endTime))
                {
                    EndTime = Convert.ToDateTime(endTime);
                }
                DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
                ITrigger trigger = null;

                switch (type) {
                    case "minute": // 分钟
                        trigger = TriggerBuilder.Create().WithIdentity(jobName, "lrts")
                            .StartAt(starRunTime)
                            .EndAt(endRunTime)
                            .WithSimpleSchedule(t =>
                            t.WithIntervalInMinutes(value)
                            .RepeatForever())
                            .Build();
                        break;
                    case "hours": // 小时
                        trigger = TriggerBuilder.Create()
                            .WithIdentity(jobName, "lrts")
                            .StartAt(starRunTime)
                            .EndAt(endRunTime)
                            .WithSimpleSchedule(t =>
                            t.WithIntervalInHours(value)
                            .RepeatForever())
                            .Build();
                        break;
                    case "day": // 天
                        trigger = TriggerBuilder.Create()
                             .WithIdentity(jobName, "lrts")
                             .StartAt(starRunTime)
                             .EndAt(endRunTime)
                             .WithSchedule(
                                        CalendarIntervalScheduleBuilder.Create()
                                        .WithIntervalInDays(value)
                                         )
                             .Build();
                        break;
                    case "week":// 周
                        trigger = TriggerBuilder.Create()
                         .WithIdentity(jobName, "lrts")
                         .StartAt(starRunTime)
                         .EndAt(endRunTime)
                         .WithSchedule(
                                    CalendarIntervalScheduleBuilder.Create()
                                     .WithIntervalInWeeks(value)
                                     )
                         .Build();
                        break;
                }
                //实例化
                sched.ScheduleJob(job, trigger);
                //启动
                if (!sched.IsStarted)
                {
                    sched.Start();
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        /// <summary>
        /// 添加Cron表达式任务（一个表达式）
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="keyValue">主键名称</param>
        /// <param name="corn">Cron表达式</param>
        public static void AddCronJob(string jobName, string starTime, string endTime, string keyValue, string corn)
        {
            try
            {
                IScheduler sched = schedFact.GetScheduler(); //得到一个调度程序

                IJobDetail job = JobBuilder.Create<SchedulingHelper>()//新建任务执行类
                    .WithIdentity(jobName, "lrts") // name "myJob", group "group1"
                    .UsingJobData("keyValue", keyValue)//传递参数
                    .Build();
                ///开始时间处理
                DateTime StarTime = DateTime.Now;
                if (!string.IsNullOrEmpty(starTime))
                {
                    StarTime = Convert.ToDateTime(starTime);
                }
                DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(StarTime, 1);
                ///结束时间处理
                DateTime EndTime = DateTime.MaxValue.AddDays(-1);
                if (!string.IsNullOrEmpty(endTime))
                {
                    EndTime = Convert.ToDateTime(endTime);
                }
                DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
                // 创建一个时间触发器
                ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
                                                 .StartAt(starRunTime)
                                                 .EndAt(endRunTime)
                                                 .WithIdentity(jobName, "lrts")
                                                 .WithCronSchedule(corn)
                                                 .Build();
                sched.ScheduleJob(job, trigger);

                // 启动  
                if (!sched.IsStarted)
                {
                    sched.Start();
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        /// <summary>
        /// 添加多触发器任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        /// <param name="jobGroupName">任务组名称</param>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="keyValue">任务主键</param>
        /// <param name="list">cron数组</param>
        public static void AddListCronJob(string jobName, string starTime, string endTime, string keyValue, List<string> list)
        {
            try
            {
                IScheduler sched = schedFact.GetScheduler(); //得到一个调度程序

                IJobDetail job = JobBuilder.Create<SchedulingHelper>()//新建任务执行类
                    .WithIdentity(jobName, "lrts") // name "myJob", group "group1"
                    .UsingJobData("keyValue", keyValue)//传递参数
                    .StoreDurably()
                    .Build();
                ///开始时间处理
                DateTime StarTime = DateTime.Now;
                if (!string.IsNullOrEmpty(starTime))
                {
                    StarTime = Convert.ToDateTime(starTime);
                }
                DateTimeOffset starRunTime = DateBuilder.NextGivenSecondDate(StarTime, 1);
                ///结束时间处理
                DateTime EndTime = DateTime.MaxValue.AddDays(-1);
                if (!string.IsNullOrEmpty(endTime))
                {
                    EndTime = Convert.ToDateTime(endTime);
                }
                DateTimeOffset endRunTime = DateBuilder.NextGivenSecondDate(EndTime, 1);
                sched.AddJob(job, true);
                // 创建一个时间触发器
                for (var i = 0; i < list.Count; i++)
                {
                    ITrigger trigger = TriggerBuilder.Create()
                                       .WithIdentity("trigger" + Guid.NewGuid().ToString())
                                       .StartAt(starRunTime)
                                       .EndAt(endRunTime)
                                       .ForJob(job)
                                       .WithCronSchedule(list[i])
                                       .Build();
                    sched.ScheduleJob(trigger);
                }
                // 启动  
                if (!sched.IsStarted)
                {
                    sched.Start();
                }
            }
            catch (Exception e)
            {
                throw (e);
            }
        }
        #endregion

        #region 删除任务
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        public static void DeleteJob(string jobName)
        {
            IScheduler sched = schedFact.GetScheduler();
            TriggerKey triggerKey = new TriggerKey(jobName, "lrts");
            sched.PauseTrigger(triggerKey);// 停止触发器  
            sched.UnscheduleJob(triggerKey);// 移除触发器
            sched.DeleteJob(JobKey.Create(jobName, "lrts"));// 删除任务  
        }
        #endregion

        #region 任务暂停和启动
        /// <summary>
        /// 暂停一个job任务
        /// </summary>
        /// <param name="jobName">任务名称</param>
        public static void PauseJob(string jobName)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                IScheduler sched = schedFact.GetScheduler(); //得到一个调度程序
                sched.PauseJob(JobKey.Create(jobName, "lrts"));
            }
        }
        /// <summary>
        /// 重启启动一个job
        /// </summary>
        /// <param name="jobName">任务名称</param>
        public static void ResumeJob(string jobName)
        {
            if (!string.IsNullOrEmpty(jobName))
            {
                IScheduler sched = schedFact.GetScheduler(); //得到一个调度程序
                sched.ResumeJob(JobKey.Create(jobName, "lrts"));
            }
        }
        #endregion

        #region 扩展应用
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="processId">任务进程主键</param>
        /// <param name="scheme">任务模板</param>
        public static void AddJob(string schemeInfoId, string processId,TSSchemeModel scheme)
        {
            string startTime = "";
            string endTime = "";
            if (scheme.startType == 2 && scheme.startTime != null) {
                startTime = ((DateTime)scheme.startTime).ToString("yyyy-MM-dd hh:mm:ss");
            }
            if (scheme.endType == 2 && scheme.endTime != null)
            {
                endTime = ((DateTime)scheme.endTime).ToString("yyyy-MM-dd hh:mm:ss");
            }


            switch (scheme.executeType) {
                case 1:// 只执行一次
                    AddRepeatOneJob(schemeInfoId, startTime, endTime, processId);//加入只执行一次的任务
                    break;
                case 2:// 简单重复执行
                    AddRepeatJob(schemeInfoId, startTime, endTime, processId, scheme.simpleValue, scheme.simpleType);
                    break;
                case 3:// 明细频率执行
                    List<string> cornlist = new List<string>();
                    foreach (var fre in scheme.frequencyList)
                    {
                        string cron = "0 ";
                        cron += fre.minute + " " + fre.hour + " ";
                        switch (fre.type) {
                            case "day":
                                cron += "* ";
                                break;
                            case "week":
                                cron += "? ";
                                break;
                            case "month":
                                cron += fre.carryDate + " ";
                                break;

                        }
                        cron += fre.carryMounth + " ";
                        if (fre.type == "week")
                        {
                            cron += fre.carryDate + " ";
                        }
                        else
                        {
                            cron += "? ";
                        }
                        cron += "*";
                        cornlist.Add(cron);
                    }
                    AddListCronJob(schemeInfoId, startTime, endTime, processId, cornlist);
                    break;
                case 4:// corn表达式
                    AddCronJob(schemeInfoId, startTime, endTime, processId, scheme.cornValue);
                    break;
            }
        }

        public static void InitJob() {
            TSProcessIBLL tSProcessIBLL = new TSProcessBLL();
            TSSchemeIBLL tSSchemeIBLL = new TSSchemeBLL();
            IEnumerable<TSProcessEntity> list = tSProcessIBLL.GetList();
            foreach (var item in list) {
                var schemeEntity = tSSchemeIBLL.GetSchemeEntity(item.F_SchemeId);
                AddJob(item.F_SchemeInfoId, item.F_Id, schemeEntity.F_Scheme.ToObject<TSSchemeModel>());
            }

        }
        #endregion
    }
}
