using Learun.Application.Extention.DisplayBoardManage;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 09:41
    /// 描 述：看板配置信息
    /// </summary>
    public class LR_KBConfigInfoMap : EntityTypeConfiguration<LR_KBConfigInfoEntity>
    {
        public LR_KBConfigInfoMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_KBCONFIGINFO");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}

