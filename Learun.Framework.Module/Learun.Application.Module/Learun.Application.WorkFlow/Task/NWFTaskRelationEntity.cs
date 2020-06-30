using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流任务执行人对应关系表(新)
    /// </summary>
    public class NWFTaskRelationEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 任务主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TASKID")]
        public string F_TaskId { get; set; }
        /// <summary> 
        /// 任务执行人员主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_USERID")]
        public string F_UserId { get; set; }
        /// <summary>
        /// 标记0需要处理1暂时不需要处理
        /// </summary>
        [Column("F_MARK")]
        public int? F_Mark { get; set; }
        /// <summary>
        /// 处理结果0.未处理1.同意2.不同意3.超时4.其他
        /// </summary>
        [Column("F_RESULT")]
        public int? F_Result { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [Column("F_SORT")]
        public int? F_Sort { get; set; }
        /// <summary>
        /// 任务执行时间
        /// </summary>
        [Column("F_TIME")]
        public DateTime? F_Time { get; set; }

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
    }
}
