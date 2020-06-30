using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Learun.Workflow.Engine
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.10
    /// 描 述：工作流引擎
    /// </summary>
    public class NWFEngine : NWFIEngine
    {
        #region 构造函数
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nWFEngineConfig"></param>
        public NWFEngine(NWFEngineConfig nWFEngineConfig)
        {
            // 初始化模板数据
            config = nWFEngineConfig;
            wfScheme = config.ParamConfig.Scheme.ToObject<NWFScheme>();
            nodesMap = new Dictionary<string, NWFNodeInfo>();
            foreach (var node in wfScheme.nodes)
            {
                if (!nodesMap.ContainsKey(node.id))
                {
                    nodesMap.Add(node.id, node);
                }

                if (node.type == "startround")
                {
                    startNode = node;
                }
            }
        }
        #endregion

        #region 模板数据信息
        private NWFEngineConfig config;
        private NWFScheme wfScheme = null;
        private Dictionary<string, NWFNodeInfo> nodesMap = null;
        private NWFNodeInfo startNode = null;
        #endregion

        #region 私有方法
        /// <summary>
        /// 计算条件
        /// </summary>
        /// <param name="node">节点信息</param>
        /// <returns></returns>
        private bool CalcCondition(NWFNodeInfo node)
        {
            bool res = true;
            if (node.conditions.Count > 0)
            {
                #region 字段条件判断
                foreach (var condition in node.conditions)
                {
                    if (!string.IsNullOrEmpty(condition.dbId) && !string.IsNullOrEmpty(condition.table) && !string.IsNullOrEmpty(condition.field1) && !string.IsNullOrEmpty(condition.field2))
                    {
                        string sql = "select " + condition.field2 + " from " + condition.table + " where " + condition.field1 + " =@processId ";
                        DataTable dataTable = config.DbFindTable(condition.dbId, sql, new { processId = config.ParamConfig.ProcessId });
                        if (dataTable.Rows.Count > 0)
                        {
                            string value = dataTable.Rows[0][0].ToString();
                            if (string.IsNullOrEmpty(value))
                            {
                                return false;
                            }

                            switch (condition.compareType)//比较类型1.等于2.不等于3.大于4.大于等于5.小于6.小于等于7.包含8.不包含9.包含于10.不包含于
                            {
                                case 1:// 等于
                                    if (value != condition.value)
                                    {
                                        res = false;
                                    }
                                    break;
                                case 2:// 不等于
                                    if (value == condition.value)
                                    {
                                        res = false;
                                    }
                                    break;
                                case 3:// 大于
                                    if (Convert.ToDecimal(value) <= Convert.ToDecimal(condition.value))
                                    {
                                        res = false;
                                    }
                                    break;
                                case 4:// 大于等于
                                    if (Convert.ToDecimal(value) < Convert.ToDecimal(condition.value))
                                    {
                                        res = false;
                                    }
                                    break;
                                case 5:// 小于
                                    if (Convert.ToDecimal(value) >= Convert.ToDecimal(condition.value))
                                    {
                                        res = false;
                                    }
                                    break;
                                case 6:// 小于等于
                                    if (Convert.ToDecimal(value) > Convert.ToDecimal(condition.value))
                                    {
                                        res = false;
                                    }
                                    break;
                                case 7:// 包含
                                    if (!value.Contains(condition.value))
                                    {
                                        res = false;
                                    }
                                    break;
                                case 8:// 不包含
                                    if (value.Contains(condition.value))
                                    {
                                        res = false;
                                    }
                                    break;
                                case 9:// 包含于
                                    if (!condition.value.Contains(value))
                                    {
                                        res = false;
                                    }
                                    break;
                                case 10:// 不包含于
                                    if (condition.value.Contains(value))
                                    {
                                        res = false;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            res = false;
                        }
                    }
                    if (!res)
                    {
                        break;
                    }
                }
                #endregion
            }
            else if (!string.IsNullOrEmpty(node.conditionSql))
            {
                res = false;
                // 流程进程ID
                string conditionSql = node.conditionSql.Replace("{processId}", "@processId");
                // 流程创建人用户
                conditionSql = conditionSql.Replace("{userId}", "@userId");
                conditionSql = conditionSql.Replace("{userAccount}", "@userAccount");
                conditionSql = conditionSql.Replace("{companyId}", "@companyId");
                conditionSql = conditionSql.Replace("{departmentId}", "@departmentId");

                var param = new
                {
                    processId = config.ParamConfig.ProcessId,
                    userId = config.ParamConfig.CreateUser.Id,
                    userAccount = config.ParamConfig.CreateUser.Account,
                    companyId = config.ParamConfig.CreateUser.CompanyId,
                    departmentId = config.ParamConfig.CreateUser.DepartmentId,
                };
                DataTable dataTable = config.DbFindTable(node.dbConditionId, conditionSql, param);
                if (dataTable.Rows.Count > 0)
                {
                    res = true;
                }
            }
            else
            {
                res = true;
            }
            return res;
        }
        /// <summary>
        /// 计算会签
        /// </summary>
        /// <param name="wfNodeInfo">节点信息</param>
        /// <param name="preNodeId">上一节点Id</param>
        /// <param name="isAgree">同意</param>
        /// <returns>0 不做处理 1 通过 -1 不通过</returns>
        private int CalcConfluence(NWFNodeInfo wfNodeInfo, string preNodeId, bool isAgree)
        {
            int res = 0;
            int agreeNum = config.GetAgreeNum(config.ParamConfig.ProcessId, wfNodeInfo.id);
            int disAgreeNum = config.GetDisAgreeNum(config.ParamConfig.ProcessId, wfNodeInfo.id);
            List<string> preNodeList = GetPreNodes(wfNodeInfo.id);
            switch (wfNodeInfo.confluenceType)//会签策略1-所有步骤通过，2-一个步骤通过即可，3-按百分比计算
            {
                case 1://所有步骤通过
                    if (isAgree)
                    {
                        if (preNodeList.Count == agreeNum + 1)
                        {
                            res = 1;
                        }
                    }
                    else
                    {
                        res = -1;
                    }
                    break;
                case 2:
                    if (isAgree)
                    {
                        res = 1;
                    }
                    else if (preNodeList.Count == disAgreeNum + 1)
                    {
                        res = -1;
                    }
                    break;
                case 3:
                    if (isAgree)
                    {
                        if ((agreeNum + 1) * 100 / preNodeList.Count >= Convert.ToDecimal(wfNodeInfo.confluenceRate))
                        {
                            res = 1;
                        }
                    }
                    else
                    {
                        if ((preNodeList.Count - disAgreeNum - 1) * 100 / preNodeList.Count < Convert.ToDecimal(wfNodeInfo.confluenceRate))
                        {
                            res = -1;
                        }
                    }
                    break;
            }
            return res;
        }
        #endregion

        #region 流程模板操作方法
        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <returns></returns>
        public string GetScheme()
        {
            return config.ParamConfig.Scheme;
        }
        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <returns></returns>
        public NWFScheme GetSchemeObj()
        {
            return wfScheme;
        }
        /// <summary>
        /// 获取开始节点
        /// </summary>
        /// <returns>节点信息</returns>
        public NWFNodeInfo GetStartNode()
        {
            return startNode;
        }
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="nodeId">流程处理节点ID</param>
        /// <returns>节点信息</returns>
        public NWFNodeInfo GetNode(string nodeId)
        {
            if (nodesMap.ContainsKey(nodeId))
            {
                return nodesMap[nodeId];
            }
            else {
                return null;
            }
           
        }

        /// <summary>
        /// 获取两节点间的线条
        /// </summary>
        /// <param name="fromNodeId">开始节点</param>
        /// <param name="toNodeId">结束节点</param>
        /// <param name="list">线条列表</param>
        /// <param name="nodes"></param>
        public bool GetLines(string fromNodeId, string toNodeId, List<NWFLineInfo> list, Dictionary<string, string> nodes = null)
        {
            bool res = false;
            if (nodes == null) {
                nodes = new Dictionary<string, string>();
            }
            foreach (var line in wfScheme.lines)
            {
                if (line.from == fromNodeId)
                {
                    
                    if (line.to == toNodeId)
                    {
                        list.Add(line);
                        return true;
                    }
                    else
                    {
                        if (line.to == fromNodeId || nodesMap[line.to] == null || nodesMap[line.to].type == "endround")
                        {
                        }
                        else if (!nodes.ContainsKey(line.to))
                        {
                            nodes.Add(line.to,"1");
                            res = GetLines(line.to, toNodeId, list, nodes);
                        }
                    }
                    if (res) {
                        list.Add(line);
                        return true;
                    }

                }
            }

            return res;
        }

        /// <summary>
        /// 获取下一节点
        /// </summary>
        /// <param name="nodeId">当前节点Id</param>
        /// <param name="code">节点操作码 agree 同意 disagree 不同意 lrtimeout 超时</param>
        /// <param name="lineList"></param>
        /// <returns>节点信息列表</returns>
        public List<NWFNodeInfo> GetNextNodes(string nodeId, string code, List<NWFLineInfo> lineList)
        {
            List<NWFNodeInfo> nextNodes = new List<NWFNodeInfo>();
            // 找到与当前节点相连的线条
            foreach (var line in wfScheme.lines)
            {
                if (line.from == nodeId)
                {
                    bool isOk = false;
                    if (string.IsNullOrEmpty(line.strategy) || line.strategy == "1")
                    {
                        isOk = true;
                    }
                    else
                    {
                        var codeList = line.agreeList.Split(',');
                        foreach (string _code in codeList)
                        {
                            if (_code == code)
                            {
                                isOk = true;
                                break;
                            }
                        }
                    }
                    if (isOk)
                    {
                        if (nodesMap.ContainsKey(line.to))
                        {
                            nextNodes.Add(nodesMap[line.to]);

                            switch (line.operationType)
                            {// 绑定的操作类型
                                case "sql":          // sql 语句
                                    if (!string.IsNullOrEmpty(line.dbId) && !string.IsNullOrEmpty(line.strSql))
                                    {
                                        lineList.Add(line);
                                    }
                                    break;
                                case "interface":    // interface 接口
                                    if (!string.IsNullOrEmpty(line.strInterface))
                                    {
                                        lineList.Add(line);
                                    }
                                    break;
                                case "ioc":          // 依赖注入
                                    if (!string.IsNullOrEmpty(line.iocName))
                                    {
                                        lineList.Add(line);
                                    }
                                    break;
                            }

                        }
                    }
                }
            }
            return nextNodes;
        }
        /// <summary>
        /// 获取上一节点列表
        /// </summary>
        /// <param name="nodeId">当前节点Id</param>
        /// <returns></returns>
        public List<string> GetPreNodes(string nodeId)
        {
            List<string> list = new List<string>();
            // 找到与当前节点相连的线条
            foreach (var line in wfScheme.lines)
            {
                if (line.to == nodeId)
                {
                    list.Add(line.from);
                }
            }
            return list;
        }
        /// <summary>
        /// 判断两节点是否连接
        /// </summary>
        /// <param name="formNodeId">开始节点</param>
        /// <param name="toNodeId">结束节点</param>
        /// <returns></returns>
        public bool IsToNode(string formNodeId, string toNodeId)
        {
            bool res = false;
            foreach (var line in wfScheme.lines)
            {
                if (line.from == formNodeId)
                {
                    if (line.to == toNodeId)
                    {
                        res = true;
                        break;
                    }
                    else
                    {
                        if (line.to == formNodeId || nodesMap[line.to] == null || nodesMap[line.to].type == "endround")
                        {
                            break;
                        }
                        else
                        {
                            if (IsToNode(line.to, toNodeId))
                            {
                                res = true;
                                break;
                            }
                        }
                    }
                }
            }
            return res;
        }
        #endregion

        #region 流程运行操作方法
        /// <summary>
        /// 获取配置参数信息
        /// </summary>
        /// <returns></returns>
        public NWFEngineParamConfig GetConfig()
        {
            return config.ParamConfig;
        }
        /// <summary>
        /// 获取接下来的任务节点信息
        /// </summary>
        /// <param name="beginNode">起始节点</param>
        /// <param name="code">节点操作码 agree 同意 disagree 不同意 lrtimeout 超时</param>
        /// <param name="isGetAuditors">是否获取下一节点审核人</param>
        /// <param name="lineList">经过的线段需要执行操作的</param>
        /// <returns></returns>
        public List<NWFNodeInfo> GetNextTaskNode(NWFNodeInfo beginNode, string code, bool isGetAuditors, List<NWFLineInfo> lineList)
        {
            List<NWFNodeInfo> list = new List<NWFNodeInfo>();
            List<NWFNodeInfo> nextNodeList = GetNextNodes(beginNode.id, code, lineList);

            Dictionary<string, string> auditers = null;
            if (!string.IsNullOrEmpty(config.ParamConfig.Auditers))
            {
                auditers = config.ParamConfig.Auditers.ToObject<Dictionary<string, string>>();
            }


            foreach (var node in nextNodeList)
            {
                if (auditers != null && auditers.ContainsKey(node.id))
                {
                    node.auditors = new List<NWFAuditor>();
                    node.auditors.Add(new NWFAuditor()
                    {
                        type = 3,
                        auditorId = auditers[node.id]
                    });
                }

                switch (node.type)
                {
                    case "conditionnode": // 条件节点
                        if (!isGetAuditors)
                        {
                            if (CalcCondition(node))
                            {
                                list.AddRange(GetNextTaskNode(node, "agree", isGetAuditors, lineList));
                            }
                            else
                            {
                                list.AddRange(GetNextTaskNode(node, "disagree", isGetAuditors, lineList));
                            }
                        }
                        else
                        {
                            list.AddRange(GetNextTaskNode(node, "agree", isGetAuditors, lineList));
                            list.AddRange(GetNextTaskNode(node, "disagree", isGetAuditors, lineList));
                        }
                        break;
                    case "confluencenode":// 会签节点
                        if (!isGetAuditors)
                        {
                            int confluenceRes;
                            if (code == "agree")
                            {
                                confluenceRes = CalcConfluence(node, beginNode.id, true);
                            }
                            else
                            {
                                confluenceRes = CalcConfluence(node, beginNode.id, false);
                            }
                            if (confluenceRes == 1)// 会签审核通过
                            {
                                list.AddRange(GetNextTaskNode(node, "agree", false, lineList));
                            }
                            else if (confluenceRes == -1)// 会签审核不通过
                            {
                                list.AddRange(GetNextTaskNode(node, "disagree", false, lineList));
                            }
                            node.confluenceRes = confluenceRes;
                            list.Add(node);
                        }
                        break;
                    case "auditornode":// 传阅节点
                        list.Add(node);
                        break;
                    case "childwfnode":// 子流程节点
                        list.Add(node);
                        if (node.childType == "2")
                        { // 异步的情况下直接往下走
                            list.AddRange(GetNextTaskNode(node, "agree", isGetAuditors, lineList));
                        }
                        break;
                    case "startround":// 开始节点 需要重新审核
                        list.Add(node);
                        config.ParamConfig.State = 1;
                        break;
                    case "endround":// 结束节点
                        config.ParamConfig.State = 2;
                        break;
                    default:         // 默认一般审核界定啊
                        list.Add(node);
                        break;
                }
            }

            return list;
        }
        #endregion

    }
}
