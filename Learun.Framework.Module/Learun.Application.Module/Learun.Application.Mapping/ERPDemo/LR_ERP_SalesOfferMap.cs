using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 17:29
    /// 描 述：销售报价
    /// </summary>
    public class LR_ERP_SalesOfferMap : EntityTypeConfiguration<LR_ERP_SalesOfferEntity>
    {
        public LR_ERP_SalesOfferMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_ERP_SALESOFFER");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

