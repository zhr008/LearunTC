using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Report
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-03-26 18:29
    /// 描 述：报表菜单关联设置
    /// </summary>
    public class LR_RPT_RelationEntity
    {
        #region 实体成员
        /// <summary>
        /// 功能主键
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 菜单主键
        /// </summary>
        [Column("F_MODULEID")]
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Column("F_ENCODE")]
        public string F_EnCode { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        [Column("F_FULLNAME")]
        public string F_FullName { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Column("F_ICON")]
        public string F_Icon { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
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
        /// 创建用户主键
        /// </summary>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// 上级菜单
        /// </summary>
        [Column("F_PARENTID")]
        public string F_ParentId { get; set; }
        /// <summary>
        /// 报表文件ID
        /// </summary>
        [Column("F_RPTFILEID")]
        public string F_RptFileId { get; set; }
        /// <summary>
        /// 报表文件地址
        /// </summary>
        [NotMapped]
        public string F_RptUrl { get; set; }
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

