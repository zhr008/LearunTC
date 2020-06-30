using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-29 14:04 
    /// 描 述：w 
    /// </summary> 
    public class FileAuthEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 文件信息主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_FILEINFOID")]
        public string F_FileInfoId { get; set; }
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
        /// 对应对象类型2角色
        /// </summary> 
        /// <returns></returns> 
        [Column("F_OBJTYPE")]
        public int? F_ObjType { get; set; }
        /// <summary> 
        /// 1 查看 2上传 3 下载  4 删除  5 复原 
        /// </summary> 
        /// <returns></returns>
        [Column("F_AUTHTYPE")]
        public string F_AuthType { get; set; }
        /// <summary> 
        /// 到期日期 
        /// </summary> 
        /// <returns></returns>
        [Column("F_TIME")]
        public DateTime? F_Time { get; set; }

        /// <summary>
        /// 权限来源 上级文件夹或者下级文件  null为自己
        /// </summary>
        public string F_from { get; set; }
        /// <summary>
        /// 0 自己 1 上级 2 下级
        /// </summary>
        public int F_Type { get; set; }
        /// <summary>
        /// 0 不是 1是文件夹
        /// </summary>
        public int F_IsFolder { get; set; }
        /// <summary>
        /// 0 自己 值越大离自己越远
        /// </summary>
        public int F_Level { get; set; }
        
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            if (this.F_Time == null)
            {
                this.F_Time = DateTime.MaxValue.AddDays(-1);
            }

            this.F_ObjType = 2;
            this.F_Id = Guid.NewGuid().ToString();
        }
        /// <summary> 
        /// 编辑调用 
        /// </summary> 
        /// <param name="keyValue"></param> 
        public void Modify(string keyValue)
        {
            if (this.F_Time == null)
            {
                this.F_Time = DateTime.MaxValue.AddDays(-1);
            }
            this.F_Id = keyValue;
        }
        #endregion
    }
}
