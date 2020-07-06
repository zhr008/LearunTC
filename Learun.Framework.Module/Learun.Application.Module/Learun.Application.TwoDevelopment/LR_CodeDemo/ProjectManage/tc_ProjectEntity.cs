using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-06 17:37
    /// 描 述：projectmanage
    /// </summary>
    public class tc_ProjectEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键ID
        /// </summary>
        [Column("PROJECTID")]
        public string ProjectId { get; set; }
        /// <summary>
        /// ApplicantId
        /// </summary>
        [Column("APPLICANTID")]
        public string ApplicantId { get; set; }
        /// <summary>
        /// RegisterAddress
        /// </summary>
        [Column("REGISTERADDRESS")]
        public string RegisterAddress { get; set; }
        /// <summary>
        /// 资质类型
        /// </summary>
        [Column("PROJECTTYPE")]
        public int? ProjectType { get; set; }
        /// <summary>
        /// 社保交费人
        /// </summary>
        [Column("SOCIALSECURITIES")]
        public int? SocialSecurities { get; set; }
        /// <summary>
        /// 资质专业
        /// </summary>
        [Column("MAJOR")]
        public string Major { get; set; }
        /// <summary>
        /// 等级
        /// </summary>
        [Column("RANK")]
        public int? Rank { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        [Column("PUBLISHDATE")]
        public DateTime? PublishDate { get; set; }
        /// <summary>
        /// 配置日期
        /// </summary>
        [Column("CONFIGDATE")]
        public DateTime? ConfigDate { get; set; }
        /// <summary>
        /// 完成日期Statu
        /// </summary>
        [Column("COMPLETEDATE")]
        public DateTime? CompleteDate { get; set; }
        /// <summary>
        /// 状态
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
            this.ProjectId = Guid.NewGuid().ToString();
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
            this.ProjectId = keyValue;
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

