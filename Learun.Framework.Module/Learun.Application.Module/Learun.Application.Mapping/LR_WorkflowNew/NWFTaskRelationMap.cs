using Learun.Application.WorkFlow;
using System.Data.Entity.ModelConfiguration;

namespace Learun.Application.Mapping.LR_WorkflowNew
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流任务执行人对应关系表(新)
    /// </summary>
    public class NWFTaskRelationMap : EntityTypeConfiguration<NWFTaskRelationEntity>
    {
        public NWFTaskRelationMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_NWF_TASKRELATION");
            //主键
            this.HasKey(t => t.F_Id);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
