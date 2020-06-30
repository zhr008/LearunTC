using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流任务日志(新)
    /// </summary>
    public class NWFTaskLogEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程进程主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PROCESSID")]
        public string F_ProcessId { get; set; }
        /// <summary> 
        /// 流程任务主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TASKID")]
        public string F_TaskId { get; set; }
        /// <summary> 
        /// 操作码create创建 agree 同意 disagree 不同意 lrtimeout 超时 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_OPERATIONCODE")]
        public string F_OperationCode { get; set; }
        /// <summary>
        /// 操作名称
        /// </summary>
        [Column("F_OPERATIONNAME")]
        public string F_OperationName { get; set; }
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
        /// 流程任务类型 0创建1审批2传阅3加签审核4子流程5重新创建6.超时流转7会签审核8加签9催办10超时提醒 100其他
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TASKTYPE")]
        public int? F_TaskType { get; set; }
        /// <summary> 
        /// 上一流程节点ID 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PREVNODEID")]
        public string F_PrevNodeId { get; set; }
        /// <summary> 
        /// 上一流程节点名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PREVNODENAME")]
        public string F_PrevNodeName { get; set; }
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
        /// 创建人员名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }

        /// <summary>
        /// 任务人Id
        /// </summary>
        [Column("F_TASKUSERID")]
        public string F_TaskUserId { get; set; }
        /// <summary>
        /// 任务人名称
        /// </summary>
        [Column("F_TASKUSERNAME")]
        public string F_TaskUserName { get; set; }
        /// <summary> 
        /// 备注信息 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_DES")]
        public string F_Des { get; set; }

        /// <summary> 
        /// 签名图片 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_SIGNIMG")]
        public string F_SignImg { get; set; }
        /// <summary> 
        /// 盖章图片
        /// </summary> 
        /// <returns></returns> 
        [Column("F_STAMPIMG")]
        public string F_StampImg { get; set; }


        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
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
    }
}
