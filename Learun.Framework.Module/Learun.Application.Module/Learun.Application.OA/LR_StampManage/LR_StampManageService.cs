using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.OA.LR_StampManage
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组（王飞）
    /// 日 期：2018.11.19
    /// 描 述：印章管理（数据库操作）
    /// </summary>
    public class LR_StampManageService : RepositoryFactory
    {
        #region 构造函数和属性
        private string fieldSql;
        public LR_StampManageService()
        {
            //sql字段
            fieldSql = @"
                        F_StampId,
                        F_StampName,
                        F_Description,
                        F_StampType,
                        F_ImgFile,
                        F_Sort,
                        F_EnabledMark
                        ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取印章信息（根据名称/状态:启用或停用模糊查询）
        /// </summary>
        /// <param name="keyWord">名称/状态</param>
        /// <returns></returns>
        public IEnumerable<LR_StampManageEntity> GetList(string keyWord)
        {
            try
            {
                StringBuilder Sql = new StringBuilder();
                Sql.Append("SELECT");
                Sql.Append(this.fieldSql);
                Sql.Append("FROM LR_Base_Stamp s where 1=1  ");
                Sql.Append("and s.F_EnabledMark =1  ");
                if (!string.IsNullOrEmpty(keyWord)) {// cbb 如果没有查询条件则不需要输入
                    Sql.AppendFormat(" and s.F_StampName LIKE '%{0}%'", keyWord);
                }

                Sql.Append(" Order by F_Sort");

                return this.BaseRepository().FindList<LR_StampManageEntity>(Sql.ToString());
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
        public IEnumerable<LR_StampManageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var queryParam = queryJson.ToJObject();
                var dp = new DynamicParameters(new { });
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Base_Stamp s  Where 1=1 ");
                if (queryParam.HasValues)
                {
                    if (!queryParam["F_StampName"].IsEmpty())
                    {
                            dp.Add("F_StampName", "%" + queryParam["F_StampName"].ToString() + "%", DbType.String);
                            strSql.Append(" AND s.F_StampName like @F_StampName ");
                    }
                    if (!queryParam["F_EnabledMark"].IsEmpty())
                    {
                        dp.Add("F_EnabledMark", queryParam["F_EnabledMark"].ToString() , DbType.String);
                        strSql.Append(" AND s.F_EnabledMark=@F_EnabledMark");
                    }
                    if (!queryParam["F_StampType"].IsEmpty())
                    {
                        dp.Add("F_StampType", queryParam["F_StampType"].ToString(), DbType.String);
                        strSql.Append(" AND s.F_StampType = @F_StampType");
                    }
                }
                return this.BaseRepository().FindList<LR_StampManageEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取印章实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public LR_StampManageEntity GetEntity(string keyValue)
        {

            try
            {
                return this.BaseRepository().FindEntity<LR_StampManageEntity>(keyValue);
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
        /// 保存印章信息（新增/编辑）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="stampEntity">实体</param>
        public void SaveEntity(string keyValue, LR_StampManageEntity entity)
        {
            try
            {
                //如果keyValue值为空或者null，表示，当前的操作是添加，否则是修改
                if (string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                else {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
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
        /// 删除印章信息
        /// </summary>
        /// <param name="keyVlaue">主键</param>
        public void DeleteEntity(string keyVlaue)
        {
            try
            {
                this.BaseRepository().Delete<LR_StampManageEntity>(s => s.F_StampId == keyVlaue);//删除操作
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
