using Learun.Application.Extention.PortalSiteManage;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-05 09:35
    /// 描 述：详细信息维护
    /// </summary>
    public class ArticleMap : EntityTypeConfiguration<ArticleEntity>
    {
        public ArticleMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_PS_ARTICLE");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

