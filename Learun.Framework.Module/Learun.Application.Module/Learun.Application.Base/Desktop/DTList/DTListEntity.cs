using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.Desktop
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-05-29 09:56
    /// 描 述：桌面消息列表配置
    /// </summary>
    public class DTListEntity
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Column("F_ICON")]
        public string F_Icon { get; set; }
        /// <summary>
        /// 数据库ID
        /// </summary>
        [Column("F_DATASOURCEID")]
        public string F_DataSourceId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        /// <returns></returns>
        [Column("F_URL")]
        public string F_Url { get; set; }
        /// <summary>
        /// sql语句
        /// </summary>
        /// <returns></returns>
        [Column("F_SQL")]
        public string F_Sql { get; set; }
        /// <summary>
        /// 单条数据点击弹层窗地址
        /// </summary>
        /// <returns></returns>
        [Column("F_ITEMURL")]
        public string F_ItemUrl { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORT")]
        public int? F_Sort { get; set; }
        /// <summary>
        /// 创建人ID
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
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
        }
        #endregion
    }
}
