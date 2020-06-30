using Learun.Application.OA.Schedule;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：新闻公告
    /// </summary>
    public class ScheduleMap : EntityTypeConfiguration<ScheduleEntity>
    {
        public ScheduleMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_OA_SCHEDULE");
            //主键
            this.HasKey(t => t.F_ScheduleId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
