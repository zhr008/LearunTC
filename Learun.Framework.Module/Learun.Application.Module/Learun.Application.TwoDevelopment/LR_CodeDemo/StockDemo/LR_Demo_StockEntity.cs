using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-06-12 18:49
    /// 描 述：库存
    /// </summary>
    public class LR_Demo_StockEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 货物名称
        /// </summary>
        [Column("F_ITEMNAME")]
        public string F_ItemName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("F_QTY")]
        public int? F_Qty { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("F_UNIT")]
        public string F_Unit { get; set; }
        /// <summary>
        /// 库位
        /// </summary>
        [Column("F_AREA")]
        public string F_Area { get; set; }
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
        #region 扩展字段
        #endregion
    }
}

