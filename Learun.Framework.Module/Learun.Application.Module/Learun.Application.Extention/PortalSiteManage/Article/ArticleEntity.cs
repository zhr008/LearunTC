using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.Extention.PortalSiteManage

{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-05 09:35
    /// 描 述：详细信息维护
    /// </summary>
    public class ArticleEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        /// <returns></returns>
        [Column("F_TITLE")]
        public string F_Title { get; set; }
        /// <summary>
        /// 缩略图名称
        /// </summary>
        [Column("F_IMGNAME")]
        public string F_ImgName { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        [Column("F_IMG")]
        public string F_Img { get; set; }
        /// <summary>
        /// 文章分类
        /// </summary>
        /// <returns></returns>
        [Column("F_CATEGORY")]
        public string F_Category { get; set; }
        /// <summary>
        /// 详细内容
        /// </summary>
        /// <returns></returns>
        [Column("F_CONTENT")]
        public string F_Content { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        /// <returns></returns>
        [Column("F_PUSHDATE")]
        public DateTime? F_PushDate { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
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
            this.F_Id = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }
}

