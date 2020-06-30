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
    public class LR_ERP_PurchaseOrderEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        [Column("F_PURCHASENO")]
        public string F_PurchaseNo { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        [Column("F_APPLER")]
        public string F_Appler { get; set; }
        /// <summary>
        /// 申请单位
        /// </summary>
        [Column("F_DEPARTMENT")]
        public string F_Department { get; set; }
        /// <summary>
        /// 采购类别
        /// </summary>
        [Column("F_PURCHASETYPE")]
        public string F_PurchaseType { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        [Column("F_APPLYDATE")]
        public DateTime? F_ApplyDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        [Column("F_FILE")]
        public string F_File { get; set; }
        /// <summary>
        /// 主题
        /// </summary>
        [Column("F_THEME")]
        public string F_Theme { get; set; }
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
        public string F_Status { get; set; }
        /// <summary>
        /// 采购员
        /// </summary>
        [Column("F_PURCHASER")]
        public string F_Purchaser { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        [Column("F_PAYMENTTYPE")]
        public string F_PaymentType { get; set; }
        /// <summary>
        /// 我方代表
        /// </summary>
        [Column("F_WE")]
        public string F_We { get; set; }
        /// <summary>
        /// 对方代表
        /// </summary>
        [Column("F_YOUR")]
        public string F_Your { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        [Column("F_TOTAL")]
        public decimal? F_Total { get; set; }
        /// <summary>
        /// 交货日期
        /// </summary>
        [Column("F_DELIVERYDATE")]
        public DateTime? F_DeliveryDate { get; set; }
        /// <summary>
        /// F_PRId
        /// </summary>
        [Column("F_PRID")]
        public string F_PRId { get; set; }

        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
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
        #region 扩展字段
        #endregion
    }
}

