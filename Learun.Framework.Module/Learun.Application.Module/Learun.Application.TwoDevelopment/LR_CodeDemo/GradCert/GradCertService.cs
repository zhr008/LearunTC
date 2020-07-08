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
    /// 日 期：2020-07-02 23:28
    /// 描 述：毕业证书
    /// </summary>
    public class GradCertService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        /// <summary>
        /// 构造方法
        /// </summary>
        public GradCertService()
        {
            fieldSql=@"
                t.F_GradCertId,
                t.F_UserName,
                t.F_IDCardNo,
                t.F_PersonId,
                t.F_Major,
                t.F_GradTime,
                t.F_EducationType,
                t.F_Term,
                t.F_OriginalType,
                t.F_DeleteMark,
                t.F_Description,
                t.F_CreateDate,
                t.F_CreateUserName,
                t.F_CreateUserId,
                t.F_ModifyDate,
                t.F_ModifyUserName,
                t.F_ModifyUserId
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        public IEnumerable<tc_GradCertEntity> GetList( string queryJson )
        {
            try
            {
                //参考写法
                //var queryParam = queryJson.ToJObject();
                // 虚拟参数
                //var dp = new DynamicParameters(new { });
                //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM tc_GradCert t ");
                return this.BaseRepository().FindList<tc_GradCertEntity>(strSql.ToString());
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
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        public IEnumerable<GradCertInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@" 
                t.F_GradCertId,
                p.F_UserName,
                p.F_IDCardNo,
                t.F_PersonId,
                t.F_Major,
                t.F_GradTime,
                t.F_EducationType,
                t.F_Term,
                t.F_OriginalType,
                t.F_DeleteMark,
                t.F_Description,
                t.F_CreateDate,
                t.F_CreateUserName,
                t.F_CreateUserId,
                t.F_ModifyDate,
                t.F_ModifyUserName,
                t.F_ModifyUserId,
                p.F_ApplicantId
                ");
           
                strSql.Append(" FROM tc_GradCert t ");
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
                if (!queryParam["F_ApplicantId"].IsEmpty())
                {
                    dp.Add("F_ApplicantId", queryParam["F_ApplicantId"].ToString(), DbType.String);
                    strSql.Append(" AND p.F_ApplicantId = @F_ApplicantId ");
                }
                if (!queryParam["F_Major"].IsEmpty())
                {
                    dp.Add("F_Major", queryParam["F_Major"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Major = @F_Major ");
                }
                if (!queryParam["F_EducationType"].IsEmpty())
                {
                    dp.Add("F_EducationType", queryParam["F_EducationType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_EducationType = @F_EducationType ");
                }
                if (!queryParam["F_Term"].IsEmpty())
                {
                    dp.Add("F_Term", queryParam["F_Term"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Term = @F_Term ");
                }
                if (!queryParam["F_OriginalType"].IsEmpty())
                {
                    dp.Add("F_OriginalType", queryParam["F_OriginalType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_OriginalType = @F_OriginalType ");
                }

                return this.BaseRepository().FindList<GradCertInfo>(strSql.ToString(),dp, pagination);
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
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_GradCertEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_GradCertEntity>(keyValue);
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
                tc_GradCertEntity entity = new tc_GradCertEntity()
                {
                    F_GradCertId = keyValue,
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
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// </summary>
        public void SaveEntity(string keyValue, tc_GradCertEntity entity)
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
