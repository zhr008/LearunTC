using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.Language

{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:00
    /// 描 述：语言映照
    /// </summary>
    public class LGMapEntity 
    {
        #region 实体成员
        /// <summary>
        /// 主键ID
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 显示内容
        /// </summary>
        /// <returns></returns>
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary>
        /// 编码(系统自动产生，作为关联项)
        /// </summary>
        /// <returns></returns>
        [Column("F_CODE")]
        public string F_Code { get; set; }
        /// <summary>
        /// 对应语言显示编码
        /// </summary>
        /// <returns></returns>
        [Column("F_TYPECODE")]
        public string F_TypeCode { get; set; }
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

