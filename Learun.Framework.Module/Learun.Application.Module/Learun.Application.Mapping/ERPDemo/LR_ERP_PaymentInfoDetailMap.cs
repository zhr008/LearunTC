using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-26 11:35
    /// 描 述：付款单
    /// </summary>
    public class LR_ERP_PaymentInfoDetailMap : EntityTypeConfiguration<LR_ERP_PaymentInfoDetailEntity>
    {
        public LR_ERP_PaymentInfoDetailMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_ERP_PAYMENTINFODETAIL");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

