using Learun.Workflow.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流任务(新)
    /// </summary>
    public class NWFTaskEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程实例主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PROCESSID")]
        public string F_ProcessId { get; set; }
        /// <summary> 
        /// 流程节点ID 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_NODEID")]
        public string F_NodeId { get; set; }
        /// <summary> 
        /// 流程节点名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_NODENAME")]
        public string F_NodeName { get; set; }
        /// <summary> 
        /// 任务类型1审批2传阅3加签4子流程5重新创建 6子流程重新创建
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TYPE")]
        public int? F_Type { get; set; }
        /// <summary> 
        /// 是否完成1完成2关闭0未完成3子流程处理中
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISFINISHED")]
        public int? F_IsFinished { get; set; }
        /// <summary> 
        /// 任务超时流转到下一个节点时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TIMEOUTACTION")]
        public int? F_TimeoutAction { get; set; }
        /// <summary> 
        /// 任务超时提醒消息时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TIMEOUTNOTICE")]
        public int? F_TimeoutNotice { get; set; }
        /// <summary> 
        /// 任务超时消息提醒间隔时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TIMEOUTINTERVAL")]
        public int? F_TimeoutInterval { get; set; }
        /// <summary> 
        /// 任务超时消息发送策略编码 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TIMEOUTSTRATEGY")]
        public string F_TimeoutStrategy { get; set; }
        /// <summary> 
        /// 上一个任务节点Id 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PREVNODEID")]
        public string F_PrevNodeId { get; set; }
        /// <summary> 
        /// 上一个节点名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PREVNODENAME")]
        public string F_PrevNodeName { get; set; }
        /// <summary> 
        /// 任务创建时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 任务创建人员 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 任务创建人员名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary> 
        /// 任务变更时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary> 
        /// 任务变更人员信息 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary> 
        /// 任务变更人员名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary> 
        /// 是否被催办 1 被催办了
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISURGE")]
        public int? F_IsUrge { get; set; }
        /// <summary> 
        /// 加签情况下最初的审核者 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FIRSTUSERID")]
        public string F_FirstUserId { get; set; }
        /// <summary>
        /// 子流程进程主键
        /// </summary>
        [Column("F_CHILDPROCESSID")]
        public string F_ChildProcessId { get; set; }
        /// <summary>
        /// 批量审核 1是允许 其他值都不允许
        /// </summary>
        [Column("F_ISBATCHAUDIT")]
        public int? F_IsBatchAudit { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_IsFinished = 0;
            this.F_IsUrge = 0;
            this.F_CreateDate = DateTime.Now;
        }
        /// <summary> 
        /// 编辑调用 
        /// </summary> 
        /// <param name="keyValue"></param> 
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
            this.F_ModifyDate = DateTime.Now;
        }
        #endregion

        #region 扩展属性
        /// <summary>
        /// 流程审核人信息
        /// </summary>
        [NotMapped]
        public List<NWFUserInfo> nWFUserInfoList { get; set; }
        #endregion
    }
}
