using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.09
    /// 描 述：流程任务
    /// </summary>
    public class NWFTaskSerivce : RepositoryFactory
    {

        #region 获取数据
        /// <summary>
        /// 获取所有的任务
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public IEnumerable<NWFTaskEntity> GetALLTaskList(string processId)
        {
            try
            {
                return this.BaseRepository().FindList<NWFTaskEntity>(t => t.F_ProcessId == processId);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取未完成的任务
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public IEnumerable<NWFTaskEntity> GetUnFinishTaskList(string processId) {
            try
            {
                return this.BaseRepository().FindList<NWFTaskEntity>(t=>t.F_ProcessId == processId && t.F_IsFinished == 0);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取所有未完成的任务
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NWFTaskEntity> GetUnFinishTaskList()
        {
            try
            {
                return this.BaseRepository().FindList<NWFTaskEntity>(t => t.F_IsFinished == 0 && (t.F_Type == 1 || t.F_Type == 3));
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 判断任务是否允许撤销
        /// </summary>
        /// <param name="processId">流程实例</param>
        /// <param name="preNodeId">上一个节点（撤销任务节点）</param>
        /// <returns></returns>
        public bool IsRevokeTask(string processId,string preNodeId)
        {
            try
            {
                bool res = true;
                var list = this.BaseRepository().FindList<NWFTaskEntity>(t=> t.F_ProcessId == processId && t.F_PrevNodeId == preNodeId && t.F_Type == 1 && t.F_IsFinished == 1);
                var list2 = (List<NWFTaskEntity>)this.BaseRepository().FindList<NWFTaskEntity>(t => t.F_ProcessId == processId && t.F_PrevNodeId == preNodeId && (t.F_Type == 1 || t.F_Type == 5) && t.F_IsFinished == 0);
                if (list2.Count > 0)
                {
                    return res;
                }
                foreach (var item in list) {
                    string nodeId = item.F_NodeId;
                    var entity = this.BaseRepository().FindEntity<NWFTaskEntity>(t => t.F_ProcessId == processId && t.F_NodeId == nodeId && t.F_IsFinished == 0);

                    if (entity == null) {
                        res = false;
                        break;
                    }
                }

                return res;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取流程任务实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public NWFTaskEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<NWFTaskEntity>(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取最近一次的任务信息（审批任务）
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public NWFTaskEntity GetEntityByNodeId(string nodeId, string processId) {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM LR_NWF_Task WHERE F_ProcessId = @processId AND F_NodeId = @nodeId AND F_Type != 3 ORDER BY F_CreateDate DESC");
                return this.BaseRepository().FindEntity<NWFTaskEntity>(strSql.ToString(),new { processId, nodeId });
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        public NWFTaskEntity GetEntityByNodeId2(string nodeId, string processId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM LR_NWF_Task WHERE F_ProcessId = @processId AND F_NodeId = @nodeId AND F_Type = 1 AND F_IsFinished = 1 ORDER BY F_CreateDate DESC");
                return this.BaseRepository().FindEntity<NWFTaskEntity>(strSql.ToString(), new { processId, nodeId });
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }


        /// <summary>
        /// 获取任务执行人列表
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <returns></returns>
        public IEnumerable<NWFTaskRelationEntity> GetTaskUserList(string taskId) {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM LR_NWF_TaskRelation WHERE F_TaskId = @taskId  ORDER BY F_Sort");
                return this.BaseRepository().FindList<NWFTaskRelationEntity>(strSql.ToString(), new { taskId });
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取任务执行日志实体
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="prcoessId">流程进程主键</param>
        /// <returns></returns>
        public NWFTaskLogEntity GetLogEntityByNodeId(string nodeId,string prcoessId) {
            try
            {
                return this.BaseRepository().FindEntity<NWFTaskLogEntity>(t=>t.F_NodeId.Equals(nodeId)&&t.F_ProcessId.Equals(prcoessId) && t.F_TaskType != 3&& t.F_TaskType != 6);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取任务执行日志列表
        /// </summary>
        /// <param name="nodeId">节点Id</param>
        /// <param name="prcoessId">流程进程主键</param>
        /// <returns></returns>
        public IEnumerable<NWFTaskLogEntity> GetLogListByNodeId(string nodeId, string prcoessId)
        {
            try
            {
                return this.BaseRepository().FindList<NWFTaskLogEntity>(t => t.F_NodeId.Equals(nodeId) && t.F_ProcessId.Equals(prcoessId) && t.F_TaskType == 1);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        /// <summary>
        /// 获取流程进程的任务处理日志
        /// </summary>
        /// <param name="prcoessId">流程进程主键</param>
        /// <returns></returns>
        public IEnumerable<NWFTaskLogEntity> GetLogList(string processId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT * FROM LR_NWF_TaskLog WHERE F_ProcessId = @processId ORDER BY F_CreateDate DESC ");

                return this.BaseRepository().FindList<NWFTaskLogEntity>(strSql.ToString(),new { processId });
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取流程进程的任务处理日志
        /// </summary>
        /// <param name="taskId">任务主键</param>
        /// <param name="userId">用户主键</param>
        /// <returns></returns>
        public NWFTaskLogEntity GetLogEntity(string taskId,string userId)
        {
            try
            {
                return this.BaseRepository().FindEntity<NWFTaskLogEntity>(t=>t.F_TaskId == taskId && t.F_CreateUserId == userId && t.F_TaskType != 100);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }

        /// <summary>
        /// 获取当前任务节点ID
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        /// <returns></returns>
        public List<string> GetCurrentNodeIds(string processId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                t.F_NodeId
                                FROM
	                                LR_NWF_Task t
                                WHERE
	                                t.F_IsFinished = 0 AND t.F_ProcessId = @processId
                                GROUP BY
	                                t.F_NodeId      
                ");
                var taskList = this.BaseRepository().FindList<NWFTaskEntity>(strSql.ToString(), new { processId });
                List<string> list = new List<string>();
                foreach (var item in taskList)
                {
                    list.Add(item.F_NodeId);
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 更新审核人
        /// </summary>
        /// <param name="list">审核人列表</param>
        /// <param name="taskList">任务列表</param>
        /// <param name="nWFTaskLogEntity">任务日志</param>
        public void Save(List<NWFTaskRelationEntity> list, List<string> taskList, NWFTaskLogEntity nWFTaskLogEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                foreach (string taskId in taskList) {
                    db.Delete<NWFTaskRelationEntity>(t => t.F_TaskId == taskId && t.F_Result == 0 && t.F_Mark == 0);
                }

                foreach (var taskUser in list) {
                    db.Insert(taskUser);
                }

                db.Insert(nWFTaskLogEntity);

                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        #endregion
    }
}
