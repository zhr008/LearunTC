using Learun.Application.CRM;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-07-11 11:30
    /// 描 述：商机管理
    /// </summary>
    public class CrmChanceMap : EntityTypeConfiguration<CrmChanceEntity>
    {
        public CrmChanceMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_CRM_CHANCE");
            //主键
            this.HasKey(t => t.F_ChanceId);
            #endregion
            this.Property(t => t.F_Amount).HasPrecision(18, 6);
            #region 配置关系
            #endregion
        }
    }
}

