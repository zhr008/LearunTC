using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Extention.DisplayBoardManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 10:08
    /// 描 述：看板发布
    /// </summary>
    public class LR_KBFeaManageService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public LR_KBFeaManageService()
        {
            fieldSql= @"
                t.F_Id,
                t.F_FullName,
                t.F_EnCode,
                t.F_ParentId,
                t.F_Icon,
                t.F_KanBanId,
                t.F_ModuleId,
                t.F_SortCode,
                t.F_Description
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_KBFeaManageEntity> GetList( string queryJson )
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
                strSql.Append(" FROM LR_KBFeaManage t ");
                return this.BaseRepository().FindList<LR_KBFeaManageEntity>(strSql.ToString());
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
        public IEnumerable<LR_KBFeaManageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                                t.F_Id,
                                t.F_FullName,
                                t.F_EnCode,
                                (select F_FullName from LR_Base_Module where F_ModuleId=t.F_ParentId) as F_ParentId,
                                t.F_Icon,
                                (select F_KanBanName from LR_KBKanBanInfo where F_Id=t.F_KanBanId) as F_KanBanId,
                                t.F_ModuleId,
                                t.F_SortCode,
                                t.F_Description
                              ");
                strSql.Append(" FROM LR_KBFeaManage t ");
                return this.BaseRepository().FindList<LR_KBFeaManageEntity>(strSql.ToString(), pagination);
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
        public LR_KBFeaManageEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_KBFeaManageEntity>(keyValue);
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
                this.BaseRepository().Delete<LR_KBFeaManageEntity>(t=>t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, LR_KBFeaManageEntity entity)
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
