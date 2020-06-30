using Learun.Util;
using Learun.Workflow.Engine;
using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.09
    /// 描 述：流程进程
    /// </summary>
    public interface NWFProcessIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取流程进程实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        NWFProcessEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetMyPageList(string userId, Pagination pagination, string queryJson, string schemeCode = null);
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetMyPageList(string userId, string queryJson, string schemeCode = null);
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetMyTaskPageList(UserInfo userInfo, Pagination pagination, string queryJson, string schemeCode = null);
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetMyTaskPageList(UserInfo userInfo, string queryJson, string schemeCode = null);

        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetMyTaskPageList(UserInfo userInfo, Pagination pagination, string queryJson, bool isBatchAudit, string schemeCode = null);
        /// <summary>
        /// 获取未处理任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetMyTaskPageList(UserInfo userInfo, string queryJson, bool isBatchAudit, string schemeCode = null);
        /// <summary>
        /// 获取已处理任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetMyFinishTaskPageList(UserInfo userInfo, Pagination pagination, string queryJson, string schemeCode = null);
        /// <summary>
        /// 获取已处理任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        IEnumerable<NWFProcessEntity> GetMyFinishTaskPageList(UserInfo userInfo, string queryJson, string schemeCode = null);
        #endregion

        #region 保存更新删除
        /// <summary>
        /// 删除流程进程实体
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        void DeleteEntity(string processId);
        #endregion

        #region 流程API

        /// <summary>
        /// 获取下一节点审核人
        /// </summary>
        /// <param name="code">流程模板code</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="nodeId">流程节点Id</param>
        /// <param name="operationCode">流程操作代码</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        Dictionary<string, List<NWFUserInfo>> GetNextAuditors(string code, string processId, string taskId, string nodeId, string operationCode, UserInfo userInfo);
        /// <summary>
        /// 获取流程进程信息
        /// </summary>
        /// <param name="processId">进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="userInfo">当前人员信息</param>
        /// <returns></returns>
        NWFProcessDetailsModel GetProcessDetails(string processId, string taskId, UserInfo userInfo);
        /// <summary>
        /// 获取子流程详细信息
        /// </summary>
        /// <param name="processId">父流程进程主键</param>
        /// <param name="taskId">父流程子流程发起主键</param>
        /// <param name="schemeCode">子流程流程模板编码</param>
        /// <param name="nodeId">父流程发起子流程节点Id</param>
        /// <param name="userInfo">当前用户操作信息</param>
        /// <returns></returns>
        NWFProcessDetailsModel GetChildProcessDetails(string processId, string taskId, string schemeCode, string nodeId, UserInfo userInfo);
        /// <summary>
        /// 保存草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="userInfo">当前操作人信息</param>
        void SaveDraft(string processId, string schemeCode, UserInfo userInfo);
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="userInfo">当前用户操作信息</param>
        void DeleteDraft(string processId, UserInfo userInfo);
        /// <summary>
        /// 创建流程
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="title">标题</param>
        /// <param name="level">流程等级</param>
        /// <param name="auditors">下一节点审核人</param>
        /// <param name="userInfo">当前操作人信息</param>
        void CreateFlow(string schemeCode, string processId, string title, int level, string auditors, UserInfo userInfo);
        /// <summary>
        /// 创建流程(子流程)
        /// </summary>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="userInfo">当前操作人信息</param>
        void CreateChildFlow(string schemeCode, string processId, string parentProcessId, string parentTaskId, UserInfo userInfo);
        /// <summary>
        /// 重新创建流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="userInfo">当前操作人信息</param>
        void AgainCreateFlow(string processId, UserInfo userInfo);
        /// <summary>
        /// 审批流程
        /// </summary>
        /// <param name="operationCode">流程审批操作码agree 同意 disagree 不同意 lrtimeout 超时</param>
        /// <param name="operationName">流程审批操名称</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="des">审批意见</param>
        /// <param name="auditors">下一节点指定审核人</param>
        /// <param name="userInfo">当前操作人信息</param>
        void AuditFlow(string operationCode, string operationName, string processId, string taskId, string des, string auditors, string stamp, string signUrl, UserInfo userInfo);
        /// <summary>
        /// 批量审核（只有同意和不同意）
        /// </summary>
        /// <param name="operationCode">操作码</param>
        /// <param name="taskIds">任务id串</param>
        /// <param name="userInfo">当前操作人信息</param>
        void AuditFlows(string operationCode, string taskIds, UserInfo userInfo);
        /// <summary>
        /// 流程加签
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="userId">加签人员</param>
        /// <param name="des">加签说明</param>
        /// <param name="userInfo">当前操作人信息</param>
        void SignFlow(string processId, string taskId, string userId, string des, UserInfo userInfo);
        /// <summary>
        /// 流程加签审核
        /// </summary>
        /// <param name="operationCode">审核操作码</param>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="des">加签说明</param>
        /// <param name="userInfo">当前操作人信息</param>
        void SignAuditFlow(string operationCode, string processId, string taskId, string des, UserInfo userInfo);

        /// <summary>
        /// 确认阅读
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">流程任务主键</param>
        /// <param name="userInfo">当前操作人信息</param>
        void ReferFlow(string processId, string taskId, UserInfo userInfo);

        /// <summary>
        /// 催办流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="userInfo">当前操作人信息</param>
        void UrgeFlow(string processId, UserInfo userInfo);
        /// <summary>
        /// 撤销流程（只有在该流程未被处理的情况下）
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="userInfo">当前操作人信息</param>
        void RevokeFlow(string processId, UserInfo userInfo);
        /// <summary>
        /// 撤销审核（只有在下一个节点未被处理的情况下才能执行）
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="taskId">任务主键</param>
        /// <param name="userInfo">当前操作人信息</param>
        bool RevokeAudit(string processId, string taskId, UserInfo userInfo);

        /// <summary>
        /// 流程任务超时处理
        /// </summary>
        void MakeTaskTimeout();
        /// <summary>
        /// 获取流程当前任务需要处理的人
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        IEnumerable<NWFTaskEntity> GetTaskUserList(string processId);
        /// <summary>
        /// 指派流程审核人
        /// </summary>
        /// <param name="list">任务列表</param>
        /// <param name="userInfo">当前操作人信息</param>
        void AppointUser(IEnumerable<NWFTaskEntity> list, UserInfo userInfo);

        /// <summary>
        /// 作废流程
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <param name="userInfo">当前操作人信息</param>
        void DeleteFlow(string processId, UserInfo userInfo);

        /// <summary>
        /// 给指定的流程添加审核节点
        /// </summary>
        /// <param name="processId">流程实例ID</param>
        /// <param name="bNodeId">开始节点</param>
        /// <param name="eNodeId">结束节点（审核任务的节点）</param>
        void AddTask(string processId, string bNodeId, string eNodeId, UserInfo userInfo);
        #endregion

        #region 获取sql语句
        /// <summary>
        /// 获取我的流程信息列表SQL语句
        /// </summary>
        /// <returns></returns>
        string GetMySql();
        /// <summary>
        /// 获取我的代办任务列表SQL语句
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        string GetMyTaskSql(UserInfo userInfo, bool isBatchAudit = false);
        /// <summary>
        /// 获取我的已办任务列表SQL语句
        /// </summary>
        /// <returns></returns>
        string GetMyFinishTaskSql();
        #endregion
    }
}
