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
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-29 15:28
    /// 描 述：销售订单
    /// </summary>
    public class SoorderService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表分页数据
        /// <summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<AllotAssetsEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_Title,
                t.F_Degree,
                t.F_Dept
                ");
                strSql.Append("  FROM AllotAssets t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_Title"].IsEmpty())
                {
                    dp.Add("F_Title", "%" + queryParam["F_Title"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Title Like @F_Title ");
                }
                if (!queryParam["F_Degree"].IsEmpty())
                {
                    dp.Add("F_Degree", "%" + queryParam["F_Degree"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Degree Like @F_Degree ");
                }
                if (!queryParam["F_Dept"].IsEmpty())
                {
                    dp.Add("F_Dept",queryParam["F_Dept"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Dept = @F_Dept ");
                }
                return this.BaseRepository("WFDB").FindList<AllotAssetsEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<AllotAssetsEntity> GetList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_Title,
                t.F_Degree,
                t.F_Dept
                ");
                strSql.Append("  FROM AllotAssets t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_Title"].IsEmpty())
                {
                    dp.Add("F_Title", "%" + queryParam["F_Title"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Title Like @F_Title ");
                }
                if (!queryParam["F_Degree"].IsEmpty())
                {
                    dp.Add("F_Degree", "%" + queryParam["F_Degree"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Degree Like @F_Degree ");
                }
                if (!queryParam["F_Dept"].IsEmpty())
                {
                    dp.Add("F_Dept",queryParam["F_Dept"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Dept = @F_Dept ");
                }
                return this.BaseRepository("WFDB").FindList<AllotAssetsEntity>(strSql.ToString(),dp);
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
        /// 获取ApplyAssets表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<ApplyAssetsEntity> GetApplyAssetsList(string keyValue)
        {
            try
            {
                return this.BaseRepository("WFDB").FindList<ApplyAssetsEntity>(t=>t.F_Id == keyValue );
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
        /// 获取AllotAssets表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public AllotAssetsEntity GetAllotAssetsEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("WFDB").FindEntity<AllotAssetsEntity>(keyValue);
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
        /// 获取ApplyAssets表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public ApplyAssetsEntity GetApplyAssetsEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository("WFDB").FindEntity<ApplyAssetsEntity>(t=>t.F_Id == keyValue);
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
            var db = this.BaseRepository("WFDB").BeginTrans();
            try
            {
                var allotAssetsEntity = GetAllotAssetsEntity(keyValue); 
                db.Delete<AllotAssetsEntity>(t=>t.F_Id == keyValue);
                db.Delete<ApplyAssetsEntity>(t=>t.F_Id == allotAssetsEntity.F_Id);
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
        public void SaveEntity( UserInfo userInfo, string keyValue, AllotAssetsEntity entity,List<ApplyAssetsEntity> applyAssetsList)
        {
            var db = this.BaseRepository("WFDB").BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var allotAssetsEntityTmp = GetAllotAssetsEntity(keyValue); 
                    entity.Modify(keyValue,userInfo);
                    db.Update(entity);
                    db.Delete<ApplyAssetsEntity>(t=>t.F_Id == allotAssetsEntityTmp.F_Id);
                    foreach (ApplyAssetsEntity item in applyAssetsList)
                    {
                        item.Create(userInfo);
                        item.F_Id = allotAssetsEntityTmp.F_Id;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create(userInfo);
                    db.Insert(entity);
                    foreach (ApplyAssetsEntity item in applyAssetsList)
                    {
                        item.Create(userInfo);
                        item.F_Id = entity.F_Id;
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
