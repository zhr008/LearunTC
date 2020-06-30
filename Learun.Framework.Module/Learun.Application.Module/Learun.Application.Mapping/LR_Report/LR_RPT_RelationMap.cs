using Learun.Application.Report;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-03-26 18:29
    /// 描 述：报表菜单关联设置
    /// </summary>
    public class LR_RPT_RelationMap : EntityTypeConfiguration<LR_RPT_RelationEntity>
    {
        public LR_RPT_RelationMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_RPTRELATION");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

