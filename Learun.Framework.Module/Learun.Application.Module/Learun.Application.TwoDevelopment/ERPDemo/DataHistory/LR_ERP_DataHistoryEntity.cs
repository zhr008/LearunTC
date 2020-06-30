using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.TwoDevelopment.LR_CodeDemo

{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-26 15:11
    /// 描 述：单据历史
    /// </summary>
    public class LR_ERP_DataHistoryEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        /// <returns></returns>
        [Column("F_PURCHASENO")]
        public string F_PurchaseNo { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        /// <returns></returns>
        [Column("F_APPLER")]
        public string F_Appler { get; set; }
        /// <summary>
        /// 申请单位
        /// </summary>
        /// <returns></returns>
        [Column("F_DEPARTMENT")]
        public string F_Department { get; set; }
        /// <summary>
        /// 报价类别
        /// </summary>
        /// <returns></returns>
        [Column("F_PURCHASETYPE")]
        public string F_PurchaseType { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        /// <returns></returns>
        [Column("F_APPLYDATE")]
        public DateTime? F_ApplyDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        /// <returns></returns>
        [Column("F_FILE")]
        public string F_File { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        /// <returns></returns>
        [Column("F_THEME")]
        public string F_Theme { get; set; }
        /// <summary>
        /// 商品编号
        /// </summary>
        /// <returns></returns>
        [Column("F_CODE")]
        public string F_Code { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        /// <returns></returns>
        [Column("F_BARCODE")]
        public string F_BarCode { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        /// <returns></returns>
        [Column("F_PLACE")]
        public string F_Place { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        /// <returns></returns>
        [Column("F_SPECIFICATION")]
        public string F_Specification { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPE")]
        public string F_Type { get; set; }
        /// <summary>
        /// 批次号
        /// </summary>
        /// <returns></returns>
        [Column("F_NUMBER")]
        public string F_Number { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        /// <returns></returns>
        [Column("F_UNIT")]
        public string F_Unit { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        /// <returns></returns>
        [Column("F_COUNT")]
        public decimal? F_Count { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        /// <returns></returns>
        [Column("F_PRICE")]
        public decimal? F_Price { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        /// <returns></returns>
        [Column("F_AMOUNT")]
        public decimal? F_Amount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        /// <returns></returns>
        [Column("F_STATUS")]
        public int? F_Status { get; set; }
        /// <summary>
        /// 采购员
        /// </summary>
        /// <returns></returns>
        [Column("F_PURCHASER")]
        public string F_Purchaser { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        /// <returns></returns>
        [Column("F_PAYMENTTYPE")]
        public string F_PaymentType { get; set; }
        /// <summary>
        /// 我方代表
        /// </summary>
        /// <returns></returns>
        [Column("F_WE")]
        public string F_We { get; set; }
        /// <summary>
        /// 对方代表
        /// </summary>
        /// <returns></returns>
        [Column("F_YOUR")]
        public string F_Your { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        /// <returns></returns>
        [Column("F_TOTAL")]
        public decimal? F_Total { get; set; }
        /// <summary>
        /// 交货日期
        /// </summary>
        /// <returns></returns>
        [Column("F_DELIVERYDATE")]
        public DateTime? F_DeliveryDate { get; set; }
        /// <summary>
        /// 发货人
        /// </summary>
        /// <returns></returns>
        [Column("F_PURCHASEWAREHOUSINGER")]
        public string F_PurchaseWarehousinger { get; set; }
        /// <summary>
        /// 点收日期
        /// </summary>
        /// <returns></returns>
        [Column("F_PURCHASEWAREHOUSINGDATE")]
        public DateTime? F_PurchaseWarehousingDate { get; set; }
        /// <summary>
        /// 发货地址
        /// </summary>
        /// <returns></returns>
        [Column("F_FROMADDRESS")]
        public string F_FromAddress { get; set; }
        /// <summary>
        /// 收获地址
        /// </summary>
        /// <returns></returns>
        [Column("F_TOADDRESS")]
        public string F_ToAddress { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        /// <returns></returns>
        [Column("F_ORDER")]
        public string F_Order { get; set; }
        /// <summary>
        /// F_DataID
        /// </summary>
        /// <returns></returns>
        [Column("F_DATAID")]
        public string F_DataId { get; set; }
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

