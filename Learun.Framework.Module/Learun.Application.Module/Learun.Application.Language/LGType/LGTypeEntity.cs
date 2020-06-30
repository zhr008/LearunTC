using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.Language

{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:08
    /// 描 述：语言类型
    /// </summary>
    public class LGTypeEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键ID
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 语言名称
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 语言编码（不予许重复）
        /// </summary>
        /// <returns></returns>
        [Column("F_CODE")]
        public string F_Code { get; set; }
        /// <summary>
        /// 是否是主语言0不是1是
        /// </summary>
        /// <returns></returns>
        [Column("F_ISMAIN")]
        public int? F_IsMain { get; set; }
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

