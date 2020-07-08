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
    /// 日 期：2020-07-05 19:59
    /// 描 述：培训记录
    /// </summary>
    public class PersonnelTrainService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<PersonnelTrainInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                p.F_UserName,
                p.F_IDCardNo,
                t.F_PersonnelTrainId,
                t.F_CertType,
                t.F_MajorType,
                t.F_Major,
                t.F_CertOrganization,
                t.F_CertDateBegin,
                t.F_CertDateEnd,
                t.F_CertStyle,
                t.F_CertStatus,
                t.F_TrainStatus,
                t.F_CheckInDate,
                t.F_ApplyDate,
                t.F_ExpectedTrainDate,
                t.F_FeeStandard,
                t.F_TrainCollectStatus,
                t.F_TrainOrgName,
                t.F_TrainOrgBankName,
                t.F_TrainOrgBankAccount,
                t.F_TrainPayAmount,
                t.F_TrainPayVoucher,
                t.F_TrainPayStatus,
                t.F_Description
                ");
                strSql.Append("  FROM tc_PersonnelTrain t ");
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
                if (!queryParam["F_CertType"].IsEmpty())
                {
                    dp.Add("F_CertType", "%" + queryParam["F_CertType"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_CertType Like @F_CertType ");
                }
                if (!queryParam["F_MajorType"].IsEmpty())
                {
                    dp.Add("F_MajorType", "%" + queryParam["F_MajorType"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_MajorType Like @F_MajorType ");
                }
                if (!queryParam["F_Major"].IsEmpty())
                {
                    dp.Add("F_Major", "%" + queryParam["F_Major"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Major Like @F_Major ");
                }
                if (!queryParam["F_CertOrganization"].IsEmpty())
                {
                    dp.Add("F_CertOrganization", "%" + queryParam["F_CertOrganization"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_CertOrganization Like @F_CertOrganization ");
                }
                if (!queryParam["F_CertDateBegin"].IsEmpty())
                {
                    dp.Add("F_CertDateBegin", queryParam["F_CertDateBegin"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertDateBegin = @F_CertDateBegin ");
                }
                if (!queryParam["F_CertDateEnd"].IsEmpty())
                {
                    dp.Add("F_CertDateEnd", queryParam["F_CertDateEnd"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertDateEnd = @F_CertDateEnd ");
                }
                if (!queryParam["F_CertStatus"].IsEmpty())
                {
                    dp.Add("F_CertStatus", queryParam["F_CertStatus"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertStatus = @F_CertStatus ");
                }
                if (!queryParam["F_TrainPayStatus"].IsEmpty())
                {
                    dp.Add("F_TrainPayStatus", queryParam["F_TrainPayStatus"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_TrainPayStatus = @F_TrainPayStatus ");
                }
                return this.BaseRepository().FindList<PersonnelTrainInfo>(strSql.ToString(), dp, pagination);
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
        /// 获取tc_PersonnelTrain表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_PersonnelTrainEntity Gettc_PersonnelTrainEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_PersonnelTrainEntity>(keyValue);
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

                tc_PersonnelTrainEntity entity = new tc_PersonnelTrainEntity()
                {
                    F_PersonnelTrainId = keyValue,
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
        public void SaveEntity(string keyValue, tc_PersonnelTrainEntity entity)
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
