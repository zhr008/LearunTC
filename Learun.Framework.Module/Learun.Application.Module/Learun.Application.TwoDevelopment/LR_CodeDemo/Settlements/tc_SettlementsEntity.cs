using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-05 20:42
    /// 描 述：合同结算
    /// </summary>
    public class tc_SettlementsEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_SettlementsId
        /// </summary>
        [Column("F_SETTLEMENTSID")]
        public string F_SettlementsId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("F_USERNAME")]
        public string F_UserName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [Column("F_IDCARDNO")]
        public string F_IDCardNo { get; set; }
        /// <summary>
        /// 个人Id
        /// </summary>
        [Column("F_PERSONID")]
        public string F_PersonId { get; set; }
        /// <summary>
        /// 合同状态
        /// </summary>
        [Column("F_CONTRACTSTATUS")]
        public int? F_ContractStatus { get; set; }
        /// <summary>
        /// 合同起日
        /// </summary>
        [Column("F_CONTRACTSTARTDATE")]
        public DateTime? F_ContractStartDate { get; set; }
        /// <summary>
        /// 合同止日
        /// </summary>
        [Column("F_CONTRACTENDDATE")]
        public DateTime? F_ContractEndDate { get; set; }
        /// <summary>
        /// 维护代表
        /// </summary>
        [Column("F_MAINTAIN")]
        public string F_Maintain { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        [Column("F_MOBILE")]
        public string F_Mobile { get; set; }
        /// <summary>
        /// 联系地址
        /// </summary>
        [Column("F_ADDRESS")]
        public string F_Address { get; set; }
        /// <summary>
        /// 其它方式
        /// </summary>
        [Column("F_OTHERCONTACT")]
        public string F_OtherContact { get; set; }
        /// <summary>
        /// 人才签约代表
        /// </summary>
        [Column("F_APPLICANTID")]
        public string F_ApplicantId { get; set; }
        /// <summary>
        /// 收款人
        /// </summary>
        [Column("F_PAYEE")]
        public string F_payee { get; set; }
        /// <summary>
        /// 开户行
        /// </summary>
        [Column("F_BANKNAME")]
        public string F_BankName { get; set; }
        /// <summary>
        /// 银行账号
        /// </summary>
        [Column("F_BANKACCOUNT")]
        public string F_BankAccount { get; set; }
        /// <summary>
        /// 人员薪酬
        /// </summary>
        [Column("F_PERSONAMOUNT")]
        public decimal? F_PersonAmount { get; set; }
        /// <summary>
        /// 中介费用
        /// </summary>
        [Column("F_APPLICANTAMOUNT")]
        public decimal? F_ApplicantAmount { get; set; }
        /// <summary>
        /// 人员薪酬
        /// </summary>
        [Column("F_CONTRACTAMOUNT")]
        public decimal? F_ContractAmount { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        [Column("F_PAYSTATUS")]
        public int? F_PayStatus { get; set; }
        /// <summary>
        /// 累计支付金额
        /// </summary>
        [Column("F_PAYTOTALAMOUNT")]
        public decimal? F_PayTotalAmount { get; set; }
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
            this.F_SettlementsId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_DeleteMark = 0;
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
            this.F_SettlementsId = keyValue;
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
    public class SettlementsInfo: tc_SettlementsEntity
    {
        [NotMapped]
        public string ApplicantId { get; set; }
    }
}

