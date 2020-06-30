using Learun.Application.Language;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:08
    /// 描 述：语言类型
    /// </summary>
    public class LGTypeMap : EntityTypeConfiguration<LGTypeEntity>
    {
        public LGTypeMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_LG_TYPE");
            //主键
            this.HasKey(t => t.F_Id);

            #endregion

            #region 配置关系
            #endregion
        }
    }
}

