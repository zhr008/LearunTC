using Learun.Application.Extention.DisplayBoardManage;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 10:10
    /// 描 述：看板信息
    /// </summary>
    public class LR_KBKanBanInfoMap : EntityTypeConfiguration<LR_KBKanBanInfoEntity>
    {
        public LR_KBKanBanInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_KBKANBANINFO");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

