using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:50
    /// 描 述：检查项目
    /// </summary>
    public class MSTB_QUA_CHECKITEMINFOMap : EntityTypeConfiguration<MSTB_QUA_CHECKITEMINFOEntity>
    {
        public MSTB_QUA_CHECKITEMINFOMap()
        {
            #region 表、主键
            //表
            this.ToTable("MSTB_QUA_CHECKITEMINFO");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

