using System.Collections.Generic;

namespace Learun.Workflow.Engine
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.10
    /// 描 述：工作流引擎
    /// </summary>
    public interface NWFIEngine
    {
        #region 流程模板操作方法
        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <returns></returns>
        string GetScheme();
        /// <summary>
        /// 获取流程模板
        /// </summary>
        /// <returns></returns>
        NWFScheme GetSchemeObj();
        /// <summary>
        /// 获取开始节点
        /// </summary>
        /// <returns>节点信息</returns>
        NWFNodeInfo GetStartNode();
        /// <summary>
        /// 获取节点
        /// </summary>
        /// <param name="nodeId">流程处理节点ID</param>
        /// <returns>节点信息</returns>
        NWFNodeInfo GetNode(string nodeId);

        /// <summary>
        /// 获取两节点间的线条
        /// </summary>
        /// <param name="fromNodeId">开始节点</param>
        /// <param name="toNodeId">结束节点</param>
        /// <param name="list">线条列表</param>
        /// <param name="nodes"></param>
        bool GetLines(string fromNodeId, string toNodeId, List<NWFLineInfo> list,Dictionary<string, string> nodes = null);
        /// <summary>
        /// 获取下一节点
        /// </summary>
        /// <param name="nodeId">当前节点Id</param>
        /// <param name="code">节点操作码 agree 同意 disagree 不同意 lrtimeout 超时</param>
        /// <param name="lineList"></param>
        /// <returns>节点信息列表</returns>
        List<NWFNodeInfo> GetNextNodes(string nodeId, string code, List<NWFLineInfo> lineList);
        /// <summary>
        /// 获取上一节点列表
        /// </summary>
        /// <param name="nodeId">当前节点Id</param>
        /// <returns></returns>
        List<string> GetPreNodes(string nodeId);
        /// <summary>
        /// 判断两节点是否连接
        /// </summary>
        /// <param name="formNodeId">开始节点</param>
        /// <param name="toNodeId">结束节点</param>
        /// <returns></returns>
        bool IsToNode(string formNodeId, string toNodeId);
        #endregion

        #region 流程运行操作方法
        /// <summary>
        /// 获取配置参数信息
        /// </summary>
        /// <returns></returns>
        NWFEngineParamConfig GetConfig();
        /// <summary>
        /// 获取接下来的任务节点信息
        /// </summary>
        /// <param name="beginNode">起始节点</param>
        /// <param name="code">节点操作码 agree 同意 disagree 不同意 lrtimeout 超时</param>
        /// <param name="isGetAuditors">是否获取下一节点审核人</param>
        /// <param name="lineList">经过的线段需要执行操作的</param>
        /// <returns></returns>
        List<NWFNodeInfo> GetNextTaskNode(NWFNodeInfo beginNode, string code, bool isGetAuditors, List<NWFLineInfo> lineList);
        #endregion
    }
}
