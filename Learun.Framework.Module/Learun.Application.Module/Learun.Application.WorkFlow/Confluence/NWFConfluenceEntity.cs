using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流会签计算(新)
    /// </summary>
    public class NWFConfluenceEntity
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
        /// 会签节点ID 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_NODEID")]
        public string F_NodeId { get; set; }
        /// <summary> 
        /// 上一节点ID  
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FORMNODEID")]
        public string F_FormNodeId { get; set; }
        /// <summary> 
        /// 状态1同意0不同意 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_STATE")]
        public int? F_State { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
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
        /// <summary>
        /// 是否清除之前审核信息
        /// </summary>
        [NotMapped]
        public bool isClear { get; set; }
        /// <summary>
        /// 会签审核结果 1 通过 -1 不通过
        /// </summary>
        [NotMapped]
        public int confluenceRes { get; set; }
    }
}
