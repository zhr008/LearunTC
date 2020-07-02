using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-02 23:28
    /// 描 述：毕业证书
    /// </summary>
    public class tc_GradCertMap : EntityTypeConfiguration<tc_GradCertEntity>
    {
        public tc_GradCertMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_GRADCERT");
            //主键
            this.HasKey(t => t.F_GradCertId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

