using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-05 19:31
    /// 描 述：从业经历
    /// </summary>
    public class tc_WorkExperienceEntity
    {
        #region 实体成员
        /// <summary>
        /// F_WorkExperienceId
        /// </summary>
        [Column("F_WORKEXPERIENCEID")]
        public string F_WorkExperienceId { get; set; }
        /// <summary>
        /// 个人Id
        /// </summary>
        [Column("F_PERSONID")]
        public string F_PersonId { get; set; }
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
        /// 从业单位名称
        /// </summary>
        [Column("F_COMPANYNAME")]
        public string F_CompanyName { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        [Column("F_ENTRYDATE")]
        public DateTime? F_EntryDate { get; set; }
        /// <summary>
        /// 离职日期
        /// </summary>
        [Column("F_QUITDATE")]
        public DateTime? F_QuitDate { get; set; }
        /// <summary>
        /// 就职类型
        /// </summary>
        [Column("F_VOCATIONTYPE")]
        public int? F_VocationType { get; set; }
        /// <summary>
        /// 就职证书类型
        /// </summary>
        [Column("F_CERTTYPE")]
        public int? F_CertType { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        [Column("F_CHECKINDATE")]
        public DateTime? F_CheckInDate { get; set; }
        /// <summary>
        /// 主要担任项目
        /// </summary>
        [Column("F_MAJORPROJECTS")]
        public string F_MajorProjects { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
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
        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_WorkExperienceId = Guid.NewGuid().ToString();
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
            this.F_WorkExperienceId = keyValue;
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
    public class WorkExperienceInfo : tc_WorkExperienceEntity
    {

        [NotMapped]
        public string F_ApplicantId { get; set; }
    }
}

