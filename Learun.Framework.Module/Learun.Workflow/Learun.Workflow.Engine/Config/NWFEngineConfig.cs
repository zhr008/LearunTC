namespace Learun.Workflow.Engine
{


    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.10
    /// 描 述：工作流引擎配置
    /// </summary>
    public class NWFEngineConfig
    {
        /// <summary>
        /// 流程参数配置
        /// </summary>
        public NWFEngineParamConfig ParamConfig { get; set; }


        #region 委托方法
        /// <summary>
        /// 获取数据库数据
        /// </summary>
        public DbFindTableMethod DbFindTable { get; set; }
        /// <summary>
        /// 获取审核同意数
        /// </summary>
        public GetConfluenceNumMethod GetAgreeNum { get; set; }
        /// <summary>
        /// 获取审核不同意数
        /// </summary>
        public GetConfluenceNumMethod GetDisAgreeNum { get; set; }
        #endregion
    }
    /// <summary>
    /// 流程模板引擎参数配置
    /// </summary>
    public class NWFEngineParamConfig
    {
        /// <summary>
        /// 是否已经有流程实例
        /// </summary>
        public bool HasInstance { get; set; }
        /// <summary>
        /// 是否是子流程 1是 0不是
        /// </summary>
        public int IsChild { get; set; }
        /// <summary>
        /// 父级流程任务主键
        /// </summary>
        public string ParentTaskId { get; set; }
        /// <summary>
        /// 父级流程实例主键
        /// </summary>
        public string ParentProcessId { get; set; }

        /// <summary>
        /// 流程模板
        /// </summary>
        public string Scheme { get; set; }
        /// <summary>
        /// 流程模板名称
        /// </summary>
        public string SchemeName { get; set; }
        /// <summary>
        /// 流程模板编码
        /// </summary>
        public string SchemeCode { get; set; }
        /// <summary>
        /// 流程模板主键
        /// </summary>
        public string SchemeId { get; set; }
        /// <summary>
        /// 流程实例Id
        /// </summary>
        public string ProcessId { get; set; }
        /// <summary>
        /// 流程实例等级 1-普通，2-重要，3-紧急
        /// </summary>
        public int ProcessLevel { get; set; }
        /// <summary>
        /// 流程标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 审核人信息
        /// </summary>
        public string Auditers { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        public NWFUserInfo CreateUser { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public NWFUserInfo CurrentUser { get; set; }
        /// <summary>
        /// 流程状态 0 默认运行状态 1 重新发起 2 运行结束
        /// </summary>
        public int State { get; set; }
    }
}
