using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-14 23:25
    /// 描 述：1231
    /// </summary>
    public class tc_RelationMap : EntityTypeConfiguration<tc_RelationEntity>
    {
        public tc_RelationMap()
        {
            #region 表、主键
            //表
            this.ToTable("TC_RELATION");
            //主键
            this.HasKey(t => t.F_RelationId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

