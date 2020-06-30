using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.11
    /// 描 述：工作流进程详情数据模型
    /// </summary>
    public class NWFProcessDetailsModel
    {

        /// <summary>
        /// 当前节点ID
        /// </summary>
        public string CurrentNodeId { get; set; }
        /// <summary>
        /// 当前正在执行的任务节点ID数据
        /// </summary>
        public List<string> CurrentNodeIds { get; set; }
        /// <summary>
        /// 流程模板信息
        /// </summary>
        public string Scheme { get; set; }
        /// <summary>
        /// 任务执行记录
        /// </summary>
        public List<NWFTaskLogEntity> TaskLogList { get; set; }
        /// <summary>
        /// 子流程进程主键
        /// </summary>
        public string childProcessId { get; set; }
        /// <summary>
        /// 父流程进程主键
        /// </summary>
        public string parentProcessId { get; set; }
        /// <summary>
        /// 流程是否结束 0 不是 1 是
        /// </summary>
        public int isFinished { get; set; }
    }
}
