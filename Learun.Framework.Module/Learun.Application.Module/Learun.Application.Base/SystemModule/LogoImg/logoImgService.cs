using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Base.SystemModule
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2018-07-30 14:53 
    /// 描 述：logo设置 
    /// </summary> 
    public class LogoImgService : RepositoryFactory
    {
        #region 构造函数和属性 

        private string fieldSql;
        /// <summary>
        /// 
        /// </summary>
        public LogoImgService()
        {
            fieldSql = @" 
                t.F_Code, 
                t.F_FileName 
            ";
        }
        #endregion

        #region 获取数据 

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        public IEnumerable<LogoImgEntity> GetList(string queryJson)
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
                strSql.Append(" FROM LR_Base_Logo t ");
                return this.BaseRepository().FindList<LogoImgEntity>(strSql.ToString());
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
        public LogoImgEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LogoImgEntity>(keyValue);
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
                this.BaseRepository().Delete<LogoImgEntity>(t => t.F_Code == keyValue);
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
        /// <param name="entity">实体</param> 
        public void SaveEntity(LogoImgEntity entity)
        {
            try
            {
                LogoImgEntity entityTmp = this.BaseRepository().FindEntity<LogoImgEntity>(entity.F_Code);
                if (entityTmp != null)
                {
                    entity.Modify(entity.F_Code);
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
