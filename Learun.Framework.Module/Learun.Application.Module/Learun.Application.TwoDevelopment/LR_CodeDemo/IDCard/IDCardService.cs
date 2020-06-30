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
    /// 日 期：2020-06-29 21:15
    /// 描 述：身份证管理
    /// </summary>
    public class IDCardService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tc_IDCardEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t1.F_IDCardId,
                t1.F_PersonId,
                t1.F_IDCardNo,
                t1.F_UserName,
                t1.F_IssueDate,
                t1.F_ExpirationDate,
                t1.F_SafeguardType,
                t1.F_WarehouseDate,
                t1.F_Description
                ");
                strSql.Append("  FROM tc_IDCard t1 ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_IDCardNo"].IsEmpty())
                {
                    dp.Add("F_IDCardNo", "%" + queryParam["F_IDCardNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.F_IDCardNo Like @F_IDCardNo ");
                }
                if (!queryParam["F_UserName"].IsEmpty())
                {
                    dp.Add("F_UserName", "%" + queryParam["F_UserName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t1.F_UserName Like @F_UserName ");
                }
                if (!queryParam["F_SafeguardType"].IsEmpty())
                {
                    dp.Add("F_SafeguardType", queryParam["F_SafeguardType"].ToString(), DbType.String);
                    strSql.Append(" AND t1.F_SafeguardType = @F_SafeguardType ");
                }
                if (!queryParam["F_PersonId"].IsEmpty())
                {
                    dp.Add("F_PersonId", queryParam["F_PersonId"].ToString(), DbType.String);
                    strSql.Append(" AND t1.F_PersonId = @F_PersonId ");
                }
                if (!queryParam["F_IssueDate"].IsEmpty())
                {
                    dp.Add("F_IssueDate", queryParam["F_IssueDate"].ToString(), DbType.String);
                    strSql.Append(" AND t1.F_IssueDate = @F_IssueDate ");
                }
                if (!queryParam["F_ExpirationDate"].IsEmpty())
                {
                    dp.Add("F_ExpirationDate", queryParam["F_ExpirationDate"].ToString(), DbType.String);
                    strSql.Append(" AND t1.F_ExpirationDate = @F_ExpirationDate ");
                }

                return this.BaseRepository().FindList<tc_IDCardEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取tc_IDCard表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_IDCardEntity Gettc_IDCardEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_IDCardEntity>(t => t.F_PersonId == keyValue);
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
        /// 获取tc_Personnels表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_PersonnelsEntity Gettc_PersonnelsEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_PersonnelsEntity>(keyValue);
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
        public DataTable GetSqlTree(string PersonId, string ApplicantId)
        {
            try
            {

                StringBuilder str = new StringBuilder();
                str.Append(@"select a.F_PersonId id ,a.F_UserName text ,a.F_IDCardNo value ,a.F_ApplicantId  parentid   from  tc_Personnels a  where 1=1 ");
                if (!string.IsNullOrEmpty(PersonId))
                {
                    str.AppendFormat(@" and a.F_PersonId='{0}' ", PersonId);
                }
                str.Append(" union ");
                str.Append(@"   select b.F_ApplicantId ,b.F_CompanyName text ,'' value ,''  parentid   from  tc_Applicant b  where 1=1 ");
                if (!string.IsNullOrEmpty(ApplicantId))
                {
                    str.AppendFormat(@" and b.F_ApplicantId='{0}' ", ApplicantId);
                }

                return this.BaseRepository().FindTable(str.ToString());
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
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<tc_IDCardEntity>(t => t.F_IDCardId == keyValue);
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveEntity(string keyValue, tc_IDCardEntity tc_IDCardEntity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var tc_PersonnelsEntityTmp = Gettc_IDCardEntity(keyValue);
                    tc_IDCardEntity.Modify(keyValue);
                    db.Update(tc_IDCardEntity);
                }
                else
                {

                    tc_IDCardEntity.Create();
                    tc_IDCardEntity.F_PersonId = tc_IDCardEntity.F_PersonId;
                    db.Insert(tc_IDCardEntity);
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
