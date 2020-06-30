using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-29 21:15
    /// 描 述：身份证管理
    /// </summary>
    public class tc_IDCardMap : EntityTypeConfiguration<tc_IDCardEntity>
    {
        public tc_IDCardMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_IDCARD");
            //主键
            this.HasKey(t => t.F_IDCardId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

