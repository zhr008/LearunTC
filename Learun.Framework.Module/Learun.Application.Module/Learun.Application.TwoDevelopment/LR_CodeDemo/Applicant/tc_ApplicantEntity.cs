using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2020-06-27 23:47
    /// 描 述：供应商登记
    /// </summary>
    public class tc_ApplicantEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_ApplicantId
        /// </summary>
        [Column("F_APPLICANTID")]
        public string F_ApplicantId { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        [Column("F_COMPANYNAME")]
        public string F_CompanyName { get; set; }
        /// <summary>
        /// 工商注册号码
        /// </summary>
        [Column("F_REGISTRATIONNO")]
        public string F_RegistrationNo { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [Column("F_PROVINCEID")]
        public string F_ProvinceId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [Column("F_CITYID")]
        public string F_CityId { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        [Column("F_COUNTYID")]
        public string F_CountyId { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Column("F_ADDRESS")]
        public string F_Address { get; set; }
        /// <summary>
        /// 注册资金(万元)
        /// </summary>
        [Column("F_REGISTEREDCAPITAL")]
        public decimal? F_RegisteredCapital { get; set; }
        /// <summary>
        /// 法定代表人
        /// </summary>
        [Column("F_REPRESENTATIVE")]
        public string F_Representative { get; set; }
        /// <summary>
        /// 成立日期
        /// </summary>
        [Column("F_ESTABLISHDATE")]
        public DateTime? F_EstablishDate { get; set; }
        /// <summary>
        /// 工商更新日期
        /// </summary>
        [Column("F_BUSINESSUPDATEDATE")]
        public DateTime? F_BusinessUpdateDate { get; set; }
        /// <summary>
        /// 现有资质证书
        /// </summary>
        [Column("F_QUALIFICATIONCERT")]
        public string F_QualificationCert { get; set; }
        /// <summary>
        /// 资质取得日期
        /// </summary>
        [Column("F_QUALIFICATIONSTARTDATE")]
        public string F_QualificationStartDate { get; set; }
        /// <summary>
        /// 资质到期日期
        /// </summary>
        [Column("F_QUALIFICATIONENDDATE")]
        public string F_QualificationEndDate { get; set; }
        /// <summary>
        /// 经营状态
        /// </summary>
        [Column("F_MANAGETYPE")]
        public string F_ManageType { get; set; }
        [Column("F_CERTDATEBEGIN")]
        public DateTime? F_CertDateBegin { get; set; }
        /// <summary>
        /// F_CertDateEnd
        /// </summary>
        [Column("F_CERTDATEEND")]
        public DateTime? F_CertDateEnd { get; set; }

        /// <summary>
        /// 负责人姓名
        /// </summary>
        [Column("F_LEADERUSERNAME")]
        public string F_LeaderUserName { get; set; }
        /// <summary>
        /// 负责人手机
        /// </summary>
        [Column("F_LEADERMOBILE")]
        public string F_LeaderMobile { get; set; }
        /// <summary>
        /// 负责人其他联系
        /// </summary>
        [Column("F_LEADEROTHERCONTACT")]
        public string F_LeaderOtherContact { get; set; }
        /// <summary>
        /// 办事人名称
        /// </summary>
        [Column("F_CLERKUSERNAME")]
        public string F_ClerkUserName { get; set; }
        /// <summary>
        /// 办事人电话
        /// </summary>
        [Column("F_CLERKMOBILE")]
        public string F_ClerkMobile { get; set; }
        /// <summary>
        /// 办事人职务
        /// </summary>
        [Column("F_CLERKJOB")]
        public string F_ClerkJob { get; set; }
        /// <summary>
        /// 办事人其他联系
        /// </summary>
        [Column("F_CLERKOTHERCONTACT")]
        public string F_ClerkOtherContact { get; set; }
        /// <summary>
        /// 单位开户行
        /// </summary>
        [Column("F_BANKNAME")]
        public string F_BankName { get; set; }
        /// <summary>
        /// 单位银行账户
        /// </summary>
        [Column("F_BANKACCOUNT")]
        public string F_BankAccount { get; set; }
        /// <summary>
        /// 单位类型
        /// </summary>
        [Column("F_APPLICANTTYPE")]
        public int? F_ApplicantType { get; set; }
        /// <summary>
        /// F_Description
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// F_CreateDate
        /// </summary>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// F_CreateUserName
        /// </summary>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// F_CreateUserId
        /// </summary>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// F_ModifyDate
        /// </summary>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// F_ModifyUserName
        /// </summary>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// F_ModifyUserId
        /// </summary>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// F_DeleteMark
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }

        /// <summary>
        /// F_SupplyType
        /// </summary>
        [Column("F_SUPPLYTYPE")]
        public int? F_SupplyType { get; set; }
        /// <summary>
        /// F_AccountName
        /// </summary>
        [Column("F_ACCOUNTNAME")]
        public string F_AccountName { get; set; }

        
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_ApplicantId = Guid.NewGuid().ToString();
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
            this.F_ApplicantId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

