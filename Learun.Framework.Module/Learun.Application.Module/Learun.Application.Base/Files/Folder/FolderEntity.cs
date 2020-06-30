using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:23 
    /// 描 述：文件夹管理
    /// </summary> 
    public class FolderEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 父级ID 
        /// </summary> 
        [Column("F_PID")]
        public string F_PId { get; set; }
        /// <summary> 
        /// 文件夹名称 
        /// </summary> 
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary> 
        /// 最后更新时间 
        /// </summary> 
        [Column("F_TIME")]
        public DateTime? F_Time { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_Time = DateTime.Now;
        }
        /// <summary> 
        /// 编辑调用 
        /// </summary> 
        /// <param name="keyValue"></param> 
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
            this.F_Time = DateTime.Now;
        }
        #endregion
        #region 扩展字段 
        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string F_AuthType { get; set; }

        #endregion
    }
}
