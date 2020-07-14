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
    /// 日 期：2020-07-14 23:25
    /// 描 述：1231
    /// </summary>
    public class RelationService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        /// <summary>
        /// 构造方法
        /// </summary>
        public RelationService()
        {
            fieldSql=@"
                t.F_RelationId,
                t.ProjectId,
                t.ProjectDetailId,
                t.F_CertId,
                t.F_PersonId,
                t.F_Description,
                t.F_CreateDate,
                t.F_CreateUserName,
                t.F_CreateUserId,
                t.F_ModifyDate,
                t.F_ModifyUserName,
                t.F_ModifyUserId,
                t.F_DeleteMark
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        public IEnumerable<tc_RelationEntity> GetList( string queryJson )
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
                strSql.Append(" FROM tc_Relation t ");
                return this.BaseRepository().FindList<tc_RelationEntity>(strSql.ToString());
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
        public IEnumerable<tc_RelationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM tc_Relation t ");
                return this.BaseRepository().FindList<tc_RelationEntity>(strSql.ToString(), pagination);
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



        public IEnumerable<RelationDeatil> GetRelationDetail(string ProjectDetailId)
        {

            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@"select  

n.F_UserName,
      n.F_IDCardNo,
      c.F_PersonId,
      c.F_CertType,
      c.F_MajorType,
      c.F_Major,
      c.F_CertOrganization,
      c.F_CertDateBegin,
      c.F_CertDateEnd,
      c.F_CertStatus,
      c.F_CertStyle,
      c.F_PracticeStyle,
      c.F_PracticeSealStyle,
      c.F_CheckInTime,
      c.F_Description from dbo.tc_Relation t
left
                      join dbo.tc_Credentials c on t.F_CertId = c.F_CredentialsId
                 left
                      join dbo.tc_Personnels n on n.F_personId = t.F_personid");
                strSql.AppendFormat(@"  where t.ProjectDetailId='{0}' ", ProjectDetailId);
                return this.BaseRepository().FindList<RelationDeatil>(strSql.ToString());
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_RelationEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_RelationEntity>(keyValue);
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
                this.BaseRepository().Delete<tc_RelationEntity>(t=>t.F_RelationId == keyValue);
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
        public void SaveEntity(string keyValue, tc_RelationEntity entity)
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
