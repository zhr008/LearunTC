using Learun.Application.Message;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-10-16 16:24
    /// 描 述：消息策略
    /// </summary>
    public class LR_MS_StrategyInfoMap : EntityTypeConfiguration<LR_MS_StrategyInfoEntity>
    {
        public LR_MS_StrategyInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_MS_STRATEGYINFO");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

