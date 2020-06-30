using Learun.Application.Base.SystemModule;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-02 12:03
    /// 描 述：图片保存
    /// </summary>
    public class ImgMap : EntityTypeConfiguration<ImgEntity>
    {
        /// <summary>
        /// 映射
        /// </summary>
        public ImgMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_BASE_IMG");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
