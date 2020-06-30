using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-02 12:03
    /// 描 述：图片保存
    /// </summary>
    public class ImgEntity
    {
        #region 实体成员 
        /// <summary>
        /// 主键 
        /// </summary> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 图片名称
        /// </summary>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 扩展名
        /// </summary>
        [Column("F_EXNAME")]
        public string F_ExName { get; set; }
        /// <summary> 
        /// 保存的图片内容 
        /// </summary> 
        [Column("F_CONTENT")]
        public string F_Content { get; set; }
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
