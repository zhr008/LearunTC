using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-02 23:56
    /// 描 述：个人资格证
    /// </summary>
    public class tc_CredentialsEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_CredentialsId
        /// </summary>
        [Column("F_CREDENTIALSID")]
        public string F_CredentialsId { get; set; }
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
        /// F_CertType
        /// </summary>
        [Column("F_CERTTYPE")]
        public int? F_CertType { get; set; }
        /// <summary>
        /// F_MajorType
        /// </summary>
        [Column("F_MAJORTYPE")]
        public int? F_MajorType { get; set; }
        /// <summary>
        /// F_Major
        /// </summary>
        [Column("F_MAJOR")]
        public string F_Major { get; set; }
        /// <summary>
        /// F_CertOrganization
        /// </summary>
        [Column("F_CERTORGANIZATION")]
        public string F_CertOrganization { get; set; }
        /// <summary>
        /// F_CertDateBegin
        /// </summary>
        [Column("F_CERTDATEBEGIN")]
        public DateTime? F_CertDateBegin { get; set; }
        /// <summary>
        /// F_CertDateEnd
        /// </summary>
        [Column("F_CERTDATEEND")]
        public DateTime? F_CertDateEnd { get; set; }
        /// <summary>
        /// F_MarketDate
        /// </summary>
        [Column("F_MARKETDATE")]
        public DateTime? F_MarketDate { get; set; }
        /// <summary>
        /// F_MarketPrice
        /// </summary>
        [Column("F_MARKETPRICE")]
        public decimal? F_MarketPrice { get; set; }
        /// <summary>
        /// F_CertStatus
        /// </summary>
        [Column("F_CERTSTATUS")]
        public int? F_CertStatus { get; set; }
        /// <summary>
        /// F_CertStyle
        /// </summary>
        [Column("F_CERTSTYLE")]
        public int? F_CertStyle { get; set; }
        /// <summary>
        /// F_PracticeStyle
        /// </summary>
        [Column("F_PRACTICESTYLE")]
        public int? F_PracticeStyle { get; set; }
        /// <summary>
        /// F_PracticeSealStyle
        /// </summary>
        [Column("F_PRACTICESEALSTYLE")]
        public int? F_PracticeSealStyle { get; set; }
        /// <summary>
        /// F_CheckInTime
        /// </summary>
        [Column("F_CHECKINTIME")]
        public DateTime? F_CheckInTime { get; set; }
        /// <summary>
        /// F_Description
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// F_DeleteMark
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_CredentialsId = Guid.NewGuid().ToString();
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
            this.F_CredentialsId = keyValue;
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
    public class CredentialsInfo : tc_CredentialsEntity
    {
        [NotMapped]
        public string F_ApplicantId { get; set; }
    }
}

