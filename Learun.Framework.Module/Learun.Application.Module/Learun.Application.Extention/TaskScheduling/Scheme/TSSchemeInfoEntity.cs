using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 16:07
    /// 描 述：任务计划模板信息
    /// </summary>
    public class TSSchemeInfoEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 任务名称 
        /// </summary> 
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary> 
        /// 看板说明 
        /// </summary> 
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 0未删除1删除
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_DeleteMark = 0;
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
        /// 状态 1.未执行2.运行中3.暂停4.已结束10.已关闭
        /// </summary>
        [NotMapped]
        public int? F_State { get; set; }
        /// <summary> 
        /// 开始时间 
        /// </summary> 
        [NotMapped]
        public DateTime? F_BeginTime { get; set; }
        /// <summary> 
        /// 结束时间 
        /// </summary> 
        [NotMapped]
        public DateTime? F_EndTime { get; set; }
        /// <summary>
        /// 任务进程主键
        /// </summary>
        [NotMapped]
        public string F_PorcessId { get; set; }
        
        #endregion
    }
}
