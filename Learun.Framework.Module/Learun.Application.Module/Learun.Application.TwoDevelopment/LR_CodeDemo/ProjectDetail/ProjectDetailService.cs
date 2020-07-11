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
    /// 日 期：2020-07-10 18:11
    /// 描 述：项目详情
    /// </summary>
    public class ProjectDetailService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<tc_ProjectDetailEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ProjectDetailId,
                t.CertType,
                t.CertMajor,
                t.StandardNum,
                t.SocialSecurityRequire,
                t.CertRequire,
                t.IDCardRequire,
                t.GradCertRequire,
                t.SceneRequire,
                t.OtherRequire,
                t.AlreadyNum,
                t.NeedNum,
                t.Status,
                t.F_Description
                ");
                strSql.Append("  FROM tc_ProjectDetail t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["ProjectId"].IsEmpty())
                {
                    dp.Add("ProjectId", queryParam["ProjectId"].ToString(), DbType.String);
                    strSql.Append(" AND t.ProjectId = @ProjectId ");
                }
                return this.BaseRepository().FindList<tc_ProjectDetailEntity>(strSql.ToString(),dp, pagination);
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





        public IEnumerable<tc_ProjectDetailEntity> GetPageListByProjectId(string ProjectId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.ProjectDetailId,
                t.ProjectId,
                t.CertType,
                t.CertMajor,
                t.StandardNum,
                t.SocialSecurityRequire,
                t.CertRequire,
                t.IDCardRequire,
                t.GradCertRequire,
                t.SceneRequire,
                t.OtherRequire,
                t.AlreadyNum,
                t.NeedNum,
                t.Status,
                t.F_Description
                ");
                strSql.Append("  FROM tc_ProjectDetail t ");
                strSql.Append("  WHERE 1=1 ");
              
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!string.IsNullOrEmpty(ProjectId))
                {
                  
                    strSql.AppendFormat(" AND t.ProjectId = '{0}' ", ProjectId);
                }
                return this.BaseRepository().FindList<tc_ProjectDetailEntity>(strSql.ToString());
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
        /// 获取tc_ProjectDetail表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_ProjectDetailEntity Gettc_ProjectDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<tc_ProjectDetailEntity>(keyValue);
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
                this.BaseRepository().Delete<tc_ProjectDetailEntity>(t=>t.ProjectDetailId == keyValue);
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
        public void SaveEntity(string keyValue, tc_ProjectDetailEntity entity)
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
