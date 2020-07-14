using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.TwoDevelopment.LR_CodeDemo

{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-14 23:25
    /// 描 述：1231
    /// </summary>
    public class tc_RelationEntity
    {
        #region 实体成员
        /// <summary>
        /// F_RelationId
        /// </summary>
        /// <returns></returns>
        [Column("F_RELATIONID")]
        public string F_RelationId { get; set; }
        /// <summary>
        /// ProjectId
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTID")]
        public string ProjectId { get; set; }
        /// <summary>
        /// ProjectDetailId
        /// </summary>
        /// <returns></returns>
        [Column("PROJECTDETAILID")]
        public string ProjectDetailId { get; set; }
        /// <summary>
        /// F_CertId
        /// </summary>
        /// <returns></returns>
        [Column("F_CERTID")]
        public string F_CertId { get; set; }
        /// <summary>
        /// F_PersonId
        /// </summary>
        /// <returns></returns>
        [Column("F_PERSONID")]
        public string F_PersonId { get; set; }
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
        /// <summary>
        /// F_DeleteMark
        /// </summary>
        /// <returns></returns>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_RelationId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
            this.F_DeleteMark = 1;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_RelationId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }



    public class RelationDeatil
    {
      
        public string F_CredentialsId { get; set; }
    
        public string F_UserName { get; set; }
      
        public string F_IDCardNo { get; set; }
     
        public string F_PersonId { get; set; }
       
        public int? F_CertType { get; set; }
       
        public int? F_MajorType { get; set; }
      
        public string F_Major { get; set; }
     
      
        public string F_CertOrganization { get; set; }
       
        
        public DateTime? F_CertDateBegin { get; set; }
       
      
        public DateTime? F_CertDateEnd { get; set; }
        
       
        public int? F_CertStatus { get; set; }
      
      
        public int? F_CertStyle { get; set; }
    
      
        public int? F_PracticeStyle { get; set; }
     
       
        public int? F_PracticeSealStyle { get; set; }
       
       
        public DateTime? F_CheckInTime { get; set; }
       
        public string F_Description { get; set; }

    }
}

