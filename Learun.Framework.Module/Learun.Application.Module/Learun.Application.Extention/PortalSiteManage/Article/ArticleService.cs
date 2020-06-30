using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.Extention.PortalSiteManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-05 09:35
    /// 描 述：详细信息维护
    /// </summary>
    public class ArticleService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public ArticleService()
        {
            fieldSql= @"
                t.F_Id,
                t.F_Title,
                t.F_Category,
                t.F_Content,
                t.F_PushDate,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<ArticleEntity> GetList( string queryJson )
        {
            try
            {
                //参考写法
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                //var dp = new DynamicParameters(new { });
                //dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_PS_Article t where 1=1 ");

                string category = "";
                if (!queryParam["category"].IsEmpty()) {
                    strSql.Append(" AND t.F_Category = @category  ");
                    category = queryParam["category"].ToString();
                }

                return this.BaseRepository().FindList<ArticleEntity>(strSql.ToString(),new { category });
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
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<ArticleEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var dp = new DynamicParameters(new { });
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                        t.F_Id,
                        t.F_Title,
                        t.F_Category,
                        t.F_PushDate,
                        t.F_CreateDate,
                        t.F_CreateUserId,
                        t.F_CreateUserName,
                        t.F_ModifyDate,
                        t.F_ModifyUserId,
                        t.F_ModifyUserName
                 ");
                strSql.Append(" FROM LR_PS_Article t ");
                strSql.Append(" where 1=1");
                if (queryParam.HasValues)
                {
                    if (!queryParam["F_Category"].IsEmpty())
                    {
                        dp.Add("F_Category", queryParam["F_Category"].ToString(), DbType.String);
                        strSql.Append(" AND t.F_Category = @F_Category ");
                    }
                    if (!queryParam["F_Title"].IsEmpty())
                    {
                        dp.Add("F_Title", "%" + queryParam["F_Title"].ToString() + "%", DbType.String);
                        strSql.Append(" AND t.F_Title like @F_Title ");
                    }
                    if (!queryParam["dateBegin"].IsEmpty() && !queryParam["dateEnd"].IsEmpty())
                    {
                        dp.Add("dateBegin", queryParam["dateBegin"].ToString(), DbType.String);
                        dp.Add("dateEnd", queryParam["dateEnd"].ToString(), DbType.String);
                        strSql.Append(" AND (t.F_PushDate >= @dateBegin AND t.F_PushDate <= @dateEnd )");
                    }
                }
                return this.BaseRepository().FindList<ArticleEntity>(strSql.ToString(),dp, pagination);
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
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public ArticleEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<ArticleEntity>(keyValue);
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
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<ArticleEntity>(t=>t.F_Id == keyValue);
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
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, ArticleEntity entity)
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
