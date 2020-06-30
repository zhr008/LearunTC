using Learun.Util;
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
    public class FilelistEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
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
        /// 文件信息ID 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FILEINFOID")]
        public string F_FileInfoId { get; set; }
        /// <summary> 
        /// 关联文件夹ID 0顶层文件夹 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FOLDER")]
        public string F_Folder { get; set; }
        /// <summary> 
        /// 原始文件对应ID 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FILEID")]
        public string F_FileId { get; set; }
        /// <summary> 
        /// 预览文件ID 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PFILED")]
        public string F_PFiled { get; set; }
        /// <summary> 
        /// 版本号 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_VER")]
        public string F_Ver { get; set; }
        /// <summary> 
        /// 是否发布 0 不是 1 是 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ISPUBLISH")]
        public int? F_IsPublish { get; set; }
        /// <summary> 
        /// 发布时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_PUBLISHTIME")]
        public DateTime? F_PublishTime { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATETIME")]
        public DateTime? F_CreateTime { get; set; }
        /// <summary> 
        /// 创建人Id 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 创建人名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
            this.F_IsPublish = 0;
            this.F_CreateTime = DateTime.Now;
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
