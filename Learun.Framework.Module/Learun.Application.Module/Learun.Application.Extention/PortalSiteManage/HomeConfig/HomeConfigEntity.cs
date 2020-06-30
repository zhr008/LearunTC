using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Extention.PortalSiteManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-04 09:35
    /// 描 述：门户网站首页配置
    /// </summary>
    public class HomeConfigEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键
        /// </summary> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 名称 
        /// </summary> 
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary> 
        /// 类型1.顶部文字2.底部文字3.底部地址4.logo图片5.微信图片6.顶部菜单7.底部菜单8.轮播图片9.模块 10底部logo 11微信文字
        /// </summary> 
        [Column("F_TYPE")]
        public string F_Type { get; set; }
        /// <summary> 
        /// 链接类型 
        /// </summary> 
        [Column("F_URLTYPE")]
        public int? F_UrlType { get; set; }
        /// <summary> 
        /// 链接地址 
        /// </summary> 
        [Column("F_URL")]
        public string F_Url { get; set; }
        /// <summary> 
        /// 图片 
        /// </summary> 
        [Column("F_IMG")]
        public string F_Img { get; set; }
        /// <summary> 
        /// 上级菜单 
        /// </summary> 
        [Column("F_PARENTID")]
        public string F_ParentId { get; set; }
        /// <summary> 
        /// 排序码 
        /// </summary> 
        [Column("F_SORT")]
        public int? F_Sort { get; set; }
        /// <summary> 
        /// 模块配置信息 
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
        }
        /// <summary> 
        /// 编辑调用 
        /// </summary> 
        /// <param name="keyValue"></param> 
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
        }
        #endregion
        #region 扩展字段 
        #endregion
    }
}
