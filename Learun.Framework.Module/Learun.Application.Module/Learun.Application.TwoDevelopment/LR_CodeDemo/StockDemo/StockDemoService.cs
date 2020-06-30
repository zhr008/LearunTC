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
    /// 日 期：2019-06-12 18:49
    /// 描 述：库存
    /// </summary>
    public class StockDemoService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_Demo_StockEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_ItemName,
                t.F_Qty,
                t.F_Unit,
                t.F_Area
                ");
                strSql.Append("  FROM LR_Demo_Stock t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_ItemName"].IsEmpty())
                {
                    dp.Add("F_ItemName", "%" + queryParam["F_ItemName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_ItemName Like @F_ItemName ");
                }
                if (!queryParam["F_Unit"].IsEmpty())
                {
                    dp.Add("F_Unit", "%" + queryParam["F_Unit"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Unit Like @F_Unit ");
                }
                if (!queryParam["F_Area"].IsEmpty())
                {
                    dp.Add("F_Area",queryParam["F_Area"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Area = @F_Area ");
                }
                return this.BaseRepository().FindList<LR_Demo_StockEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取指定库位列表数据
        /// <summary>
        /// <param name="stockArea">库位ID</param>
        /// <returns></returns>
        public IEnumerable<LR_Demo_StockEntity> GetStock(string stockArea)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_ItemName,
                t.F_Qty,
                t.F_Unit,
                t.F_Area
                ");
                strSql.Append("  FROM LR_Demo_Stock t ");
                strSql.Append("  WHERE 1=1 ");
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrEmpty(stockArea))
                {
                    dp.Add("F_Area", stockArea, DbType.String);
                    strSql.Append(" AND t.F_Area = @F_Area ");
                }
                return this.BaseRepository().FindList<LR_Demo_StockEntity>(strSql.ToString(), dp);
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
        /// 获取LR_Demo_Stock表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_Demo_StockEntity GetLR_Demo_StockEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_Demo_StockEntity>(keyValue);
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
                this.BaseRepository().Delete<LR_Demo_StockEntity>(t=>t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, LR_Demo_StockEntity entity)
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
