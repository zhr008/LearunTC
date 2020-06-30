using Learun.Application.Extention.DisplayBoardManage;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 10:08
    /// 描 述：看板发布
    /// </summary>
    public class LR_KBFeaManageMap : EntityTypeConfiguration<LR_KBFeaManageEntity>
    {
        public LR_KBFeaManageMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_KBFEAMANAGE");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

