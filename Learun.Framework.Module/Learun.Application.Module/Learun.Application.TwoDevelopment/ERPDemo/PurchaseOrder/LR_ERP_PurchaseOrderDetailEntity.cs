using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 09:37
    /// 描 述：采购订单
    /// </summary>
    public class LR_ERP_PurchaseOrderDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        [Column("F_CODE")]
        public string F_Code { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        [Column("F_BARCODE")]
        public string F_BarCode { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        [Column("F_PLACE")]
        public string F_Place { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        [Column("F_SPECIFICATION")]
        public string F_Specification { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        [Column("F_TYPE")]
        public string F_Type { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        [Column("F_NUMBER")]
        public string F_Number { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        [Column("F_UNIT")]
        public string F_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("F_COUNT")]
        public decimal? F_Count { get; set; }
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
        /// 状态
        /// </summary>
        [Column("F_STATUS")]
        public int? F_Status { get; set; }
        /// <summary>
        /// 编辑日期
        /// </summary>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("F_DESCRIPTION")]
        public int? F_Description { get; set; }
        /// <summary>
        /// 有效标志0否1是
        /// </summary>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary>
        /// 删除标记0否1是
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// F_PRID
        /// </summary>
        [Column("F_POID")]
        public string F_POID { get; set; }
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
            this.F_ModifyDate = DateTime.Now;
        }
        #endregion
    }
}

