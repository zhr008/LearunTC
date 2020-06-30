using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-06-10 17:21
    /// 描 述：工单管理
    /// </summary>
    public class WorkOrderService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_Demo_WorkOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_Code,
                t.F_DepartmentId,
                t.F_ManagerId,
                t.F_Qty,
                t.F_Process,
                t.F_Spec,
                t.F_Status
                ");
                strSql.Append("  FROM LR_Demo_WorkOrder t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_Code"].IsEmpty())
                {
                    dp.Add("F_Code", "%" + queryParam["F_Code"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Code Like @F_Code ");
                }
                if (!queryParam["F_DepartmentId"].IsEmpty())
                {
                    dp.Add("F_DepartmentId", queryParam["F_DepartmentId"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_DepartmentId = @F_DepartmentId ");
                }
                if (!queryParam["F_ManagerId"].IsEmpty())
                {
                    dp.Add("F_ManagerId", "%" + queryParam["F_ManagerId"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_ManagerId Like @F_ManagerId ");
                }
                if (!queryParam["F_Process"].IsEmpty())
                {
                    dp.Add("F_Process", queryParam["F_Process"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Process = @F_Process ");
                }
                if (!queryParam["F_DepartmentId"].IsEmpty())
                {
                    dp.Add("F_DepartmentId", queryParam["F_DepartmentId"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_DepartmentId = @F_DepartmentId ");
                }
                return this.BaseRepository().FindList<LR_Demo_WorkOrderEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取屏幕显示数据
        /// <summary>
        /// <returns></returns>
        public DataTable GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                               SUM(CASE WHEN F_Process='01' AND F_Status='1' THEN F_Qty ELSE 0 END) as '1-1' ,
			                   SUM(CASE WHEN F_Process='01' AND F_Status='2' THEN F_Qty ELSE 0 END) as '1-2' ,
                               SUM(CASE WHEN F_Process='02' AND F_Status='1' THEN F_Qty ELSE 0 END) as '2-1' ,
			                   SUM(CASE WHEN F_Process='02' AND F_Status='2' THEN F_Qty ELSE 0 END) as '2-2' ,
                               SUM(CASE WHEN F_Process='03' AND F_Status='1' THEN F_Qty ELSE 0 END) as '3-1' ,
			                   SUM(CASE WHEN F_Process='03' AND F_Status='2' THEN F_Qty ELSE 0 END) as '3-2' ,
                               SUM(CASE WHEN F_Process='04' AND F_Status='1' THEN F_Qty ELSE 0 END) as '4-1' ,
			                   SUM(CASE WHEN F_Process='04' AND F_Status='2' THEN F_Qty ELSE 0 END) as '4-2' 
                ");
                strSql.Append("  FROM LR_Demo_WorkOrder");
                strSql.Append("  WHERE 1=1 ");
                return this.BaseRepository().FindTable(strSql.ToString());
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
        /// 获取套打数据
        /// <summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public DataTable GetPrintItem(string keyValue)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                                   F_Code as '工单单号'
                                  ,F_Code as '研发单号'
                                  ,'A0980' as '生产料号'
                                  ,'宝骏乘用车' as '品名'
                                  ,F_Spec as '规格'
                                  ,'A4仓' as '物料情况'
                                  ,'标准样品' as '样品'
                                  ,F_Spec as '样品品名'
                                  ,'2019-07-30' as '单据日期'
                                  ,'2019-05-15' as '订单日期'
                                  ,'2019-09-28' as '交货日期'
                                  ,F_ManagerId as '生管人员'
                                  ,'杨伟' as '业务员'
                                  ,'国六标准' as '机型'
                ");
                strSql.Append("  FROM LR_Demo_WorkOrder ");
                strSql.Append("  WHERE 1=1 ");
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrEmpty(keyValue))
                {
                    dp.Add("F_Id", keyValue, DbType.String);
                    strSql.Append(" AND F_Id = @F_Id ");
                }
                return this.BaseRepository().FindTable(strSql.ToString(),dp);
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
        /// 获取LR_Demo_WorkOrder表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_Demo_WorkOrderEntity GetLR_Demo_WorkOrderEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_Demo_WorkOrderEntity>(keyValue);
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
        /// 获取主表实体数据
        /// <param name="processId">流程实例ID</param>
        /// <summary>
        /// <returns></returns>
        public LR_Demo_WorkOrderEntity GetEntityByProcessId(string processId)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_Demo_WorkOrderEntity>(t => t.F_Id == processId);
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
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetSqlTree()
        {
            try
            {
                return this.BaseRepository().FindTable(" select * from lr_base_department ");
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
                this.BaseRepository().Delete<LR_Demo_WorkOrderEntity>(t => t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, LR_Demo_WorkOrderEntity entity)
        {
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
                else
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
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

    }
}
