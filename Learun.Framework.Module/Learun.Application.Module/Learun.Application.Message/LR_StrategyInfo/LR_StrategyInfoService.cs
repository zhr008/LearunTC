using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Learun.Application.Message
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-10-16 16:24
    /// 描 述：消息策略
    /// </summary>
    public class LR_StrategyInfoService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public LR_StrategyInfoService()
        {
            fieldSql=@"
                t.F_Id,
                t.F_StrategyName,
                t.F_StrategyCode,
                t.F_SendRole,
                t.F_MessageType,
                t.F_Description,
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
        public IEnumerable<LR_MS_StrategyInfoEntity> GetList( string queryJson )
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
                strSql.Append(" FROM LR_MS_StrategyInfo t ");
                return this.BaseRepository().FindList<LR_MS_StrategyInfoEntity>(strSql.ToString());
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
        public IEnumerable<LR_MS_StrategyInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var dp = new DynamicParameters(new { });
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_MS_StrategyInfo t  where 1=1");
                if (queryParam.HasValues)
                {
                    if (!queryParam["keyword"].IsEmpty())
                    {
                        dp.Add("keyword", "%" + queryParam["keyword"].ToString() + "%", DbType.String);
                        strSql.Append(" AND t.F_StrategyName like @keyword  or t.F_StrategyCode like @keyword");
                    }
                    if (!queryParam["dateBegin"].IsEmpty() && !queryParam["dateEnd"].IsEmpty())
                    {
                        dp.Add("dateBegin", queryParam["dateBegin"].ToString(), DbType.String);
                        dp.Add("dateEnd", queryParam["dateEnd"].ToString(), DbType.String);
                        strSql.Append(" AND (t.F_CreateDate >= @dateBegin AND t.F_CreateDate <= @dateEnd )");
                    }
                }
                return this.BaseRepository().FindList<LR_MS_StrategyInfoEntity>(strSql.ToString(),dp,pagination);
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
        public LR_MS_StrategyInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_MS_StrategyInfoEntity>(keyValue);
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
        /// 根据策略编码获取策略
        /// </summary>
        /// <param name="code">策略编码</param>
        /// <returns></returns>
        public LR_MS_StrategyInfoEntity GetEntityByCode(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_MS_StrategyInfoEntity>(t=>t.F_StrategyCode==code);
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
                this.BaseRepository().Delete<LR_MS_StrategyInfoEntity>(t=>t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, LR_MS_StrategyInfoEntity entity)
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

        #region 编码验证
        /// <summary>
        ///策略编码不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="F_StrategyCode">编码</param>
        /// <returns></returns>
        public bool ExistStrategyCode(string keyValue,string F_StrategyCode)
        {
            try
            {
                var expression = LinqExtensions.True<LR_MS_StrategyInfoEntity>();
                expression = expression.And(t => t.F_StrategyCode == F_StrategyCode);
                if (!string.IsNullOrEmpty(keyValue))
                {
                    expression = expression.And(t => t.F_Id != keyValue);
                }
                return this.BaseRepository().IQueryable(expression).Count() == 0 ? true : false;
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
