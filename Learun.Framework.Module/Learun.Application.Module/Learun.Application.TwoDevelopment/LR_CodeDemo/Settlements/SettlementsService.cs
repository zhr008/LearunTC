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
    /// 日 期：2020-07-05 20:42
    /// 描 述：合同结算
    /// </summary>
    public class SettlementsService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<SettlementsInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                p.F_UserName,
                p.F_IDCardNo,
                t.F_PersonId,
                t.F_SettlementsId,
                t.F_ContractStatus,
                t.F_ContractStartDate,
                t.F_ContractEndDate,
                t.F_Mobile,
                t.F_Address,
                t.F_ApplicantId,
                t.F_payee,
                t.F_BankName,
                t.F_BankAccount,
                t.F_PersonAmount,
                t.F_ApplicantAmount,
                t.F_ContractAmount,
                t.F_PayStatus,
                t.F_PayTotalAmount,
                t.F_Maintain,
                t.F_OtherContact,
                t.F_PayNumber,
                p.F_ApplicantId ApplicantId
                ");
                strSql.Append("  FROM tc_Settlements t ");
                strSql.Append("  LEFT JOIN tc_Personnels p ON t.F_PersonId =p.F_PersonId ");
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
                if (!queryParam["F_PersonId"].IsEmpty())
                {
                    dp.Add("F_PersonId", queryParam["F_PersonId"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PersonId = @F_PersonId ");
                }
                if (!queryParam["F_ContractStatus"].IsEmpty())
                {
                    dp.Add("F_ContractStatus", queryParam["F_ContractStatus"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_ContractStatus = @F_ContractStatus ");
                }
                if (!queryParam["F_ContractStartDate"].IsEmpty())
                {
                    dp.Add("F_ContractStartDate", queryParam["F_ContractStartDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_ContractStartDate = @F_ContractStartDate ");
                }
                if (!queryParam["F_ContractEndDate"].IsEmpty())
                {
                    dp.Add("F_ContractEndDate", queryParam["F_ContractEndDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_ContractEndDate = @F_ContractEndDate ");
                }
                if (!queryParam["F_Mobile"].IsEmpty())
                {
                    dp.Add("F_Mobile", "%" + queryParam["F_Mobile"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Mobile Like @F_Mobile ");
                }
                if (!queryParam["F_payee"].IsEmpty())
                {
                    dp.Add("F_payee", "%" + queryParam["F_payee"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_payee Like @F_payee ");
                }
                if (!queryParam["F_BankName"].IsEmpty())
                {
                    dp.Add("F_BankName", "%" + queryParam["F_BankName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_BankName Like @F_BankName ");
                }
                if (!queryParam["F_PayStatus"].IsEmpty())
                {
                    dp.Add("F_PayStatus", queryParam["F_PayStatus"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PayStatus = @F_PayStatus ");
                }
                if (!queryParam["F_ApplicantId"].IsEmpty())
                {
                    dp.Add("F_ApplicantId", queryParam["F_ApplicantId"].ToString(), DbType.String);
                    strSql.Append(" AND p.F_ApplicantId = @F_ApplicantId ");
                }
                return this.BaseRepository().FindList<SettlementsInfo>(strSql.ToString(), dp, pagination);
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
        /// 获取tc_Settlements表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_SettlementsEntity Gettc_SettlementsEntity(string keyValue)
        {
            try
            {

                return this.BaseRepository().FindEntity<tc_SettlementsEntity>(keyValue);
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
                tc_SettlementsEntity entity = new tc_SettlementsEntity()
                {
                    F_SettlementsId = keyValue,
                    F_DeleteMark = 1
                };
                this.BaseRepository().Update(entity);

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
        public void SaveEntity(string keyValue, tc_SettlementsEntity entity)
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
