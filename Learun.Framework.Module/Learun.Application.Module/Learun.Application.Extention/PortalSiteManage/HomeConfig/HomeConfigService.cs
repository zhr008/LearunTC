using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Extention.PortalSiteManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-04 09:35
    /// 描 述：门户网站首页配置
    /// </summary>
    public class HomeConfigService : RepositoryFactory
    {
        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// <summary> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public IEnumerable<HomeConfigEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@" 
                t.F_Id, 
                t.F_Name 
                ");
                strSql.Append("  FROM LR_PS_HomeConfig t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数 
                var dp = new DynamicParameters(new { });
                return this.BaseRepository().FindList<HomeConfigEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取LR_PS_HomeConfig表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public HomeConfigEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<HomeConfigEntity>(keyValue);
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
        /// 获取实体根据类型
        /// </summary>
        /// <param name="type">1.顶部文字2.底部文字3.底部地址4.logo图片5.微信图片6.顶部菜单7.底部菜单8.轮播图片9.模块 10底部logo</param>
        /// <returns></returns>
        public HomeConfigEntity GetEntityByType(string type)
        {
            try
            {
                return this.BaseRepository().FindEntity<HomeConfigEntity>(t=>t.F_Type == type);
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
        /// 获取配置列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<HomeConfigEntity> GetList(string type)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@" 
                t.F_Id, 
                t.F_Name,
                t.F_Type,
                t.F_UrlType,
                t.F_Url,
                t.F_Img,
                t.F_ParentId,
                t.F_Sort,
                t.F_Scheme
                ");
                strSql.Append("  FROM LR_PS_HomeConfig t ");
                strSql.Append("  WHERE t.F_Type = @type Order by F_Sort ");
                // 虚拟参数 
                var dp = new DynamicParameters(new { });
                return this.BaseRepository().FindList<HomeConfigEntity>(strSql.ToString(), new { type });
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
        /// 获取所有的配置列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HomeConfigEntity> GetALLList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@" 
                t.F_Id, 
                t.F_Name,
                t.F_Type,
                t.F_UrlType,
                t.F_Url,
                t.F_Img,
                t.F_ParentId,
                t.F_Sort,
                t.F_Scheme
                ");
                strSql.Append("  FROM LR_PS_HomeConfig t where t.F_Type != 4 AND  t.F_Type != 5 AND t.F_Type != 10 AND t.F_Type != 9");
                strSql.Append("  Order by F_Sort ");
                // 虚拟参数 
                var dp = new DynamicParameters(new { });
                return this.BaseRepository().FindList<HomeConfigEntity>(strSql.ToString());
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
                this.BaseRepository().Delete<HomeConfigEntity>(t => t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, HomeConfigEntity entity)
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
