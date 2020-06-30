using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流模板权限(新)
    /// </summary>
    public class NWFSchemeAuthEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 流程模板信息主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_SCHEMEINFOID")]
        public string F_SchemeInfoId { get; set; }
        /// <summary> 
        /// 对象名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_OBJNAME")]
        public string F_ObjName { get; set; }
        /// <summary> 
        /// 对应对象主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_OBJID")]
        public string F_ObjId { get; set; }
        /// <summary> 
        /// 对应对象类型1岗位2角色3用户4所用人可看 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_OBJTYPE")]
        public int? F_ObjType { get; set; }
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
