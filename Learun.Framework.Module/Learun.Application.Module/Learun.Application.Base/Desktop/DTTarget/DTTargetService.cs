using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Base.Desktop
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-05-28 16:08
    /// 描 述：桌面统计数据配置
    /// </summary>
    public class DTTargetService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public DTTargetService()
        {
            fieldSql = @"
                t.F_Id,
                t.F_Name,
                t.F_Icon,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_CreateDate,
                t.F_Description,
                t.F_Url,
                t.F_Sort,
                t.F_DataSourceId
            ";
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DTTargetEntity> GetList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_DT_Target t Order by t.F_Sort ");
                return this.BaseRepository().FindList<DTTargetEntity>(strSql.ToString());
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
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<DTTargetEntity> GetList(string keyword)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql );
                strSql.Append(" ,t.F_Sql FROM LR_DT_Target t where 1=1 ");

                if (!string.IsNullOrEmpty(keyword)) {
                    keyword = "%" + keyword + "%";
                    strSql.Append(" t.F_Name like @keyword ");
                }

                strSql.Append(" Order by t.F_Sort ");
                
                return this.BaseRepository().FindList<DTTargetEntity>(strSql.ToString(),new { keyword });
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public DTTargetEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<DTTargetEntity>(keyValue);
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
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<DTTargetEntity>(t => t.F_Id == keyValue);
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
        /// <returns></returns>
        public void SaveEntity(string keyValue, DTTargetEntity entity)
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
