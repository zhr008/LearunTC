using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流模板信息(新)
    /// </summary>
    public class NWFSchemeInfoEntity
    {
        #region 实体成员
        /// <summary> 
        /// 主键 
        /// </summary>
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程编码 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CODE")]
        public string F_Code { get; set; }
        /// <summary> 
        /// 流程模板名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary> 
        /// 流程分类 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CATEGORY")]
        public string F_Category { get; set; }
        /// <summary> 
        /// 流程模板ID 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_SCHEMEID")]
        public string F_SchemeId { get; set; }
        /// <summary> 
        /// 是否有效 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary> 
        /// 是否在我的任务允许发起 1允许 2不允许 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_MARK")]
        public int? F_Mark { get; set; }
        /// <summary> 
        /// 是否在App上允许发起 1允许 2不允许 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISINAPP")]
        public int? F_IsInApp { get; set; }
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
            this.F_EnabledMark = 1;
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
        /// <summary>
        /// 1.正式（已发布）2.草稿
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public int? F_Type { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [NotMapped]
        public string F_CreateUserName { get; set; }
        #endregion
    }
}
