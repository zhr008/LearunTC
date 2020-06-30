using Learun.Application.Base.Files;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping.LR_Files
{
    public class FileBInfoMap : EntityTypeConfiguration<FileBInfoEntity>
    {
        public FileBInfoMap()
        {
            #region 表、主键 
            //表 
            this.ToTable("LR_BASE_FILEINFO");
            //主键 
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
