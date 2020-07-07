using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-07 13:32
    /// 描 述：projectmanage
    /// </summary>
    public class tc_ProjectMap : EntityTypeConfiguration<tc_ProjectEntity>
    {
        public tc_ProjectMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_PROJECT");
            //主键
            this.HasKey(t => t.ProjectId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

