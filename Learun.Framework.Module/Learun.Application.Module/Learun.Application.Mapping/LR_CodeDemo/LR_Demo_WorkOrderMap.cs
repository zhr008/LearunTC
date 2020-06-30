using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-06-10 17:21
    /// 描 述：工单管理
    /// </summary>
    public class LR_Demo_WorkOrderMap : EntityTypeConfiguration<LR_Demo_WorkOrderEntity>
    {
        public LR_Demo_WorkOrderMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_DEMO_WORKORDER");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

