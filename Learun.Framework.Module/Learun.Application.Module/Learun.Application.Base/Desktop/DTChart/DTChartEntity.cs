using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.Desktop
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-05-29 09:58
    /// 描 述：桌面图表配置
    /// </summary>
    public class DTChartEntity
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
        /// 图表类型0饼图1折线图2柱状图
        /// </summary>
        [Column("F_TYPE")]
        public int? F_Type { get; set; }
        /// <summary>
        /// sql语句
        /// </summary>
        /// <returns></returns>
        [Column("F_SQL")]
        public string F_Sql { get; set; }
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
        /// <summary>
        /// 皮肤一比例
        /// </summary>
        /// <returns></returns>
        [Column("F_PROPORTION1")]
        public int? F_Proportion1 { get; set; }
        /// <summary>
        /// 皮肤二比例
        /// </summary>
        /// <returns></returns>
        [Column("F_PROPORTION2")]
        public int? F_Proportion2 { get; set; }
        /// <summary>
        /// 皮肤三比例
        /// </summary>
        /// <returns></returns>
        [Column("F_PROPORTION3")]
        public int? F_Proportion3 { get; set; }
        /// <summary>
        /// 皮肤四比例
        /// </summary>
        /// <returns></returns>
        [Column("F_PROPORTION4")]
        public int? F_Proportion4 { get; set; }
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
