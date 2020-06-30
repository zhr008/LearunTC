using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.Language
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:08
    /// 描 述：语言类型
    /// </summary>
    public class LGTypeService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public LGTypeService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_Code,
                t.F_IsMain
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取语言类型全部数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGTypeEntity> GetAllData()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Lg_Type");
                return this.BaseRepository().FindList<LGTypeEntity>(strSql.ToString());
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
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGTypeEntity> GetList(string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Lg_Type t WHERE 1=1 ");
                //参考写法
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (queryParam.Property("keyword") != null && queryParam.Property("keyword").ToString() != "")
                {
                    dp.Add("F_Name", "%" + queryParam["keyword"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_Name like @F_Name ");
                }
                strSql.Append(" ORDER BY t.F_IsMain DESC ");
                return this.BaseRepository().FindList<LGTypeEntity>(strSql.ToString(), dp);
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
        public IEnumerable<LGTypeEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_Lg_Type t ");
                return this.BaseRepository().FindList<LGTypeEntity>(strSql.ToString(), pagination);
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
        public LGTypeEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LGTypeEntity>(keyValue);
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
        public LGTypeEntity GetEntityByCode(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LGTypeEntity>(t => t.F_Code == keyValue);
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
            var db = this.BaseRepository().BeginTrans();
            try
            {
                //删除实体数据
                db.Delete<LGTypeEntity>(t => t.F_Id == keyValue);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, LGTypeEntity entity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //更改映照表对应字段
                    var oldentity = GetEntity(keyValue);
                    var sql = "UPDATE LR_Lg_Map SET F_TypeCode='" + entity.F_Code + "' WHERE F_TypeCode='" + oldentity.F_Code + "'";
                    db.ExecuteBySql(sql);
                    entity.Modify(keyValue);
                    db.Update(entity);
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                }
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
        /// 设为主语言
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SetMainLG(string keyValue)
        {
            var db = this.BaseRepository("BaseDb").BeginTrans();
            try
            {
                string sql = "UPDATE LR_Lg_Type SET F_IsMain=0 WHERE F_IsMain=1";
                db.ExecuteBySql(sql);
                string sql1 = "UPDATE LR_Lg_Type SET F_IsMain=1 WHERE F_Id='" + keyValue + "'";
                db.ExecuteBySql(sql1);
                db.Commit();
            }
            catch (Exception ex)
            {
                db.Rollback();
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
