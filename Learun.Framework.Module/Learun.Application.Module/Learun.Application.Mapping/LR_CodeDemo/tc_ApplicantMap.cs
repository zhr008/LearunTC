using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2020-06-27 23:47
    /// 描 述：供应商登记
    /// </summary>
    public class tc_ApplicantMap : EntityTypeConfiguration<tc_ApplicantEntity>
    {
        public tc_ApplicantMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_APPLICANT");
            //主键
            this.HasKey(t => t.F_ApplicantId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

