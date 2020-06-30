using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 16:07
    /// 描 述：任务进程
    /// </summary>
    public class TSProcessService : RepositoryFactory 
    {
        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// <summary> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public IEnumerable<TSProcessEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                SELECT
	                t.F_Id,
	                t.F_SchemeInfoId,
	                t.F_SchemeId,
	                t.F_BeginTime,
	                t.F_EndType,
	                t.F_EndTime,
	                t.F_State,
	                t.F_CreateDate,
	                s.F_Name
                FROM
	                LR_TS_Process t
                LEFT JOIN 
	                LR_TS_SchemeInfo s ON s.F_Id = t.F_SchemeInfoId
                ");
                strSql.Append("  WHERE 1=1 ");
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
                    strSql.Append(" AND ( s.F_Name like @keyword ) ");
                }
                return this.BaseRepository().FindList<TSProcessEntity>(strSql.ToString(), new { startTime, endTime, keyword }, pagination);
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

        public IEnumerable<TSProcessEntity> GetList() {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"
                SELECT
	                t.F_Id,
	                t.F_SchemeInfoId,
	                t.F_SchemeId,
	                t.F_BeginTime,
	                t.F_EndType,
	                t.F_EndTime,
	                t.F_State,
	                t.F_CreateDate
                FROM
	                LR_TS_Process t
                ");
                strSql.Append("  WHERE 1=1 ");
                DateTime nowTime = DateTime.Now;
                strSql.Append(" AND ( t.F_EndTime >= @nowTime  OR t.F_EndType = 1 ) AND (t.F_State = 1 OR  t.F_State = 2)");

                return this.BaseRepository().FindList<TSProcessEntity>(strSql.ToString(), new { nowTime });
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
        /// 获取LR_TS_Process表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public TSProcessEntity GetProcessEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<TSProcessEntity>(keyValue);
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

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<TSProcessEntity>(t => t.F_Id == keyValue);
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
        /// 保存实体数据（新增、修改） 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public void SaveEntity(string keyValue, TSProcessEntity entity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    db.Update(entity);
                }
                else
                {
                    var entityTmp = db.FindEntity<TSProcessEntity>("SELECT * FROM LR_TS_Process Where F_SchemeInfoId = @schemeId AND  F_State != 10 ", new { schemeId = entity.F_SchemeInfoId });
                    if (entityTmp == null) {
                        db.ExecuteBySql("delete FROM LR_TS_Process Where F_SchemeInfoId = @schemeId AND  F_State != 10 ", new { schemeId = entity.F_SchemeInfoId });
                    }

                    entity.Create();
                    db.Insert(entity);
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

        #endregion
    }
}
