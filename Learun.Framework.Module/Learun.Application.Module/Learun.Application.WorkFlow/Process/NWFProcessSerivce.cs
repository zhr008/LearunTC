using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.07
    /// 描 述：流程进程
    /// </summary>
    public class NWFProcessSericve: RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取流程进程实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public NWFProcessEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<NWFProcessEntity>(keyValue);
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
        /// 获取流程进程实例
        /// </summary>
        /// <param name="processId">父流程进程主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public NWFProcessEntity GetEntityByProcessId(string processId, string nodeId) {
            try
            {
                return this.BaseRepository().FindEntity<NWFProcessEntity>(t=>t.F_ParentProcessId == processId && t.F_ParentNodeId == nodeId);
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
        /// 获取子流程列表
        /// </summary>
        /// <param name="parentProcessId">父流程进程主键</param>
        /// <returns></returns>
        public IEnumerable<NWFProcessEntity> GetChildProcessList(string parentProcessId) {
            try
            {
                return this.BaseRepository().FindList<NWFProcessEntity>(t => t.F_ParentProcessId == parentProcessId);
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
        /// 获取流程信息列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<NWFProcessEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var expression = LinqExtensions.True<NWFProcessEntity>();
                var queryParam = queryJson.ToJObject();
                // 分类
                if (!queryParam["categoryId"].IsEmpty()) // 1:未完成 2:已完成
                {
                    if (queryParam["categoryId"].ToString() == "1")
                    {
                        expression = expression.And(t => t.F_IsFinished == 0);
                    }
                    else
                    {
                        expression = expression.And(t => t.F_IsFinished == 1);
                    }
                }
                // 操作时间
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate();
                    expression = expression.And(t => t.F_CreateDate >= startTime && t.F_CreateDate <= endTime);
                }
                // 关键字
                if (!queryParam["keyword"].IsEmpty())
                {
                    string keyword = queryParam["keyword"].ToString();
                    expression = expression.And(t => t.F_Title.Contains(keyword) || t.F_SchemeName.Contains(keyword) || t.F_CreateUserName.Contains(keyword));
                }
                expression = expression.And(t => t.F_EnabledMark != 2);
                expression = expression.And(t => t.F_IsChild == 0);

                var list = this.BaseRepository().FindList<NWFProcessEntity>(expression, pagination);

                foreach (var item in list) {
                    if (item.F_IsFinished != 1) {
                        var taskEntity = this.BaseRepository().FindEntity<NWFTaskEntity>(" select * from lr_nwf_task where F_ProcessId = @processId  AND F_IsFinished = 0 ", new { processId = item.F_Id });
                        if (taskEntity == null) {
                            item.F_EnabledMark = 500;
                        }
                    }
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
        /// <summary>
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public IEnumerable<NWFProcessEntity> GetMyPageList(string userId, Pagination pagination, string queryJson, string schemeCode)
        {
            try
            {
                var expression = LinqExtensions.True<NWFProcessEntity>();
                var queryParam = queryJson.ToJObject();
                expression = expression.And(t => t.F_CreateUserId == userId);
                // 操作时间
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate();
                    expression = expression.And(t => t.F_CreateDate >= startTime && t.F_CreateDate <= endTime);
                }
                // 关键字
                if (!queryParam["keyword"].IsEmpty())
                {
                    string keyword = queryParam["keyword"].ToString();
                    expression = expression.And(t => t.F_Title.Contains(keyword) || t.F_SchemeName.Contains(keyword));
                }
                if (!string.IsNullOrEmpty(schemeCode)) {
                    expression = expression.And(t => t.F_SchemeCode.Equals(schemeCode));
                }
                expression = expression.And(t => t.F_IsChild == 0);
                return this.BaseRepository().FindList<NWFProcessEntity>(expression, pagination);
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
        /// 获取我的流程信息列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public IEnumerable<NWFProcessEntity> GetMyPageList(string userId, string queryJson, string schemeCode)
        {
            try
            {
                var expression = LinqExtensions.True<NWFProcessEntity>();
                var queryParam = queryJson.ToJObject();
                expression = expression.And(t => t.F_CreateUserId == userId);
                // 操作时间
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    DateTime startTime = queryParam["StartTime"].ToDate();
                    DateTime endTime = queryParam["EndTime"].ToDate();
                    expression = expression.And(t => t.F_CreateDate >= startTime && t.F_CreateDate <= endTime);
                }
                // 关键字
                if (!queryParam["keyword"].IsEmpty())
                {
                    string keyword = queryParam["keyword"].ToString();
                    expression = expression.And(t => t.F_Title.Contains(keyword) || t.F_SchemeName.Contains(keyword));
                }
                if (!string.IsNullOrEmpty(schemeCode))
                {
                    expression = expression.And(t => t.F_SchemeCode.Equals(schemeCode));
                }
                expression = expression.And(t => t.F_IsChild == 0);
                return this.BaseRepository().FindList<NWFProcessEntity>(expression);
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
        /// 获取我的代办任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public IEnumerable<NWFProcessEntity> GetMyTaskPageList(UserInfo userInfo, Pagination pagination, string queryJson,string schemeCode, bool isBatchAudit = false)
        {
            try
            {
                string userId = userInfo.userId;
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                t.F_Id AS F_TaskId,
	                                t.F_Type AS F_TaskType,
	                                t.F_NodeName AS F_TaskName,
                                    t.F_IsUrge,
                                    t.F_ModifyDate as F_CreateDate,
                                    p.F_Id,
                                    p.F_SchemeId,
                                    p.F_SchemeCode,
                                    p.F_SchemeName,
                                    p.F_Title,
                                    p.F_Level,
                                    p.F_EnabledMark,
                                    p.F_IsAgain,
                                    p.F_IsFinished,
                                    p.F_IsChild,
                                    p.F_ParentTaskId,
                                    p.F_ParentProcessId,
                                    p.F_CreateUserId,
                                    p.F_CreateUserName,
                                    p.F_IsStart
                                FROM
	                                (
		                                SELECT
			                                F_TaskId
		                                FROM
			                                LR_NWF_TaskRelation r1
                                        LEFT JOIN LR_NWF_Task t1 ON r1.F_TaskId = t1.F_Id 
                                        WHERE r1.F_Mark = 0 AND r1.F_Result = 0 AND (r1.F_UserId  = @userId 
		                               ");


                // 添加委托信息
                List<UserInfo> delegateList = GetDelegateProcess(userId);
                foreach (var item in delegateList)
                {
                    string processId = "'" + item.wfProcessId.Replace(",", "','") + "'";
                    string userI2 = "'" + item.userId + "'";

                    strSql.Append("  OR (r1.F_UserId =" + userI2 + " AND t1.F_ProcessId in (" + processId + ") AND t1.F_Type != 2 )");
                }
                strSql.Append(@") GROUP BY
			                                F_TaskId
	                                ) r
                                LEFT JOIN LR_NWF_Task t ON t.F_Id = r.F_TaskId
                                LEFT JOIN LR_NWF_Process p ON p.F_Id = t.F_ProcessId
                                WHERE
	                                t.F_IsFinished = 0  AND (p.F_IsFinished = 0 OR t.F_Type = 2 OR t.F_Type = 4 OR t.F_Type = 6)");

                var queryParam = queryJson.ToJObject();
                DateTime startTime = DateTime.Now, endTime = DateTime.Now;

                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    startTime = queryParam["StartTime"].ToDate();
                    endTime = queryParam["EndTime"].ToDate();
                    strSql.Append(" AND ( t.F_ModifyDate >= @startTime AND  t.F_ModifyDate <= @endTime ) ");
                }
                string keyword = "";
                if (!queryParam["keyword"].IsEmpty())
                {
                    keyword = "%" + queryParam["keyword"].ToString() + "%";
                    strSql.Append(" AND ( p.F_Title like @keyword OR  p.F_SchemeName like @keyword ) ");
                }

                if (!string.IsNullOrEmpty(schemeCode)) {
                    strSql.Append(" AND p.F_SchemeCode = @schemeCode ");
                }

                if (isBatchAudit)
                {
                    strSql.Append(" AND t.F_IsBatchAudit = 1 ");
                }

                return this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { userId, startTime, endTime, keyword, schemeCode }, pagination);
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
        /// 获取我的代办任务列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public IEnumerable<NWFProcessEntity> GetMyTaskPageList(UserInfo userInfo, string queryJson, string schemeCode, bool isBatchAudit = false)
        {
            try
            {
                string userId = userInfo.userId;
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                t.F_Id AS F_TaskId,
	                                t.F_Type AS F_TaskType,
	                                t.F_NodeName AS F_TaskName,
                                    t.F_IsUrge,
                                    t.F_ModifyDate as F_CreateDate,
                                    p.F_Id,
                                    p.F_SchemeId,
                                    p.F_SchemeCode,
                                    p.F_SchemeName,
                                    p.F_Title,
                                    p.F_Level,
                                    p.F_EnabledMark,
                                    p.F_IsAgain,
                                    p.F_IsFinished,
                                    p.F_IsChild,
                                    p.F_ParentTaskId,
                                    p.F_ParentProcessId,
                                    p.F_CreateUserId,
                                    p.F_CreateUserName,
                                    p.F_IsStart
                                FROM
	                                (
		                                SELECT
			                                F_TaskId
		                                FROM
			                                LR_NWF_TaskRelation r1
                                        LEFT JOIN LR_NWF_Task t1 ON r1.F_TaskId = t1.F_Id 
                                        WHERE r1.F_Mark = 0 AND r1.F_Result = 0 AND (r1.F_UserId  = @userId 
		                               ");


                // 添加委托信息
                List<UserInfo> delegateList = GetDelegateProcess(userId);
                foreach (var item in delegateList)
                {
                    string processId = "'" + item.wfProcessId.Replace(",", "','") + "'";
                    string userI2 = "'" + item.userId + "'";

                    strSql.Append("  OR (r1.F_UserId =" + userI2 + " AND t1.F_ProcessId in (" + processId + ") AND t1.F_Type != 2 )");
                }
                strSql.Append(@") GROUP BY
			                                F_TaskId
	                                ) r
                                LEFT JOIN LR_NWF_Task t ON t.F_Id = r.F_TaskId
                                LEFT JOIN LR_NWF_Process p ON p.F_Id = t.F_ProcessId
                                WHERE
	                                t.F_IsFinished = 0  AND (p.F_IsFinished = 0 OR t.F_Type = 2 OR t.F_Type = 4 OR t.F_Type = 6)");

                var queryParam = queryJson.ToJObject();
                DateTime startTime = DateTime.Now, endTime = DateTime.Now;

                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    startTime = queryParam["StartTime"].ToDate();
                    endTime = queryParam["EndTime"].ToDate();
                    strSql.Append(" AND ( t.F_ModifyDate >= @startTime AND  t.F_ModifyDate <= @endTime ) ");
                }
                string keyword = "";
                if (!queryParam["keyword"].IsEmpty())
                {
                    keyword = "%" + queryParam["keyword"].ToString() + "%";
                    strSql.Append(" AND ( p.F_Title like @keyword OR  p.F_SchemeName like @keyword ) ");
                }

                if (!string.IsNullOrEmpty(schemeCode))
                {
                    strSql.Append(" AND p.F_SchemeCode = @schemeCode ");
                }

                if (isBatchAudit) {
                    strSql.Append(" AND t.F_IsBatchAudit = 1 ");
                }

                return this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { userId, startTime, endTime, keyword, schemeCode });
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
        /// 获取我的已办任务列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="pagination">翻页信息</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public IEnumerable<NWFProcessEntity> GetMyFinishTaskPageList(UserInfo userInfo, Pagination pagination, string queryJson, string schemeCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                    t.F_TaskId,
	                    t.F_TaskType,
	                    t.F_TaskName,
	                    t.F_CreateDate,
	                    p.F_Title,
	                    p.F_Id,
	                    p.F_SchemeId,
	                    p.F_SchemeCode,
	                    p.F_SchemeName,
	                    p.F_Level,
	                    p.F_EnabledMark,
	                    p.F_IsAgain,
	                    p.F_IsFinished,
	                    p.F_IsChild,
	                    p.F_ParentTaskId,
	                    p.F_ParentProcessId,
	                    p.F_CreateUserId,
	                    p.F_CreateUserName,
	                    p.F_IsStart
                    FROM
	                    (
		                    SELECT
			                    MAX(t.F_Id) AS F_TaskId,
			                    MAX(t.F_Type) AS F_TaskType,
			                    MAX(t.F_NodeName) AS F_TaskName,
			                    MAX(r.F_Time) AS F_CreateDate,
			                    t.F_ProcessId
		                    FROM
			                    LR_NWF_Task t
		                    LEFT JOIN LR_NWF_TaskRelation r ON r.F_TaskId = t.F_Id
		                    WHERE
			                    (
				                    r.F_Result = 1
				                    OR r.F_Result = 2
				                    OR r.F_Result = 4
			                    )
		                    AND r.F_UserId = @userId
		                    GROUP BY
			                    t.F_NodeId,F_ProcessId
	                    ) t
                    LEFT JOIN LR_NWF_Process p ON t.F_ProcessId = p.F_Id where 1=1 
                ");


                var queryParam = queryJson.ToJObject();
                DateTime startTime = DateTime.Now, endTime = DateTime.Now;

                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    startTime = queryParam["StartTime"].ToDate();
                    endTime = queryParam["EndTime"].ToDate();
                    strSql.Append(" AND ( t.F_CreateDate >= @startTime AND  t.F_CreateDate <= @endTime ) ");
                }
                string keyword = "";
                if (!queryParam["keyword"].IsEmpty())
                {
                    keyword = "%" + queryParam["keyword"].ToString() + "%";
                    strSql.Append(" AND ( p.F_Title like @keyword OR  p.F_SchemeName like @keyword ) ");
                }
                if (!string.IsNullOrEmpty(schemeCode))
                {
                    strSql.Append(" AND p.F_SchemeCode = @schemeCode ");
                }

                return this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { userInfo.userId, startTime, endTime, keyword, schemeCode }, pagination);
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
        /// 获取我的已办任务列表
        /// </summary>
        /// <param name="userId">用户主键</param>
        /// <param name="queryJson">查询条件</param>
        /// <param name="schemeCode">流程模板编码</param>
        /// <returns></returns>
        public IEnumerable<NWFProcessEntity> GetMyFinishTaskPageList(UserInfo userInfo, string queryJson, string schemeCode)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                    t.F_TaskId,
	                    t.F_TaskType,
	                    t.F_TaskName,
	                    t.F_CreateDate,
	                    p.F_Title,
	                    p.F_Id,
	                    p.F_SchemeId,
	                    p.F_SchemeCode,
	                    p.F_SchemeName,
	                    p.F_Level,
	                    p.F_EnabledMark,
	                    p.F_IsAgain,
	                    p.F_IsFinished,
	                    p.F_IsChild,
	                    p.F_ParentTaskId,
	                    p.F_ParentProcessId,
	                    p.F_CreateUserId,
	                    p.F_CreateUserName,
	                    p.F_IsStart
                    FROM
	                    (
		                    SELECT
			                    MAX(t.F_Id) AS F_TaskId,
			                    MAX(t.F_Type) AS F_TaskType,
			                    MAX(t.F_NodeName) AS F_TaskName,
			                    MAX(r.F_Time) AS F_CreateDate,
			                    t.F_ProcessId
		                    FROM
			                    LR_NWF_Task t
		                    LEFT JOIN LR_NWF_TaskRelation r ON r.F_TaskId = t.F_Id
		                    WHERE
			                    (
				                    r.F_Result = 1
				                    OR r.F_Result = 2
				                    OR r.F_Result = 4
			                    )
		                    AND r.F_UserId = @userId
		                    GROUP BY
			                    t.F_NodeId,F_ProcessId
	                    ) t
                    LEFT JOIN LR_NWF_Process p ON t.F_ProcessId = p.F_Id where 1 = 1
                ");


                var queryParam = queryJson.ToJObject();
                DateTime startTime = DateTime.Now, endTime = DateTime.Now;

                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    startTime = queryParam["StartTime"].ToDate();
                    endTime = queryParam["EndTime"].ToDate();
                    strSql.Append(" AND ( t.F_CreateDate >= @startTime AND  t.F_CreateDate <= @endTime ) ");
                }
                string keyword = "";
                if (!queryParam["keyword"].IsEmpty())
                {
                    keyword = "%" + queryParam["keyword"].ToString() + "%";
                    strSql.Append(" AND ( p.F_Title like @keyword OR  p.F_SchemeName like @keyword ) ");
                }
                if (!string.IsNullOrEmpty(schemeCode))
                {
                    strSql.Append(" AND p.F_SchemeCode = @schemeCode ");
                }

                return this.BaseRepository().FindList<NWFProcessEntity>(strSql.ToString(), new { userInfo.userId, startTime, endTime, keyword, schemeCode });
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
        /// 获取委托人关联的流程进程列表
        /// </summary>
        /// <param name="userId">当前用户主键</param>
        /// <returns></returns>
        public List<UserInfo> GetDelegateProcess(string userId)
        {
            try
            {
                List<UserInfo> delegateUserlist = new List<UserInfo>();
                DateTime datatime = DateTime.Now;
                IEnumerable<NWFDelegateRuleEntity> wfDelegateRuleList = this.BaseRepository().FindList<NWFDelegateRuleEntity>(t => t.F_ToUserId == userId && t.F_BeginDate <= datatime && t.F_EndDate >= datatime);
                foreach (var item in wfDelegateRuleList)
                {
                    UserInfo userinfo = new UserInfo();
                    userinfo.userId = item.F_CreateUserId;

                    var strSql = new StringBuilder();
                    strSql.Append(@"SELECT
	                                    p.F_Id
                                    FROM
	                                    LR_NWF_DelegateRelation d
                                    LEFT JOIN LR_NWF_SchemeInfo s ON s.F_Id = d.F_SchemeInfoId
                                    LEFT JOIN LR_NWF_Process p ON p.F_SchemeCode = s.F_Code
                                    WHERE
	                                    p.F_Id IS NOT NULL
                                    AND p.F_IsFinished = 0
                                    AND d.F_DelegateRuleId = @DelegateRuleId ");

                    DataTable dt = this.BaseRepository().FindTable(strSql.ToString(), new { DelegateRuleId = item.F_Id });
                    userinfo.wfProcessId = "";
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr[0].ToString()))
                        {
                            if (!string.IsNullOrEmpty(userinfo.wfProcessId))
                            {
                                userinfo.wfProcessId += ",";
                            }
                            userinfo.wfProcessId += dr[0].ToString();
                        }
                    }

                    if (!string.IsNullOrEmpty(userinfo.wfProcessId))
                    {
                        delegateUserlist.Add(userinfo);
                    }
                }
                return delegateUserlist;
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


        #region 获取sql语句
        /// <summary>
        /// 获取我的流程信息列表SQL语句
        /// </summary>
        /// <returns></returns>
        public string GetMySql()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                p.F_CreateDate,
                                    p.F_Id,
                                    p.F_SchemeId,
                                    p.F_SchemeCode,
                                    p.F_SchemeName,
                                    p.F_Title,
                                    p.F_Level,
                                    p.F_EnabledMark,
                                    p.F_IsAgain,
                                    p.F_IsFinished,
                                    p.F_IsChild,
                                    p.F_ParentTaskId,
                                    p.F_ParentProcessId,
                                    p.F_CreateUserId,
                                    p.F_CreateUserName,
                                    p.F_IsStart
                                FROM
	                                LR_NWF_Process p
                                WHERE
	                                p.F_CreateUserId = @userId AND p.F_IsChild = 0
                ");

                return strSql.ToString();
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
        /// 获取我的代办任务列表SQL语句
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <param name="isBatchAudit">true获取批量审核任务</param>
        /// <returns></returns>
        public string GetMyTaskSql(UserInfo userInfo, bool isBatchAudit = false)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                                t.F_Id AS F_TaskId,
	                                t.F_Type AS F_TaskType,
	                                t.F_NodeName AS F_TaskName,
                                    t.F_IsUrge,
                                    t.F_ModifyDate as F_CreateDate,
                                    p.F_Id,
                                    p.F_SchemeId,
                                    p.F_SchemeCode,
                                    p.F_SchemeName,
                                    p.F_Title,
                                    p.F_Level,
                                    p.F_EnabledMark,
                                    p.F_IsAgain,
                                    p.F_IsFinished,
                                    p.F_IsChild,
                                    p.F_ParentTaskId,
                                    p.F_ParentProcessId,
                                    p.F_CreateUserId,
                                    p.F_CreateUserName,
                                    p.F_IsStart
                                FROM
	                                (
		                                SELECT
			                                F_TaskId
		                                FROM
			                                LR_NWF_TaskRelation r1
                                        LEFT JOIN LR_NWF_Task t1 ON r1.F_TaskId = t1.F_Id 
                                        WHERE r1.F_Mark = 0 AND r1.F_Result = 0 AND (r1.F_UserId  = @userId 
		                               ");


                // 添加委托信息
                List<UserInfo> delegateList = GetDelegateProcess(userInfo.userId);
                foreach (var item in delegateList)
                {
                    string processId = "'" + item.wfProcessId.Replace(",", "','") + "'";
                    string userI2 = "'" + item.userId + "'";

                    strSql.Append("  OR (r1.F_UserId =" + userI2 + " AND t1.F_ProcessId in (" + processId + ") AND t1.F_Type != 2 )");
                }
                strSql.Append(@") GROUP BY
			                                F_TaskId
	                                ) r
                                LEFT JOIN LR_NWF_Task t ON t.F_Id = r.F_TaskId
                                LEFT JOIN LR_NWF_Process p ON p.F_Id = t.F_ProcessId
                                WHERE
	                                t.F_IsFinished = 0  AND (p.F_IsFinished = 0 OR t.F_Type = 2)");

                if (isBatchAudit)
                {
                    strSql.Append(" AND t.F_IsBatchAudit = 1 ");
                }

                return strSql.ToString();

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
        /// 获取我的已办任务列表SQL语句
        /// </summary>
        /// <returns></returns>
        public string GetMyFinishTaskSql()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"SELECT
	                    t.F_TaskId,
	                    t.F_TaskType,
	                    t.F_TaskName,
	                    t.F_CreateDate,
	                    p.F_Title,
	                    p.F_Id,
	                    p.F_SchemeId,
	                    p.F_SchemeCode,
	                    p.F_SchemeName,
	                    p.F_Level,
	                    p.F_EnabledMark,
	                    p.F_IsAgain,
	                    p.F_IsFinished,
	                    p.F_IsChild,
	                    p.F_ParentTaskId,
	                    p.F_ParentProcessId,
	                    p.F_CreateUserId,
	                    p.F_CreateUserName,
	                    p.F_IsStart
                    FROM
	                    (
		                    SELECT
			                    MAX(t.F_Id) AS F_TaskId,
			                    MAX(t.F_Type) AS F_TaskType,
			                    MAX(t.F_NodeName) AS F_TaskName,
			                    MAX(r.F_Time) AS F_CreateDate,
			                    t.F_ProcessId
		                    FROM
			                    LR_NWF_Task t
		                    LEFT JOIN LR_NWF_TaskRelation r ON r.F_TaskId = t.F_Id
		                    WHERE
			                    (
				                    r.F_Result = 1
				                    OR r.F_Result = 2
				                    OR r.F_Result = 4
			                    )
		                    AND r.F_UserId = @userId
		                    GROUP BY
			                    t.F_NodeId,F_ProcessId
	                    ) t
                    LEFT JOIN LR_NWF_Process p ON t.F_ProcessId = p.F_Id
                ");

                return strSql.ToString();
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

        #endregion

        #region 保存信息
        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFProcessEntity">流程进程</param>
        /// <param name="taskList">流程任务列表</param>
        /// <param name="taskMsgList">流程消息列表</param>
        /// <param name="taskLogEntity">任务日志</param>
        public void Save(NWFProcessEntity nWFProcessEntity,List<NWFTaskEntity> taskList,List<NWFTaskMsgEntity> taskMsgList, NWFTaskLogEntity taskLogEntity) {
            NWFProcessEntity nWFProcessEntityTmp = this.BaseRepository().FindEntity<NWFProcessEntity>(nWFProcessEntity.F_Id);
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (nWFProcessEntityTmp == null)
                {
                    db.Insert(nWFProcessEntity);
                }
                else {
                    db.Update(nWFProcessEntity);
                }
                foreach (var task in taskList) {
                    task.F_ModifyDate = DateTime.Now;
                    db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null) {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.Create();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }
                foreach (var taskMsg in taskMsgList)
                {
                    db.Insert(taskMsg);
                }

                db.Insert(taskLogEntity);

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
        /// <summary>
        /// 保存流程进程信息
        /// </summary>
        /// <param name="taskLogEntity">任务日志</param>
        /// <param name="taskRelationEntity">任务执行人状态更新</param>
        /// <param name="taskEntityUpdate">任务状态更新</param>
        /// <param name="processEntity">流程进程状态更新</param>
        /// <param name="confluenceList">会签信息</param>
        /// <param name="closeTaskList">会签需要关闭的任务</param>
        /// <param name="taskList">新的任务列表</param>
        /// <param name="taskMsgList">新的任务消息列表</param>
        public void Save(NWFTaskLogEntity taskLogEntity, NWFTaskRelationEntity taskRelationEntity, NWFTaskEntity taskEntityUpdate, NWFProcessEntity processEntity, List<NWFConfluenceEntity> confluenceList, List<NWFTaskEntity> closeTaskList, List<NWFTaskEntity> taskList, List<NWFTaskMsgEntity> taskMsgList, NWFProcessEntity pProcessEntity = null)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (taskLogEntity.F_Des == "无审核人跳过" || taskLogEntity.F_Des == "系统自动审核") {
                    taskLogEntity.F_CreateDate = taskLogEntity.F_CreateDate.Value.AddMilliseconds(10);
                }

                db.Insert(taskLogEntity);
                if (taskRelationEntity != null)
                {
                    db.Update(taskRelationEntity);
                }
                db.Update(taskEntityUpdate);

                if (processEntity != null)
                {
                    db.Update(processEntity);
                }

                if (pProcessEntity != null)
                {
                    db.Update(pProcessEntity);
                }

                if (confluenceList != null) {
                    foreach (var item in confluenceList)
                    {
                        if (item.isClear)
                        {
                            string processId = item.F_ProcessId;
                            string nodeId = item.F_NodeId;
                            db.Delete<NWFConfluenceEntity>(t => t.F_ProcessId == processId && t.F_NodeId == nodeId);
                            // 增加一条会签审核记录
                            NWFTaskLogEntity nWFTaskLogEntity = new NWFTaskLogEntity()
                            {
                                F_ProcessId = processId,
                                F_OperationCode = "confluence",
                                F_OperationName = "会签" +(item.confluenceRes == 1?"通过":"不通过"),
                                F_NodeId = item.F_NodeId,
                                F_TaskType = 7
                            };
                            nWFTaskLogEntity.Create();
                            db.Insert(nWFTaskLogEntity);
                        }
                        else
                        {
                            db.Insert(item);
                        }
                    }
                }

                if (closeTaskList != null) {
                    foreach (var item in closeTaskList) {
                        db.Update(item);
                    }
                }

                foreach (var task in taskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null) {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.Create();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }
                foreach (var taskMsg in taskMsgList)
                {
                    db.Insert(taskMsg);
                }

                

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
        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFProcessEntity">流程进程</param>
        public void Save(NWFProcessEntity nWFProcessEntity)
        {
            try
            {
                this.BaseRepository().Insert(nWFProcessEntity);
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
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFTaskLogEntity">任务日志数据</param>
        /// <param name="taskUserUpdateList">任务执行人需要更新状态数据</param>
        /// <param name="nWFTaskMsgEntity">任务消息</param>
        public void Save(NWFTaskLogEntity nWFTaskLogEntity, List<NWFTaskRelationEntity> taskUserUpdateList, NWFTaskMsgEntity nWFTaskMsgEntity) {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Insert(nWFTaskLogEntity);

                foreach (var item in taskUserUpdateList) {
                    db.Update(item);
                }
                db.Insert(nWFTaskMsgEntity);
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


        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFTaskLogEntity">任务日志数据</param>
        /// <param name="nWFTaskRelationEntity">任务执行人需要更新状态数据</param>
        /// <param name="taskEntity">任务</param>
        public void Save(NWFTaskLogEntity nWFTaskLogEntity, NWFTaskRelationEntity nWFTaskRelationEntity, NWFTaskEntity taskEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Insert(nWFTaskLogEntity);
                db.Update(nWFTaskRelationEntity);
                if (taskEntity != null) {
                    taskEntity.F_ModifyDate = DateTime.Now;
                    db.Update(taskEntity);
                }
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

        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFTaskLogEntity">任务日志数据</param>
        /// <param name="taskList">需要更新的任务列表</param>
        /// <param name="taskMsgList">任务消息列表</param>
        public void Save(NWFTaskLogEntity nWFTaskLogEntity, List<NWFTaskEntity>taskList, List<NWFTaskMsgEntity> taskMsgList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Insert(nWFTaskLogEntity);
                foreach (var item in taskList) {
                    item.F_ModifyDate = DateTime.Now;
                    db.Update(item);
                }
                foreach (var item in taskMsgList)
                {
                    db.Insert(item);
                }
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
        /// <summary>
        /// 保存流程进程数据
        /// </summary>
        /// <param name="nWFTaskLogEntity">任务日志数据</param>
        /// <param name="taskList">需要更新的任务列表</param>
        /// <param name="taskMsgList">任务消息列表</param>
        public void Save(NWFTaskLogEntity nWFTaskLogEntity, NWFTaskEntity task, List<NWFTaskMsgEntity> taskMsgList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Insert(nWFTaskLogEntity);
                task.F_ModifyDate = DateTime.Now;
                db.Update(task);
                foreach (var item in taskMsgList)
                {
                    db.Insert(item);
                }
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

        /// <summary>
        /// 保存流程进程信息
        /// </summary>
        /// <param name="taskLogEntity">任务日志</param>
        /// <param name="taskRelationEntity">任务执行人状态更新</param>
        /// <param name="taskEntityUpdate">任务状态更新</param>
        /// <param name="processEntity">流程进程状态更新</param>
        /// <param name="taskList">新的任务列表</param>
        /// <param name="taskMsgList">新的任务消息列表</param>
        public void Save(NWFTaskLogEntity pTaskLogEntity, NWFTaskRelationEntity pTaskRelationEntity, NWFTaskEntity pTaskEntityUpdate, NWFProcessEntity pProcessEntity, List<NWFTaskEntity> pTaskList, List<NWFTaskMsgEntity> pTaskMsgList, NWFProcessEntity nWFProcessEntity, List<NWFTaskEntity> taskList, List<NWFTaskMsgEntity> taskMsgList, NWFTaskLogEntity taskLogEntity)
        {
            NWFProcessEntity nWFProcessEntityTmp = this.BaseRepository().FindEntity<NWFProcessEntity>(nWFProcessEntity.F_Id);
            IEnumerable<NWFTaskEntity> uTaskList = this.BaseRepository().FindList<NWFTaskEntity>(t=>t.F_ProcessId == nWFProcessEntity.F_Id && t.F_NodeId == taskLogEntity.F_NodeId && t.F_IsFinished == 0);
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (nWFProcessEntityTmp == null)
                {
                    db.Insert(nWFProcessEntity);
                }
                else
                {
                    db.Update(nWFProcessEntity);
                }
                foreach (var task in taskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null) {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.Create();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }
                foreach (var taskMsg in taskMsgList)
                {
                    db.Insert(taskMsg);
                }

                db.Insert(taskLogEntity);
                foreach (var item in uTaskList) {
                    item.F_IsFinished = 1;
                    db.Update(item);
                }
                

                // 父流程
                db.Insert(pTaskLogEntity);
                db.Update(pTaskRelationEntity);
                db.Update(pTaskEntityUpdate);
                if (pProcessEntity != null)
                {
                    db.Update(pProcessEntity);
                }

                foreach (var task in pTaskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null) {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.Create();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }
                foreach (var taskMsg in pTaskMsgList)
                {
                    db.Insert(taskMsg);
                }

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
        /// <summary>
        /// （流程撤销）
        /// </summary>
        /// <param name="processId">流程进程实例</param>
        /// <param name="taskList">流程任务列表</param>
        /// <param name="EnabledMark">2草稿3作废</param>
        public void Save(string processId, IEnumerable<NWFTaskEntity> taskList, int EnabledMark, NWFTaskLogEntity nWFTaskLogEntity = null)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                NWFProcessEntity nWFProcessEntity = new NWFProcessEntity();
                nWFProcessEntity.F_Id = processId;
                nWFProcessEntity.F_EnabledMark = EnabledMark;
                db.Update(nWFProcessEntity);
                if (EnabledMark == 2)
                {
                    db.Delete<NWFTaskLogEntity>(t => t.F_ProcessId == processId);
                }
                foreach (var task in taskList)
                {
                    db.Delete(task);
                    string taskId = task.F_Id;
                    db.Delete<NWFTaskMsgEntity>(t => t.F_TaskId == taskId);
                    db.Delete<NWFTaskRelationEntity>(t => t.F_TaskId == taskId);
                }
                if (nWFTaskLogEntity != null) {
                    db.Insert(nWFTaskLogEntity);
                }
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

        /// <summary>
        /// 删除流程进程实体
        /// </summary>
        /// <param name="processId">流程进程主键</param>
        public void DeleteEntity(string processId) {
            try
            {
                this.BaseRepository().Delete<NWFProcessEntity>(t=>t.F_Id == processId);
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
        /// 删除流程进程所有信息（流程撤销）
        /// </summary>
        /// <param name="processId">流程进程实例</param>
        /// <param name="taskList">流程任务列表</param>
        public void Delete(string processId, IEnumerable<NWFTaskEntity> taskList) {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<NWFProcessEntity>(t=>t.F_Id == processId);
                db.Delete<NWFTaskLogEntity>(t=>t.F_ProcessId == processId);
                foreach (var task in taskList) {
                    db.Delete(task);
                    string taskId = task.F_Id;
                    db.Delete<NWFTaskMsgEntity>(t=>t.F_TaskId == taskId);
                    db.Delete<NWFTaskRelationEntity>(t=>t.F_TaskId == taskId);
                }

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


        /// <summary>
        /// 撤销审核
        /// </summary>
        /// <param name="taskList">需要撤回的任务节点</param>
        /// <param name="taskUser">当前处理人</param>
        /// <param name="taskEntity">当前任务</param>
        /// <param name="taskLogEntity">日志信息</param>
       /// <param name="taskUserNew">当前任务节点的处理人（串行多人审核）</param>
        public void RevokeAudit(List<string> taskList, NWFTaskRelationEntity taskUser,NWFTaskEntity taskEntity,NWFTaskLogEntity taskLogEntity, NWFTaskRelationEntity taskUserNew = null)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (taskList != null) {
                    foreach (var taskId in taskList)
                    {
                        db.Delete<NWFTaskEntity>(t => t.F_Id == taskId);
                        db.Delete<NWFTaskRelationEntity>(t => t.F_TaskId == taskId);
                        db.Delete<NWFTaskMsgEntity>(t => t.F_TaskId == taskId);
                    }

                }

                if (taskEntity != null) {
                    db.Update(taskEntity);
                }

                taskUser.F_Mark = 0;
                taskUser.F_Result = 0;
                db.Update(taskUser);

                db.Insert(taskLogEntity);

                if (taskUserNew != null) {
                    taskUserNew.F_Mark = 1;
                    taskUserNew.F_Result = 0;
                    db.Update(taskUserNew);
                }


                // 更新下流程实例（处理重新发起状态）
                NWFProcessEntity nWFProcessEntity = new NWFProcessEntity();
                nWFProcessEntity.F_Id = taskLogEntity.F_ProcessId;
                nWFProcessEntity.F_IsAgain = 0;
                db.Update(nWFProcessEntity);

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

        /// <summary>
        /// 保存任务
        /// </summary>
        /// <param name="taskList">任务列表</param>
        public void SaveTask(List<NWFTaskEntity> taskList) {
            var db = this.BaseRepository().BeginTrans();

            try
            {
                foreach (var task in taskList)
                {
                    task.F_ModifyDate = DateTime.Now;
                    db.Insert(task);
                    int num = 1;
                    if (task.nWFUserInfoList != null)
                    {
                        foreach (var taskUser in task.nWFUserInfoList)
                        {
                            NWFTaskRelationEntity nWFTaskRelationEntity = new NWFTaskRelationEntity();
                            nWFTaskRelationEntity.Create();
                            nWFTaskRelationEntity.F_TaskId = task.F_Id;
                            nWFTaskRelationEntity.F_UserId = taskUser.Id;
                            nWFTaskRelationEntity.F_Mark = taskUser.Mark;
                            nWFTaskRelationEntity.F_Result = 0;
                            nWFTaskRelationEntity.F_Sort = num;
                            db.Insert(nWFTaskRelationEntity);
                            num++;
                        }
                    }
                }

                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }

            
        }
        #endregion
    }
}
