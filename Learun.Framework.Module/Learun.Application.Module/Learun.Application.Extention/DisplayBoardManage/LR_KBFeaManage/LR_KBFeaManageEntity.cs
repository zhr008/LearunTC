using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.Extention.DisplayBoardManage

{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 10:08
    /// 描 述：看板发布
    /// </summary>
    public class LR_KBFeaManageEntity 
    {
        #region 实体成员
        /// <summary>
        /// 看板管理主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 功能名称
        /// </summary>
        /// <returns></returns>
        [Column("F_FULLNAME")]
        public string F_FullName { get; set; }
        /// <summary>
        /// 功能编号
        /// </summary>
        /// <returns></returns>
        [Column("F_ENCODE")]
        public string F_EnCode { get; set; }
        /// <summary>
        /// 上级功能
        /// </summary>
        /// <returns></returns>
        [Column("F_PARENTID")]
        public string F_ParentId { get; set; }
        /// <summary>
        /// 功能图标
        /// </summary>
        /// <returns></returns>
        [Column("F_ICON")]
        public string F_Icon { get; set; }
        /// <summary>
        /// 看板选择
        /// </summary>
        /// <returns></returns>
        [Column("F_KANBANID")]
        public string F_KanBanId { get; set; }
        /// <summary>
        /// 模块id
        /// </summary>
        [Column("F_MODULEID")]
        public string F_ModuleId { get; set; }
        /// <summary>
        /// 排序码
        /// </summary>
        [Column("F_SORTCODE")]
        public int? F_SortCode { get; set; }
        /// <summary>
        /// 功能描述
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

