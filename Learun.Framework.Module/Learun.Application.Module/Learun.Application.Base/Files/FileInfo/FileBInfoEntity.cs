using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-29 14:04 
    /// 描 述：文件管理
    /// </summary> 
    public class FileBInfoEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 文件编号 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CODE")]
        public string F_Code { get; set; }
        /// <summary> 
        /// 文件名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_NAME")]
        public string F_Name { get; set; }
        /// <summary> 
        /// 关键字 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_KEYWORD")]
        public string F_KeyWord { get; set; }
        /// <summary> 
        /// 是否发布 0 不是 1 是 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISPUBLISH")]
        public int? F_IsPublish { get; set; }
        /// <summary> 
        /// 关联文件夹ID 0顶层文件夹 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FOLDER")]
        public string F_Folder { get; set; }
        /// <summary> 
        /// 是否删除标记 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary>
        /// 新增调用 
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_DeleteMark = 0;
            this.F_IsPublish = 0;
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
        /// 文件版本号
        /// </summary>
        [NotMapped]
        public string F_Ver { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        [NotMapped]
        public int F_FileSize { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary>
        [NotMapped]
        public string F_FileType { get; set; }
        /// <summary> 
        /// 原始文件对应ID 
        /// </summary> 
        [NotMapped]
        public string F_FileId { get; set; }
        /// <summary> 
        /// 预览文件ID 
        /// </summary> 
        [NotMapped]
        public string F_PFiled { get; set; }
        /// <summary>
        /// 权限操作类型
        /// </summary>
        [NotMapped]
        public string F_AuthType { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        [NotMapped]
        public string Type { get; set; }
        #endregion
    }
}
