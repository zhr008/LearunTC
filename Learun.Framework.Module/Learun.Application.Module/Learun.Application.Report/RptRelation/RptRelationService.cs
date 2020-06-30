using Dapper;
using Learun.Application.Base.SystemModule;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.Report
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-03-26 18:29
    /// 描 述：报表菜单关联设置
    /// </summary>
    public class RptRelationService : RepositoryFactory
    {
        private ModuleIBLL moduleIBLL = new ModuleBLL();
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_RPT_RelationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_EnCode,
                t.F_FullName,
                t.F_ParentId,
                t.F_Icon,
                t.F_SortCode,
                t.F_RptFileId,
                t.F_Description
                ");
                strSql.Append("  FROM LR_RptRelation t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_FullName"].IsEmpty())
                {
                    dp.Add("F_FullName", "%" + queryParam["F_FullName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_FullName Like @F_FullName ");
                }
                if (!queryParam["F_ModifyUserName"].IsEmpty())
                {
                    dp.Add("F_ModifyUserName", queryParam["F_ModifyUserName"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_ModifyUserName = @F_ModifyUserName ");
                }
                return this.BaseRepository().FindList<LR_RPT_RelationEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取LR_RptRelation表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_RPT_RelationEntity GetLR_RptRelationEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_RPT_RelationEntity>(keyValue);
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
                //删除报表关系同时把菜单也删除
                LR_RPT_RelationEntity relation = this.BaseRepository().FindEntity<LR_RPT_RelationEntity>(keyValue);
                string moduleCode = relation.F_EnCode;
                var module = this.BaseRepository().FindList<ModuleEntity>(t => t.F_EnCode == moduleCode);
                this.BaseRepository().Delete(module);
                this.BaseRepository().Delete(relation);
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
        public void SaveEntity(string keyValue, LR_RPT_RelationEntity entity)
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
                    ModuleEntity moduleEntity = new ModuleEntity();
                    moduleEntity.F_IsMenu = 1;
                    moduleEntity.F_EnabledMark = 1;
                    moduleEntity.F_EnCode = entity.F_EnCode;
                    moduleEntity.F_FullName = entity.F_FullName;
                    moduleEntity.F_ParentId = entity.F_ParentId;
                    moduleEntity.F_Icon = entity.F_Icon;
                    moduleEntity.F_Description = entity.F_Description;
                    moduleEntity.F_Target = "iframe";
                    moduleEntity.F_UrlAddress = "/LR_ReportModule/RptManage/Report?reportId=" + entity.F_RptUrl;
                    List<ModuleButtonEntity> moduleButtonList = new List<ModuleButtonEntity>();
                    List<ModuleColumnEntity> moduleColumnList = new List<ModuleColumnEntity>();
                    List<ModuleFormEntity> moduleFormEntitys = new List<ModuleFormEntity>();
                    moduleIBLL.SaveEntity(null, moduleEntity, moduleButtonList, moduleColumnList, moduleFormEntitys);
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
