using Learun.Application.Base.Files;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping.LR_Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:28 
    /// 描 述：文件夹管理 
    /// </summary> 
    public class FolderMap : EntityTypeConfiguration<FolderEntity>
    {
        public FolderMap()
        {
            #region 表、主键 
            //表 
            this.ToTable("LR_BASE_FOLDER");
            //主键 
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
