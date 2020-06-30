using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-12 10:56
    /// 描 述：系统流程Demo
    /// </summary>
    public class F_parentEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 文本
        /// </summary>
        [Column("F_TEXT")]
        public string F_text { get; set; }
        /// <summary>
        /// 文本区域
        /// </summary>
        [Column("F_TEXTAREA")]
        public string F_textarea { get; set; }
        /// <summary>
        /// 编辑框
        /// </summary>
        [Column("F_EDIT")]
        public string F_edit { get; set; }
        /// <summary>
        /// 单选
        /// </summary>
        [Column("F_RADIO")]
        public string F_radio { get; set; }
        /// <summary>
        /// 多选
        /// </summary>
        [Column("F_CHECKBOX")]
        public string F_checkbox { get; set; }
        /// <summary>
        /// 选择框
        /// </summary>
        [Column("F_SELECT")]
        public string F_select { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        [Column("F_DATE")]
        public DateTime? F_date { get; set; }
        /// <summary>
        /// 日期差
        /// </summary>
        [Column("F_DATEV")]
        public string F_datev { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        [Column("F_CODE")]
        public string F_code { get; set; }
        /// <summary>
        /// 公司
        /// </summary>
        [Column("F_COMPANY")]
        public string F_company { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [Column("F_DEPARTMENT")]
        public string F_department { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        [Column("F_USER")]
        public string F_user { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        [Column("F_FILE")]
        public string F_file { get; set; }
        /// <summary>
        /// 时间2
        /// </summary>
        [Column("F_DATE2")]
        public DateTime? F_date2 { get; set; }
        /// <summary>
        /// 当前公司
        /// </summary>
        [Column("F_CCOMPANY")]
        public string F_ccompany { get; set; }
        /// <summary>
        /// 当前部门
        /// </summary>
        [Column("F_CDEPARTMENT")]
        public string F_cdepartment { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        [Column("F_CUSER")]
        public string F_cuser { get; set; }
        /// <summary>
        /// 当前时间
        /// </summary>
        [Column("F_CDATE")]
        public DateTime? F_cdate { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
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

