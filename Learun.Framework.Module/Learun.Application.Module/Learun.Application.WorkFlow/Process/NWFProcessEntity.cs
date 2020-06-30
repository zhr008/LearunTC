using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流进程(新)
    /// </summary>
    public class NWFProcessEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程模板主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_SCHEMEID")]
        public string F_SchemeId { get; set; }
        /// <summary> 
        /// 流程模板编码 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_SCHEMECODE")]
        public string F_SchemeCode { get; set; }
        /// <summary> 
        /// 流程模板名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_SCHEMENAME")]
        public string F_SchemeName { get; set; }
        /// <summary> 
        /// 流程进程自定义标题 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TITLE")]
        public string F_Title { get; set; }
        /// <summary> 
        /// 流程进程等级 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_LEVEL")]
        public int? F_Level { get; set; }
        /// <summary> 
        /// 流程进程有效标志 1正常2草稿3作废
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary> 
        /// 是否重新发起1是0不是 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISAGAIN")]
        public int? F_IsAgain { get; set; }
        /// <summary> 
        /// 流程进程是否结束1是0不是 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISFINISHED")]
        public int? F_IsFinished { get; set; }
        /// <summary> 
        /// 是否是子流程进程1是0不是 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISCHILD")]
        public int? F_IsChild { get; set; }

        /// <summary> 
        /// 子流程执行方式1异步0同步
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISASYN")]
        public int? F_IsAsyn { get; set; }
        /// <summary>
        /// 父流程的发起子流程的节点Id
        /// </summary>
        [Column("F_PARENTNODEID")]
        public string F_ParentNodeId { get; set; }
        /// <summary> 
        /// 流程进程父进程任务主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PARENTTASKID")]
        public string F_ParentTaskId { get; set; }
        /// <summary> 
        /// 流程进程父进程主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PARENTPROCESSID")]
        public string F_ParentProcessId { get; set; }
        /// <summary>
        /// 1表示开始处理过了 0 还没人处理过
        /// </summary>
        [Column("F_ISSTART")]
        public int? F_IsStart { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 创建人主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 创建人名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary> 
        /// 编辑调用 
        /// </summary> 
        /// <param name="keyValue"></param> 
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion

        #region 扩展字段
        /// <summary>
        /// 任务名称
        /// </summary>
        [NotMapped]
        public string F_TaskName { get; set; }
        /// <summary>
        /// 任务主键
        /// </summary>
        [NotMapped]
        public string F_TaskId { get; set; }
        /// <summary>
        /// 任务类型
        /// </summary>
        [NotMapped]
        public int? F_TaskType { get; set; }

        /// <summary> 
        /// 是否被催办 1 被催办了
        /// </summary> 
        /// <returns></returns> 
        [NotMapped]
        public int? F_IsUrge { get; set; }
        #endregion
    }
}
