using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-08 22:22
    /// 描 述：合同结算详情
    /// </summary>
    public class tc_SettlementsDetailsMap : EntityTypeConfiguration<tc_SettlementsDetailsEntity>
    {
        public tc_SettlementsDetailsMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_SETTLEMENTSDETAILS");
            //主键
            this.HasKey(t => t.F_SettlementDetailsId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

