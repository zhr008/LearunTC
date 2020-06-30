using Learun.Application.Report;
using System.Data.Entity.ModelConfiguration;

namespace  Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 15:17
    /// 描 述：报表文件管理
    /// </summary>
    public class LR_RPT_FileInfoMap : EntityTypeConfiguration<LR_RPT_FileInfoEntity>
    {
        public LR_RPT_FileInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_RPT_FILEINFO");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

