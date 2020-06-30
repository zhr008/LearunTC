using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-29 15:28
    /// 描 述：销售订单
    /// </summary>
    public class AllotAssetsMap : EntityTypeConfiguration<AllotAssetsEntity>
    {
        public AllotAssetsMap()
        {
            #region 表、主键
            //表
            this.ToTable("ALLOTASSETS");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

