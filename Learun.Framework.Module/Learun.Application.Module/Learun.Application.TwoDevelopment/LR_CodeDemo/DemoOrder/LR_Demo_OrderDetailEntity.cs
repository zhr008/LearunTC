using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-06-04 10:38
    /// 描 述：订单信息
    /// </summary>
    public class LR_Demo_OrderDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// 订单明细主键
        /// </summary>
        [Column("F_ORDERENTRYID")]
        public string F_OrderEntryId { get; set; }
        /// <summary>
        /// 订单主键
        /// </summary>
        [Column("F_ORDERID")]
        public string F_OrderId { get; set; }
        /// <summary>
        /// 商品Id
        /// </summary>
        [Column("F_PRODUCTID")]
        public string F_ProductId { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        [Column("F_PRODUCTCODE")]
        public string F_ProductCode { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("F_PRODUCTNAME")]
        public string F_ProductName { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("F_UNITID")]
        public string F_UnitId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("F_QTY")]
        public decimal? F_Qty { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        [Column("F_PRICE")]
        public decimal? F_Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        [Column("F_AMOUNT")]
        public decimal? F_Amount { get; set; }
        /// <summary>
        /// 含税单价
        /// </summary>
        [Column("F_TAXPRICE")]
        public decimal? F_Taxprice { get; set; }
        /// <summary>
        /// 税率
        /// </summary>
        [Column("F_TAXRATE")]
        public decimal? F_TaxRate { get; set; }
        /// <summary>
        /// 税额
        /// </summary>
        [Column("F_TAX")]
        public decimal? F_Tax { get; set; }
        /// <summary>
        /// 含税金额
        /// </summary>
        [Column("F_TAXAMOUNT")]
        public decimal? F_TaxAmount { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 有效标志
        /// </summary>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_OrderEntryId = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_OrderEntryId = keyValue;
        }
        #endregion
    }
}

