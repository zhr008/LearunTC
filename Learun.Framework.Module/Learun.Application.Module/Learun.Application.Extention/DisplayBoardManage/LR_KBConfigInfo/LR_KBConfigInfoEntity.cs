using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.Extention.DisplayBoardManage

{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 09:41
    /// 描 述：看板配置信息
    /// </summary>
    public class LR_KBConfigInfoEntity 
    {
        #region 实体成员
        /// <summary>
        /// 模块主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 看板id
        /// </summary>
        /// <returns></returns>
        [Column("F_KANBANID")]
        public string F_KanBanId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        /// <returns></returns>
        [Column("F_MODENAME")]
        public string F_ModeName { get; set; }
        /// <summary>
        /// 类型statistics统计;2表格3图表
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPE")]
        public string F_Type { get; set; }
        /// <summary>
        /// 上边距
        /// </summary>
        /// <returns></returns>
        [Column("F_TOPVALUE")]
        public string F_TopValue { get; set; }
        /// <summary>
        /// 左边距
        /// </summary>
        /// <returns></returns>
        [Column("F_LEFTVALUE")]
        public string F_LeftValue { get; set; }
        /// <summary>
        /// 宽度
        /// </summary>
        /// <returns></returns>
        [Column("F_WIDTHVALUE")]
        public string F_WidthValue { get; set; }
        /// <summary>
        /// 高度
        /// </summary>
        [Column("F_HIGHTVALUE")]
        public string F_HightValue { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        /// <returns></returns>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 刷新时间（分钟）
        /// </summary>
        /// <returns></returns>
        [Column("F_REFRESHTIME")]
        public int? F_RefreshTime { get; set; }
        /// <summary>
        /// 配置信息
        /// </summary>
        /// <returns></returns>
        [Column("F_CONFIGURATION")]
        public string F_Configuration { get; set; }
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
    }
}

