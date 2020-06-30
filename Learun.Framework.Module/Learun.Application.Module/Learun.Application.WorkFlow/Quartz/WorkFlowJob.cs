using Quartz;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.19
    /// 描 述：流程定时执行任务
    /// </summary>
    public class WorkFlowJob:IJob
    {
        private NWFProcessIBLL nWFProcessIBLL = new NWFProcessBLL();


        /// <summary>
        /// 执行任务方法
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            try
            {
                // 流程任务超时处理
                nWFProcessIBLL.MakeTaskTimeout();
            }
            catch
            {

            }
        }
    }
}
