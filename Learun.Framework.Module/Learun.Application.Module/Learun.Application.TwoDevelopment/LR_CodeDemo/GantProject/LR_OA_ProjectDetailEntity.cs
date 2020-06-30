using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-05-08 18:30
    /// 描 述：甘特图应用
    /// </summary>
    public class LR_OA_ProjectDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 父ID
        /// </summary>
        [Column("F_PARENTID")]
        public string F_ParentId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [Column("F_ITEMNAME")]
        public string F_ItemName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("F_STARTTIME")]
        public DateTime? F_StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        [Column("F_ENDTIME")]
        public DateTime? F_EndTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("F_REMARK")]
        public string F_Remark { get; set; }
        /// <summary> 
        /// 状态 
        /// </summary> 
        [Column("F_STATUS")]
        public string F_Status { get; set; }
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

