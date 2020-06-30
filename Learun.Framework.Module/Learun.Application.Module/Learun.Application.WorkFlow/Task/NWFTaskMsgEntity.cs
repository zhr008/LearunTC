using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流任务消息(新)
    /// </summary>
    public class NWFTaskMsgEntity
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
        /// 任务发送人主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FROMUSERID")]
        public string F_FromUserId { get; set; }
        /// <summary> 
        /// 任务发送人账号 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FROMUSERACCOUNT")]
        public string F_FromUserAccount { get; set; }
        /// <summary> 
        /// 任务发送人名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FROMUSERNAME")]
        public string F_FromUserName { get; set; }
        /// <summary> 
        /// 任务接收人主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TOUSERID")]
        public string F_ToUserId { get; set; }
        /// <summary> 
        /// 任务接收人账号 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TOACCOUNT")]
        public string F_ToAccount { get; set; }
        /// <summary> 
        /// 任务接收人名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TONAME")]
        public string F_ToName { get; set; }
        /// <summary> 
        /// 任务标题 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TITLE")]
        public string F_Title { get; set; }
        /// <summary> 
        /// 任务内容 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CONTENT")]
        public string F_Content { get; set; }
        /// <summary> 
        /// 任务创建时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 是否结束1结束0未结束 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISFINISHED")]
        public int? F_IsFinished { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_IsFinished = 0;
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

        #region 扩展属性
        /// <summary> 
        /// 节点Id 
        /// </summary>
        /// <returns></returns> 
        [NotMapped]
        public string NodeId { get; set; }
        #endregion
    }
}
