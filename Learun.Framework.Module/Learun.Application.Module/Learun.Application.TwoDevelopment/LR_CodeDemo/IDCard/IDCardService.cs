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
        public IEnumerable<IDCardEntityInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_IDCardId,
                t.F_PersonId,
                p.F_UserName,
                p.F_IDCardNo,
                t.F_IssueDate,
                t.F_ExpirationDate,
                t.F_SafeguardType,
                t.F_WarehouseDate,
                t.F_Description,
                p.F_ApplicantId
                ");
                strSql.Append("  FROM tc_IDCard t ");
                strSql.Append("  LEFT JOIN tc_Personnels p ON t.F_PersonId =p.F_PersonId ");
                strSql.Append("  WHERE 1=1 ");
                strSql.Append("  AND t.F_DeleteMark=0 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_IDCardNo"].IsEmpty())
                {
                    dp.Add("F_IDCardNo", "%" + queryParam["F_IDCardNo"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_IDCardNo Like @F_IDCardNo ");
                }
                if (!queryParam["F_UserName"].IsEmpty())
                {
                    dp.Add("F_UserName", "%" + queryParam["F_UserName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_UserName Like @F_UserName ");
                }
                if (!queryParam["F_SafeguardType"].IsEmpty())
                {
                    dp.Add("F_SafeguardType", queryParam["F_SafeguardType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_SafeguardType = @F_SafeguardType ");
                }
                if (!queryParam["F_PersonId"].IsEmpty())
                {
                    dp.Add("F_PersonId", queryParam["F_PersonId"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PersonId = @F_PersonId ");
                }
                if (!queryParam["F_ApplicantId"].IsEmpty())
                {
                    dp.Add("F_ApplicantId", queryParam["F_ApplicantId"].ToString(), DbType.String);
                    strSql.Append(" AND p.F_ApplicantId = @F_ApplicantId ");
                }
                if (!queryParam["F_IssueDate"].IsEmpty())
                {
                    dp.Add("F_IssueDate", queryParam["F_IssueDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_IssueDate = @F_IssueDate ");
                }
                if (!queryParam["F_ExpirationDate"].IsEmpty())
                {
                    dp.Add("F_ExpirationDate", queryParam["F_ExpirationDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_ExpirationDate = @F_ExpirationDate ");
                }

                return this.BaseRepository().FindList<IDCardEntityInfo>(strSql.ToString(), dp, pagination);
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
                return this.BaseRepository().FindEntity<tc_IDCardEntity>(t => t.F_IDCardId == keyValue);
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
                str.Append(@"select a.F_PersonId id ,a.F_UserName text ,a.F_IDCardNo value ,'48741BEB-FA5E-B647-2ADA-1473A71FD524'  parentid   from  tc_Personnels a  where 1=1   ");
                if (!string.IsNullOrEmpty(PersonId))
                {
                    str.AppendFormat(@" and a.F_PersonId='{0}' ", PersonId);
                }
                str.Append(" union ");
                str.Append(@"     select '48741BEB-FA5E-B647-2ADA-1473A71FD524' id,'全部人员' text ,'' value,''  parentid ");
                //if (!string.IsNullOrEmpty(ApplicantId))
                //{
                //    str.AppendFormat(@" and b.id='{0}' ", ApplicantId);
                //}

                return this.BaseRepository().FindTable(str.ToString());



                //StringBuilder str = new StringBuilder();
                //str.Append(@"select a.F_PersonId id ,a.F_UserName text ,a.F_IDCardNo value ,a.F_ApplicantId  parentid   from  tc_Personnels a  where 1=1   ");
                //if (!string.IsNullOrEmpty(PersonId))
                //{
                //    str.AppendFormat(@" and a.F_PersonId='{0}' ", PersonId);
                //}
                //str.Append(" union ");
                //str.Append(@"   select b.F_ApplicantId ,b.F_CompanyName text ,'' value ,''  parentid   from  tc_Applicant b  where   b.F_ApplicantType=1 ");
                //if (!string.IsNullOrEmpty(ApplicantId))
                //{
                //    str.AppendFormat(@" and b.F_ApplicantId='{0}' ", ApplicantId);
                //}

                //return this.BaseRepository().FindTable(str.ToString());
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
                tc_IDCardEntity entity = new tc_IDCardEntity()
                {
                    F_IDCardId = keyValue,
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
            //var db = this.BaseRepository().BeginTrans();
            //try
            //{
            //    db.Delete<tc_IDCardEntity>(t => t.F_IDCardId == keyValue);
            //    db.Commit();
            //}
            //catch (Exception ex)
            //{
            //    db.Rollback();
            //    if (ex is ExceptionEx)
            //    {
            //        throw;
            //    }
            //    else
            //    {
            //        throw ExceptionEx.ThrowServiceException(ex);
            //    }
            //}
        }

        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        public void SaveEntity(string keyValue, tc_IDCardEntity entity)
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
