using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-05-08 18:30
    /// 描 述：甘特图应用
    /// </summary>
    public class LR_OA_ProjectMap : EntityTypeConfiguration<LR_OA_ProjectEntity>
    {
        public LR_OA_ProjectMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_OA_PROJECT");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

