using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-02 23:56
    /// 描 述：个人资格证
    /// </summary>
    public class tc_CredentialsMap : EntityTypeConfiguration<tc_CredentialsEntity>
    {
        public tc_CredentialsMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_CREDENTIALS");
            //主键
            this.HasKey(t => t.F_CredentialsId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

