using Learun.Application.Language;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:00
    /// 描 述：语言映照
    /// </summary>
    public class LGMapMap : EntityTypeConfiguration<LGMapEntity>
    {
        public LGMapMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_LG_MAP");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

