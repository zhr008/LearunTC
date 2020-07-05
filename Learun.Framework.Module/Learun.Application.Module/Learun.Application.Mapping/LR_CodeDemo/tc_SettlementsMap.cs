using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-05 20:42
    /// 描 述：合同结算
    /// </summary>
    public class tc_SettlementsMap : EntityTypeConfiguration<tc_SettlementsEntity>
    {
        public tc_SettlementsMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_SETTLEMENTS");
            //主键
            this.HasKey(t => t.F_SettlementsId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

