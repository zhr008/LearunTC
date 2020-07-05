using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-05 19:31
    /// 描 述：从业经历
    /// </summary>
    public class tc_WorkExperienceMap : EntityTypeConfiguration<tc_WorkExperienceEntity>
    {
        public tc_WorkExperienceMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_WORKEXPERIENCE");
            //主键
            this.HasKey(t => t.F_WorkExperienceId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

