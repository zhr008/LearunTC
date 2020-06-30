using Quartz;
using Quartz.Impl;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.19
    /// 描 述：任务调度器
    /// </summary>
    public class WfJobScheduler
    {
        /// <summary>
        /// 开启任务调度器
        /// </summary>
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<WorkFlowJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
              .WithIdentity("triggerName", "groupName")
              .WithSimpleSchedule(t =>
                t.WithIntervalInMinutes(10)//WithIntervalInHours
                 .RepeatForever())
                 .Build();

            scheduler.ScheduleJob(job, trigger);
        }
        /// <summary>
        /// 关闭任务调度器
        /// </summary>
        public static void End()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Shutdown(true);
        }
    }
}
