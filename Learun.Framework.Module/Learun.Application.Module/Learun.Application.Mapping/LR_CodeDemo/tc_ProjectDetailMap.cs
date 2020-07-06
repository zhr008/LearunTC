using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-06 17:37
    /// 描 述：projectmanage
    /// </summary>
    public class tc_ProjectDetailMap : EntityTypeConfiguration<tc_ProjectDetailEntity>
    {
        public tc_ProjectDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_PROJECTDETAIL");
            //主键
            this.HasKey(t => t.ProjectDetailId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

