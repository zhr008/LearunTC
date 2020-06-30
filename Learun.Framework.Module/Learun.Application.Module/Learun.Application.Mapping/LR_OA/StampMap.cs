using Learun.Application.OA.LR_StampManage;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{/// <summary>
 
 /// Copyright (c) 2013-2020 上海力软信息技术有限公司
 /// 创 建：超级管理员
 /// 日 期：2017-07-12 09:57
 /// 描 述：报表管理
 /// </summary>
    public class StampMap : EntityTypeConfiguration<LR_StampManageEntity>
    {
        public StampMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_BASE_STAMP");
            //主键
            this.HasKey(s =>s.F_StampId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
