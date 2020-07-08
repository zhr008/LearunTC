using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-07 13:32
    /// 描 述：projectmanage
    /// </summary>
    public class tc_ProjectDetailEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键ID
        /// </summary>
        [Column("PROJECTDETAILID")]
        public string ProjectDetailId { get; set; }
        /// <summary>
        /// 项目ID
        /// </summary>
        [Column("PROJECTID")]
        public string ProjectId { get; set; }
        /// <summary>
        /// 证书类型
        /// </summary>
        [Column("CERTTYPE")]
        public int? CertType { get; set; }
        /// <summary>
        /// 证书专业
        /// </summary>
        [Column("CERTMAJOR")]
        public string CertMajor { get; set; }
        /// <summary>
        /// 标准数量
        /// </summary>
        [Column("STANDARDNUM")]
        public int? StandardNum { get; set; }
        /// <summary>
        /// 社保要求
        /// </summary>
        [Column("SOCIALSECURITYREQUIRE")]
        public int? SocialSecurityRequire { get; set; }
        /// <summary>
        /// 证书要求
        /// </summary>
        [Column("CERTREQUIRE")]
        public int? CertRequire { get; set; }
        /// <summary>
        /// 身份证要求
        /// </summary>
        [Column("IDCARDREQUIRE")]
        public int? IDCardRequire { get; set; }
        /// <summary>
        /// 毕业证要求
        /// </summary>
        [Column("GRADCERTREQUIRE")]
        public int? GradCertRequire { get; set; }
        /// <summary>
        /// 到场要求
        /// </summary>
        [Column("SCENEREQUIRE")]
        public int? SceneRequire { get; set; }
        /// <summary>
        /// 其他要求
        /// </summary>
        [Column("OTHERREQUIRE")]
        public string OtherRequire { get; set; }
        /// <summary>
        /// 甲方数量
        /// </summary>
        [Column("ALREADYNUM")]
        public int? AlreadyNum { get; set; }
        /// <summary>
        /// 我方配置数量
        /// </summary>
        [Column("NEEDNUM")]
        public int? NeedNum { get; set; }
        /// <summary>
        /// 配置状态
        /// </summary>
        [Column("STATUS")]
        public int? Status { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 编辑人
        /// </summary>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// 编辑人ID
        /// </summary>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 删除标记0否1是
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 编辑日期
        /// </summary>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
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
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ProjectDetailId = Guid.NewGuid().ToString();
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
            this.ProjectDetailId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }
}

