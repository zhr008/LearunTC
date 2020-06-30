namespace Learun.Application.WorkFlow
{    
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.05
    /// 描 述：流程绑定方法参数
    /// </summary>
    public class WfMethodParameter
    {
        /// <summary>
        /// 流程进程Id
        /// </summary>
        public string processId { get; set; }
        /// <summary>
        /// 子流程进程主键
        /// </summary>
        public string childProcessId { get; set; }
        /// <summary>
        /// 当前任务Id
        /// </summary>
        public string taskId { get; set; }
        /// <summary>
        /// 当前节点名称
        /// </summary>
        public string nodeName { get; set; }
        /// <summary>
        /// 操作码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 当前操作用户
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 当前用户账号
        /// </summary>
        public string userAccount { get; set; }
        /// <summary>
        /// 当前用户公司
        /// </summary>
        public string companyId { get; set; }
        /// <summary>
        /// 当前用户部门
        /// </summary>
        public string departmentId { get; set; }
             
    }
}
