using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.09
    /// 描 述：流程任务
    /// </summary>
    public interface NWFTaskIBLL
    {
        #region 获取数据
        /// <summary>
        /// 获取所有的任务
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        IEnumerable<NWFTaskEntity> GetALLTaskList(string processId);
        /// <summary>
        /// 获取未完成的任务
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        IEnumerable<NWFTaskEntity> GetUnFinishTaskList(string processId);
        /// <summary>
        /// 获取所有未完成的任务
        /// </summary>
        /// <returns></returns>
        IEnumerable<NWFTaskEntity> GetUnFinishTaskList();

        /// <summary>
        /// 判断任务是否允许撤销
        /// </summary>
        /// <param name="processId">流程实例</param>
        /// <param name="preNodeId">上一个节点（撤销任务节点）</param>
        /// <returns></returns>
        bool IsRevokeTask(string processId, string preNodeId);
        /// <summary>
        /// 获取流程任务实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        NWFTaskEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取任务执行日志实体
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="prcoessId">流程进程主键</param>
        /// <returns></returns>
        NWFTaskLogEntity GetLogEntityByNodeId(string nodeId, string prcoessId);
        /// <summary>
        /// 获取流程进程的任务处理日志
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        IEnumerable<NWFTaskLogEntity> GetLogList(string processId);
        /// <summary>
        /// 获取流程进程的任务处理日志
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        NWFTaskLogEntity GetLogEntity(string taskId, string userId);

        /// <summary>
        /// 获取当前任务节点ID
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        List<string> GetCurrentNodeIds(string processId);

        /// <summary>
        /// 获取最近一次的任务信息（审批任务）
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        NWFTaskEntity GetEntityByNodeId(string nodeId, string processId);
        NWFTaskEntity GetEntityByNodeId2(string nodeId, string processId);
        /// <summary>
        /// 获取任务执行人列表
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        IEnumerable<NWFTaskRelationEntity> GetTaskUserList(string taskId);
        #endregion

        #region 保存数据
        /// <summary>
        /// 更新审核人
        /// </summary>
        /// <param name="list">审核人列表</param>
        /// <param name="taskList">任务列表</param>
        /// <param name="nWFTaskLogEntity">任务日志</param>
        void Save(List<NWFTaskRelationEntity> list, List<string> taskList, NWFTaskLogEntity nWFTaskLogEntity);
        #endregion
    }
}
