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
    /// 创 建：超级管理员
    /// 日 期：2020-07-08 22:22
    /// 描 述：合同结算详情
    /// </summary>
    public class SettlementsDetailsService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<SettlementsDetailsInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_SettlementDetailsId,
                t.F_BatchNumber,
                t.F_PayAmount,
                t.F_PayStatus,
                t.F_PayCondition,
                p.F_UserName,
                p.F_IDCardNo,
                t.F_PayDate,
                p.F_ApplicantId,
                t.F_PersonId
                ");
                strSql.Append("  FROM tc_SettlementsDetails t ");
                strSql.Append("  LEFT JOIN tc_Personnels p ON t.F_PersonId =p.F_PersonId ");
                strSql.Append("  LEFT JOIN tc_Settlements s ON s.F_SettlementsId =t.F_SettlementsId ");
                strSql.Append("  WHERE 1=1 ");
                strSql.Append("  AND t.F_DeleteMark=0 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_IDCardNo"].IsEmpty())
                {
                    dp.Add("F_IDCardNo", "%" + queryParam["F_IDCardNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND p.F_IDCardNo Like @F_IDCardNo ");
                }
                if (!queryParam["F_UserName"].IsEmpty())
                {
                    dp.Add("F_UserName", "%" + queryParam["F_UserName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND p.F_UserName Like @F_UserName ");
                }
                if (!queryParam["F_ApplicantId"].IsEmpty())
                {
                    dp.Add("F_ApplicantId", queryParam["F_ApplicantId"].ToString(), DbType.String);
                    strSql.Append(" AND p.F_ApplicantId = @F_ApplicantId ");
                }
                if (!queryParam["F_PersonId"].IsEmpty())
                {
                    dp.Add("F_PersonId", queryParam["F_PersonId"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PersonId = @F_PersonId ");
                }
                if (!queryParam["F_BatchNumber"].IsEmpty())
                {
                    dp.Add("F_BatchNumber", "%" + queryParam["F_BatchNumber"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_BatchNumber Like @F_BatchNumber ");
                }
                if (!queryParam["F_PayAmount"].IsEmpty())
                {
                    dp.Add("F_PayAmount", "%" + queryParam["F_PayAmount"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_PayAmount Like @F_PayAmount ");
                }
                if (!queryParam["F_PayStatus"].IsEmpty())
                {
                    dp.Add("F_PayStatus", queryParam["F_PayStatus"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PayStatus = @F_PayStatus ");
                }
                if (!queryParam["F_SettlementsId"].IsEmpty())
                {
                    dp.Add("F_SettlementsId", queryParam["F_SettlementsId"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_SettlementsId = @F_SettlementsId ");
                }
                return this.BaseRepository().FindList<SettlementsDetailsInfo>(strSql.ToString(), dp, pagination);
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



        public IEnumerable<SettlementsDetailsInfo> GetPageList(string F_SettlementsId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_SettlementDetailsId,
                t.F_BatchNumber,
                t.F_PayAmount,
                t.F_PayStatus,
                t.F_PayCondition,
                p.F_UserName,
                p.F_IDCardNo,
                p.F_ApplicantId,
                t.F_PersonId,
                t.F_PayDate
                ");
                strSql.Append("  FROM tc_SettlementsDetails t ");
                strSql.Append("  LEFT JOIN tc_Personnels p ON t.F_PersonId =p.F_PersonId ");
                strSql.Append("  LEFT JOIN tc_Settlements s ON s.F_SettlementsId =t.F_SettlementsId ");
                strSql.Append("  WHERE 1=1 ");

                // 虚拟参数
                var dp = new DynamicParameters(new { });

                if (!string.IsNullOrEmpty(F_SettlementsId))
                {
                    strSql.AppendFormat(" AND t.F_SettlementsId = '{0}' ", F_SettlementsId);
                }
                return this.BaseRepository().FindList<SettlementsDetailsInfo>(strSql.ToString());
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
        /// 获取tc_SettlementsDetails表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_SettlementsDetailsEntity Gettc_SettlementsDetailsEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_SettlementsDetailsEntity>(keyValue);
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<tc_SettlementsDetailsEntity>(t => t.F_SettlementDetailsId == keyValue);
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveEntity(string keyValue, tc_SettlementsDetailsEntity entity)
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
