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
    /// 日 期：2020-07-02 23:56
    /// 描 述：个人资格证
    /// </summary>
    public class CredentialsService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CredentialsInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_CredentialsId,
                t.F_CertType,
                t.F_MajorType,
                t.F_Major,
                t.F_CertOrganization,
                t.F_CertDateBegin,
                t.F_CertDateEnd,
                t.F_CertStyle,
                t.F_CertStatus,
                t.F_PracticeStyle,
                t.F_PracticeSealStyle,
                t.F_CheckInTime,
                t.F_Description,
                p.F_UserName,
                p.F_IDCardNo,
                p.F_ApplicantId
                ");
                strSql.Append("  FROM tc_Credentials t ");
                strSql.Append("  LEFT JOIN tc_Personnels p ON t.F_PersonId =p.F_PersonId ");
                strSql.Append("  WHERE 1=1 ");
                strSql.Append("  AND t.F_DeleteMark=0 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
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
                if (!queryParam["F_CertType"].IsEmpty())
                {
                    dp.Add("F_CertType",queryParam["F_CertType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertType = @F_CertType ");
                }
                if (!queryParam["F_MajorType"].IsEmpty())
                {
                    dp.Add("F_MajorType",queryParam["F_MajorType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_MajorType = @F_MajorType ");
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
                    dp.Add("F_CertDateBegin",queryParam["F_CertDateBegin"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertDateBegin = @F_CertDateBegin ");
                }
                if (!queryParam["F_CertDateEnd"].IsEmpty())
                {
                    dp.Add("F_CertDateEnd",queryParam["F_CertDateEnd"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertDateEnd = @F_CertDateEnd ");
                }
                if (!queryParam["F_CertStyle"].IsEmpty())
                {
                    dp.Add("F_CertStyle",queryParam["F_CertStyle"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertStyle = @F_CertStyle ");
                }
                if (!queryParam["F_CertStatus"].IsEmpty())
                {
                    dp.Add("F_CertStatus",queryParam["F_CertStatus"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_CertStatus = @F_CertStatus ");
                }
                if (!queryParam["F_PracticeStyle"].IsEmpty())
                {
                    dp.Add("F_PracticeStyle",queryParam["F_PracticeStyle"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PracticeStyle = @F_PracticeStyle ");
                }
                if (!queryParam["F_PracticeSealStyle"].IsEmpty())
                {
                    dp.Add("F_PracticeSealStyle",queryParam["F_PracticeSealStyle"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PracticeSealStyle = @F_PracticeSealStyle ");
                }
                return this.BaseRepository().FindList<CredentialsInfo>(strSql.ToString(),dp, pagination);
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
        /// 获取tc_Credentials表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_CredentialsEntity Gettc_CredentialsEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_CredentialsEntity>(keyValue);
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


        public tc_CredentialsEntity Gettc_CredentialsEntity(string keyValue, int? F_CertType, int? F_MajorType)
        {
            try
            {
                var expression = LinqExtensions.True<tc_CredentialsEntity>();
                if (!string.IsNullOrEmpty(keyValue))
                {
                    expression = expression.And(t => t.F_PersonId == keyValue);
                }
                if (F_CertType>0)
                {
                    expression = expression.And(t => t.F_CertType == F_CertType);
                }
                if (F_MajorType > 0)
                {
                    expression = expression.And(t => t.F_MajorType == F_MajorType);
                }
                //expression = expression.And(t => t.F_DeleteMark == 0);
                return this.BaseRepository().FindEntity<tc_CredentialsEntity>(expression);
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
                tc_CredentialsEntity entity = new tc_CredentialsEntity()
                {
                    F_CredentialsId= keyValue,
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
        public void SaveEntity(string keyValue, tc_CredentialsEntity entity)
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



        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetSqlTree(string PersonId, string ApplicantId)
        {
            try
            {

                StringBuilder str = new StringBuilder();
                str.Append(@"      select   a.F_PersonId id ,b.F_UserName text ,a.F_CertType value ,a.F_CertType  parentid   from dbo.tc_Credentials  a  
      left join dbo.tc_Personnels b  on a.F_PersonId= b.F_PersonId where b.F_UserName='张奇芳'     group by  a.F_PersonId,b.F_UserName,a.F_CertType   ");
                if (!string.IsNullOrEmpty(PersonId))
                {
                    str.AppendFormat(@" and a.F_PersonId='{0}' ", PersonId);
                }
                str.Append(" union all ");
                str.Append(@"  select   F_ItemValue id, F_ItemName  text, F_ItemValue value,'' parentid    from lr_base_dataitemdetail where F_ItemId= 'a9ce0b84-101c-4e0f-9828-0ec921dfe7bb'   ");
                if (!string.IsNullOrEmpty(ApplicantId))
                {
                    str.AppendFormat(@" and b.id='{0}' ", ApplicantId);
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

    }
}
