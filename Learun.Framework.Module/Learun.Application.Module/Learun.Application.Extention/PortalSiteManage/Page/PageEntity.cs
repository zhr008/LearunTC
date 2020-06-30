using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Extention.PortalSiteManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-02 09:35
    /// 描 述：门户网站页面配置
    /// </summary>
    public class PageEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 标题 
        /// </summary> 
        [Column("F_TITLE")]
        public string F_Title { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        [Column("F_IMG")]
        public string F_Img { get; set; }
        /// <summary> 
        /// 类型1.列表2图形列表3详细信息 
        /// </summary> 
        [Column("F_TYPE")]
        public string F_Type { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 创建人主键 
        /// </summary> 
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 创建人名称 
        /// </summary> 
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary> 
        /// 编辑时间 
        /// </summary> 
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary> 
        /// 编辑人主键 
        /// </summary> 
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary> 
        /// 编辑人名称 
        /// </summary> 
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary> 
        /// 页面配置模板 
        /// </summary> 
        [Column("F_SCHEME")]
        public string F_Scheme { get; set; }
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
        #region 扩展字段 
        #endregion
    }
}
