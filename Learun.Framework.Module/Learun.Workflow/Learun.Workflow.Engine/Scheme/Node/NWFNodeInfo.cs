using System.Collections.Generic;

namespace Learun.Workflow.Engine
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：工作流节点
    /// </summary>
    public class NWFNodeInfo
    {
        /// <summary>
        /// 节点Id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 节点名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 节点类型-》开始startround;结束endround;一般stepnode;会签节点:confluencenode;条件判断节点：conditionnode;查阅节点：auditornode;子流程节点：childwfnode
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 通知方式，绑定的消息策略编码
        /// </summary>
        public string notice { get; set; }
        /// <summary>
        /// 审核方式（1只要其中一人审核2都需要审核）
        /// </summary>
        public string isAllAuditor { get; set; }
        /// <summary>
        /// 审核执行策略（1有人不同意就往下流程（默认）2需要所有人审完才往下走）
        /// </summary>
        public string auditExecutType { get; set; }
        /// <summary>
        /// 审核方式(1并行2串行)
        /// </summary>
        public string auditorType { get; set; }
        /// <summary>
        /// 再次审核 1.已通过不需要审核 2.已通过需要审核
        /// </summary>
        public string auditorAgainType { get; set; }
        /// <summary>
        /// 审核者们
        /// </summary>
        public List<NWFAuditor> auditors { get; set; }

        /// <summary>
        /// 超时时间 0 的话不执行
        /// </summary>
        public string timeoutNotice { get; set; }
        /// <summary>
        /// 超时通知间隔 0  的话只执行一次
        /// </summary>
        public string timeoutInterval { get; set; }
        /// <summary>
        /// 超时通知绑定的消息策略编码
        /// </summary>
        public string timeoutStrategy { get; set; }
        /// <summary>
        /// 超时时间（超时后可流转下一节点）0 的话不执行
        /// </summary>
        public string timeoutAction { get; set; }

        /// <summary>
        /// 会签策略1-所有步骤通过，2-一个步骤通过即可，3-按百分比计算
        /// </summary>
        public int confluenceType { get; set; }
        /// <summary>
        /// 会签比例
        /// </summary>
        public string confluenceRate { get; set; }


        /// <summary>
        /// 子流程模板编码
        /// </summary>
        public string childFlow { get; set; }
        /// <summary>
        /// 子流程执行策略 1 同步 2 异步
        /// </summary>
        public string childType { get; set; }

        #region 条件判断
        /// <summary>
        /// 工作流条件节点-条件字段（优先执行）
        /// </summary>
        public List<NWFCondition> conditions { get; set; }
        /// <summary>
        /// 条件判断sql语句所在数据库主键
        /// </summary>
        public string dbConditionId { get; set; }
        /// <summary>
        /// 条件判断sql语句
        /// </summary>
        public string conditionSql { get; set; }
        #endregion
        /// <summary>
        /// 实际审核人信息
        /// </summary>
        public List<string> userList { get; set; }
        /// <summary>
        /// 会签审核结果0 不做处理 1 通过 -1 不通过
        /// </summary>
        public int confluenceRes { get; set; }


        /// <summary>
        /// 绑定的操作类型sql interface ioc
        /// </summary>
        public string operationType { get; set; }
        /// <summary>
        /// 绑定数据ID
        /// </summary>
        public string dbId { get; set; }
        /// <summary>
        /// 绑定的sql语句
        /// </summary>
        public string strSql { get; set; }
        /// <summary>
        /// 绑定的接口
        /// </summary>
        public string strInterface { get; set; }
        /// <summary>
        /// 绑定的ioc名称
        /// </summary>
        public string iocName { get; set; }
        /// <summary>
        /// 是否允许批量审核1允许 其他值都不允许
        /// </summary>
        public int? isBatchAudit { get; set; }

        /// <summary>
        /// 自动同意规则 1.处理人就是提交人 2.处理人和上一步的处理人相同 3.处理人审批过
        /// </summary>
        public string agreeGz { get; set; }
        /// <summary>
        /// 无对应处理人  1或者其他 超级管理员处理 2.跳过此步骤 3.不能提交
        /// </summary>
        public int? noPeopleGz { get; set; }
    }
}
