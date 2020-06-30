using Learun.Application.Base.Files;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping.LR_Files
{
    public class FilelistMap : EntityTypeConfiguration<FilelistEntity>
    {
        public FilelistMap()
        {
            #region 表、主键 
            //表 
            this.ToTable("LR_BASE_FILELIST");
            //主键 
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
