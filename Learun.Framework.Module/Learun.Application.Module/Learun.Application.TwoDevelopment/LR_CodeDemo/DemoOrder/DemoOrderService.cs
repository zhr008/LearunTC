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
    /// 日 期：2019-06-04 10:38
    /// 描 述：订单信息
    /// </summary>
    public class DemoOrderService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_Demo_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_OrderId,
                t.F_OrderCode,
                t.F_CustomerId,
                t.F_SellerId,
                t.F_DiscountSum,
                t.F_PaymentDate,
                t.F_PaymentMode
                ");
                strSql.Append("  FROM LR_Demo_Order t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.F_OrderDate >= @startTime AND t.F_OrderDate <= @endTime ) ");
                }
                if (!queryParam["F_OrderCode"].IsEmpty())
                {
                    dp.Add("F_OrderCode", "%" + queryParam["F_OrderCode"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_OrderCode Like @F_OrderCode ");
                }
                if (!queryParam["F_CustomerId"].IsEmpty())
                {
                    dp.Add("F_CustomerId",queryParam["F_CustomerId"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CustomerId = @F_CustomerId ");
                }
                if (!queryParam["F_SellerId"].IsEmpty())
                {
                    dp.Add("F_SellerId",queryParam["F_SellerId"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_SellerId = @F_SellerId ");
                }
                if (!queryParam["F_PaymentMode"].IsEmpty())
                {
                    dp.Add("F_PaymentMode",queryParam["F_PaymentMode"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PaymentMode = @F_PaymentMode ");
                }
        
                return this.BaseRepository().FindList<LR_Demo_OrderEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取LR_Demo_OrderDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_Demo_OrderDetailEntity> GetLR_Demo_OrderDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<LR_Demo_OrderDetailEntity>(t=>t.F_OrderId == keyValue );
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
        /// 获取LR_Demo_Order表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_Demo_OrderEntity GetLR_Demo_OrderEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_Demo_OrderEntity>(keyValue);
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
        /// 获取LR_Demo_OrderDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_Demo_OrderDetailEntity GetLR_Demo_OrderDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_Demo_OrderDetailEntity>(t=>t.F_OrderId == keyValue);
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
        public LR_Demo_OrderEntity GetEntityByProcessId(string processId)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_Demo_OrderEntity>(t=>t.F_OrderId == processId);
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
            var db = this.BaseRepository().BeginTrans();
            try
            {
                var lR_Demo_OrderEntity = GetLR_Demo_OrderEntity(keyValue); 
                db.Delete<LR_Demo_OrderEntity>(t=>t.F_OrderId == keyValue);
                db.Delete<LR_Demo_OrderDetailEntity>(t=>t.F_OrderId == lR_Demo_OrderEntity.F_OrderId);
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
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, LR_Demo_OrderEntity entity,List<LR_Demo_OrderDetailEntity> lR_Demo_OrderDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var lR_Demo_OrderEntityTmp = GetLR_Demo_OrderEntity(keyValue); 
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<LR_Demo_OrderDetailEntity>(t=>t.F_OrderId == lR_Demo_OrderEntityTmp.F_OrderId);
                    foreach (LR_Demo_OrderDetailEntity item in lR_Demo_OrderDetailList)
                    {
                        item.Create();
                        item.F_OrderId = lR_Demo_OrderEntityTmp.F_OrderId;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (LR_Demo_OrderDetailEntity item in lR_Demo_OrderDetailList)
                    {
                        item.Create();
                        item.F_OrderId = entity.F_OrderId;
                        db.Insert(item);
                    }
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
