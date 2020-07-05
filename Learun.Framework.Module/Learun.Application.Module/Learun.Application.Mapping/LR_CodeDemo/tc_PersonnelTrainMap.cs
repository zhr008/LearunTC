using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-05 19:59
    /// 描 述：培训记录
    /// </summary>
    public class tc_PersonnelTrainMap : EntityTypeConfiguration<tc_PersonnelTrainEntity>
    {
        public tc_PersonnelTrainMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_PERSONNELTRAIN");
            //主键
            this.HasKey(t => t.F_PersonnelTrainId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

