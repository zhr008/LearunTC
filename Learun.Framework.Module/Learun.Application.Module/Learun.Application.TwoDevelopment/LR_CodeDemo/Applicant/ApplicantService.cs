using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>


    /// 创 建：超级管理员
    /// 日 期：2020-06-27 23:47
    /// 描 述：供应商登记
    /// </summary>
    public class ApplicantService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tc_ApplicantEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_ApplicantId,
                t.F_CompanyName,
                t.F_RegistrationNo,
                t.F_ProvinceId,
                t.F_CityId,
                t.F_CountyId,
                t.F_Address,
                t.F_RegisteredCapital,
                t.F_Representative,
                t.F_EstablishDate,
                t.F_BusinessUpdateDate,
                t.F_QualificationCert,
                t.F_ManageType,
                t.F_Description,
                t.F_ApplicantType,
                t.F_CertDateBegin,
                t.F_CertDateEnd,
                t.F_AccountName,
                t.F_SupplyType

                ");
                strSql.Append("  FROM tc_Applicant t ");
                strSql.Append("  WHERE 1=1 ");
                strSql.Append("  AND t.F_DeleteMark=0 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_CompanyName"].IsEmpty())
                {
                    dp.Add("F_CompanyName", "%" + queryParam["F_CompanyName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_CompanyName Like @F_CompanyName ");
                }
                if (!queryParam["F_Representative"].IsEmpty())
                {
                    dp.Add("F_Representative", "%" + queryParam["F_Representative"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Representative Like @F_Representative ");
                }
                if (!queryParam["F_ManageType"].IsEmpty())
                {
                    dp.Add("F_ManageType", queryParam["F_ManageType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_ManageType = @F_ManageType ");
                }
                if (!queryParam["F_ApplicantType"].IsEmpty())
                {
                    dp.Add("F_ApplicantType", queryParam["F_ApplicantType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_ApplicantType = @F_ApplicantType ");
                }
                return this.BaseRepository().FindList<tc_ApplicantEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取tc_Applicant表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_ApplicantEntity Gettc_ApplicantEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_ApplicantEntity>(keyValue);
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
                tc_ApplicantEntity entity = new tc_ApplicantEntity()
                {
                    F_ApplicantId = keyValue,
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
        public void SaveEntity(string keyValue, tc_ApplicantEntity entity)
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


        public List<tc_ApplicantEntity> GetApplicantRepresentative(string PersonId) 
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@" select * from dbo.tc_Applicant where F_ApplicantId=(
select F_ApplicantId from dbo.tc_Personnels where F_PersonId = '{0}')
or F_CompanyName = '本人'", PersonId);

            return this.BaseRepository().FindList<tc_ApplicantEntity>(sb.ToString()).ToList();

        }
        #endregion

    }
}
