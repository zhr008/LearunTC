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
    /// 日 期：2020-07-05 19:31
    /// 描 述：从业经历
    /// </summary>
    public class WorkExperienceService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<WorkExperienceInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_PersonId,
                p.F_IDCardNo,
                p.F_UserName,
                t.F_WorkExperienceId,
                t.F_CompanyName,
                t.F_VocationType,
                t.F_EntryDate,
                t.F_QuitDate,
                t.F_CertType,
                t.F_CheckInDate,
                t.F_MajorProjects,
                t.F_Description,
                p.F_ApplicantId
                ");
                strSql.Append("  FROM tc_WorkExperience t ");
                strSql.Append("  LEFT JOIN tc_Personnels p ON p.F_PersonId =t.F_PersonId ");
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
                if (!queryParam["F_CompanyName"].IsEmpty())
                {
                    dp.Add("F_CompanyName", "%" + queryParam["F_CompanyName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_CompanyName Like @F_CompanyName ");
                }
                if (!queryParam["F_VocationType"].IsEmpty())
                {
                    dp.Add("F_VocationType", queryParam["F_VocationType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_VocationType = @F_VocationType ");
                }
                if (!queryParam["F_CertType"].IsEmpty())
                {
                    dp.Add("F_CertType", queryParam["F_CertType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertType = @F_CertType ");
                }
                if (!queryParam["F_EntryDate"].IsEmpty())
                {
                    dp.Add("F_EntryDate", queryParam["F_EntryDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_EntryDate = @F_EntryDate ");
                }
                if (!queryParam["F_QuitDate"].IsEmpty())
                {
                    dp.Add("F_QuitDate", queryParam["F_QuitDate"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_QuitDate = @F_QuitDate ");
                }
                return this.BaseRepository().FindList<WorkExperienceInfo>(strSql.ToString(), dp, pagination);
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
        /// 获取tc_WorkExperience表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_WorkExperienceEntity Gettc_WorkExperienceEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_WorkExperienceEntity>(keyValue);
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

                tc_WorkExperienceEntity entity = new tc_WorkExperienceEntity()
                {
                    F_WorkExperienceId = keyValue,
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
        public void SaveEntity(string keyValue, tc_WorkExperienceEntity entity)
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
