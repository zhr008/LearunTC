using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-08 22:22
    /// 描 述：合同结算详情
    /// </summary>
    public class tc_SettlementsDetailsEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键Id
        /// </summary>
        [Column("F_SETTLEMENTDETAILSID")]
        public string F_SettlementDetailsId { get; set; }
        /// <summary>
        /// 结算Id
        /// </summary>
        [Column("F_SETTLEMENTSID")]
        public string F_SettlementsId { get; set; }
        /// <summary>
        /// F_BatchNumber
        /// </summary>
        [Column("F_BATCHNUMBER")]
        public string F_BatchNumber { get; set; }
        /// <summary>
        /// F_UserName
        /// </summary>
        [Column("F_USERNAME")]
        public string F_UserName { get; set; }
        /// <summary>
        /// F_IDCardNo
        /// </summary>
        [Column("F_IDCARDNO")]
        public string F_IDCardNo { get; set; }
        /// <summary>
        /// F_PersonId
        /// </summary>
        [Column("F_PERSONID")]
        public string F_PersonId { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        [Column("F_PAYDATE")]
        public DateTime? F_PayDate { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        [Column("F_PAYAMOUNT")]
        public decimal? F_PayAmount { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        [Column("F_PAYSTATUS")]
        public int? F_PayStatus { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        [Column("F_PAYCONDITION")]
        public string F_PayCondition { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
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
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_SettlementDetailsId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_SettlementDetailsId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }


    [NotMapped]
    public class SettlementsDetailsInfo : tc_SettlementsDetailsEntity
    {
        [NotMapped]
        public string ApplicantId { get; set; }
    }
}

