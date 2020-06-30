using Learun.Application.Base.SystemModule;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping.LR_System
{
    public class LogoImgMap : EntityTypeConfiguration<LogoImgEntity>
    {
        public LogoImgMap()
        {
            #region 表、主键 
            //表 
            this.ToTable("LR_BASE_LOGO");
            //主键 
            this.HasKey(t => t.F_Code);
            #endregion

            #region 配置关系 
            #endregion
        }
    }
}
