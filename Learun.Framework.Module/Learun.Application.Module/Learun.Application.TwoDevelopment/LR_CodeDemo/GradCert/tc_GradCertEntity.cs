using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.TwoDevelopment.LR_CodeDemo

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-02 23:28
    /// 描 述：毕业证书
    /// </summary>
    public class tc_GradCertEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_GradCertId
        /// </summary>
        /// <returns></returns>
        [Column("F_GRADCERTID")]
        public string F_GradCertId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        /// <returns></returns>
        [Column("F_USERNAME")]
        public string F_UserName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        /// <returns></returns>
        [Column("F_IDCARDNO")]
        public string F_IDCardNo { get; set; }
        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        [Column("F_PERSONID")]
        public string F_PersonId { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        /// <returns></returns>
        [Column("F_MAJOR")]
        public string F_Major { get; set; }
        /// <summary>
        /// F_GradTime
        /// </summary>
        /// <returns></returns>
        [Column("F_GRADTIME")]
        public DateTime? F_GradTime { get; set; }
        /// <summary>
        /// F_EducationType
        /// </summary>
        /// <returns></returns>
        [Column("F_EDUCATIONTYPE")]
        public int? F_EducationType { get; set; }
        /// <summary>
        /// F_Term
        /// </summary>
        /// <returns></returns>
        [Column("F_TERM")]
        public int? F_Term { get; set; }
        /// <summary>
        /// F_OriginalType
        /// </summary>
        /// <returns></returns>
        [Column("F_ORIGINALTYPE")]
        public int? F_OriginalType { get; set; }
        /// <summary>
        /// F_DeleteMark
        /// </summary>
        /// <returns></returns>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// F_Description
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// F_CreateDate
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// F_CreateUserName
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// F_CreateUserId
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// F_ModifyDate
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// F_ModifyUserName
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// F_ModifyUserId
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_GradCertId = Guid.NewGuid().ToString();
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
            this.F_GradCertId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }

    [NotMapped]
    public class GradCertInfo : tc_GradCertEntity
    {
        [NotMapped]
        public string F_ApplicantId { get; set; }
    }
}

