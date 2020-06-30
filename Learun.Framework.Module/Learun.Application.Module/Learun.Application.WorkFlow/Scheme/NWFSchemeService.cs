using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流模板(新)
    /// </summary>
    public class NWFSchemeService: RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 获取流程分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询条件</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeInfoEntity> GetInfoPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT t.* ");
                strSql.Append(",t1.F_Type,t1.F_CreateDate,t1.F_CreateUserId,t1.F_CreateUserName ");
                strSql.Append(" FROM LR_NWF_SchemeInfo t LEFT JOIN LR_NWF_Scheme t1 ON t.F_SchemeId = t1.F_Id WHERE 1=1 ");

                var dp = new DynamicParameters();
                if (!string.IsNullOrEmpty(queryJson)) {
                    var queryParam = queryJson.ToJObject();
                    if (!queryParam["keyword"].IsEmpty())
                    {
                        strSql.Append(" AND ( t.F_Name like @keyword OR t.F_Code like @keyword ) ");
                        dp.Add("keyword", "%" + queryParam["keyword"].ToString() + "%", DbType.String);
                    }
                    if (!queryParam["category"].IsEmpty())
                    {
                        strSql.Append(" AND t.F_Category = @category ");
                        dp.Add("category", queryParam["category"].ToString(), DbType.String);
                    }
                }
                return this.BaseRepository().FindList<NWFSchemeInfoEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取自定义流程列表
        /// </summary>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeInfoEntity> GetInfoList(UserInfo userInfo)
        {
            try
            {
                string userId = userInfo.userId;
                string postIds = userInfo.postIds;
                string roleIds = userInfo.roleIds;
                List<NWFSchemeAuthEntity> list = (List<NWFSchemeAuthEntity>)this.BaseRepository().FindList<NWFSchemeAuthEntity>(t => t.F_ObjId == null
                    || userId.Contains(t.F_ObjId)
                    || postIds.Contains(t.F_ObjId)
                    || roleIds.Contains(t.F_ObjId)
                    );
                string schemeinfoIds = "";
                foreach (var item in list)
                {
                    schemeinfoIds += "'" + item.F_SchemeInfoId + "',";
                }
                schemeinfoIds = "(" + schemeinfoIds.Remove(schemeinfoIds.Length - 1, 1) + ")";

                var strSql = new StringBuilder();
                strSql.Append("SELECT * ");
                strSql.Append(" FROM LR_NWF_SchemeInfo t WHERE t.F_EnabledMark = 1  AND t.F_Mark = 1 AND t.F_Id in " + schemeinfoIds);

                return this.BaseRepository().FindList<NWFSchemeInfoEntity>(strSql.ToString());
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
        /// 获取流程列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NWFSchemeInfoEntity> GetInfoList()
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT * ");
                strSql.Append(" FROM LR_NWF_SchemeInfo t WHERE t.F_EnabledMark = 1 ");
                return this.BaseRepository().FindList<NWFSchemeInfoEntity>(strSql.ToString());
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
        /// 获取流程模板分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userInfo">登录者信息</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeInfoEntity> GetAppInfoPageList(Pagination pagination, UserInfo userInfo, string queryJson)
        {
            try
            {
                string userId = userInfo.userId;
                string postIds = userInfo.postIds;
                string roleIds = userInfo.roleIds;
                List<NWFSchemeAuthEntity> list = (List<NWFSchemeAuthEntity>)this.BaseRepository().FindList<NWFSchemeAuthEntity>(t => t.F_ObjId == null
                    || userId.Contains(t.F_ObjId)
                    || postIds.Contains(t.F_ObjId)
                    || roleIds.Contains(t.F_ObjId)
                    );
                string schemeinfoIds = "";
                foreach (var item in list)
                {
                    schemeinfoIds += "'" + item.F_SchemeInfoId + "',";
                }
                schemeinfoIds = "(" + schemeinfoIds.Remove(schemeinfoIds.Length - 1, 1) + ")";


                var strSql = new StringBuilder();
                strSql.Append("SELECT * ");
                strSql.Append(" FROM LR_NWF_SchemeInfo t WHERE t.F_EnabledMark = 1  AND t.F_Mark = 1 AND F_IsInApp = 1 AND t.F_Id in " + schemeinfoIds);

                var queryParam = queryJson.ToJObject();
                string keyword = "";
                if (!queryParam["keyword"].IsEmpty())
                {
                    strSql.Append(" AND ( t.F_Name like @keyword OR t.F_Code like @keyword ) ");
                    keyword = "%" + queryParam["keyword"].ToString() + "%";
                }
                return this.BaseRepository().FindList<NWFSchemeInfoEntity>(strSql.ToString(), new { keyword }, pagination);
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
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public NWFSchemeInfoEntity GetInfoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<NWFSchemeInfoEntity>(keyValue);
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
        /// 获取模板基础信息的实体
        /// </summary>
        /// <param name="code">流程编号</param>
        /// <returns></returns>
        public NWFSchemeInfoEntity GetInfoEntityByCode(string code)
        {
            try
            {
                return this.BaseRepository().FindEntity<NWFSchemeInfoEntity>(t => t.F_Code == code);
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
        /// 获取流程模板权限列表
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeAuthEntity> GetAuthList(string schemeInfoId)
        {
            try
            {
                return this.BaseRepository().FindList<NWFSchemeAuthEntity>(t => t.F_SchemeInfoId == schemeInfoId);
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
        /// 获取模板列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="schemeInfoId">流程信息主键</param>
        /// <returns></returns>
        public IEnumerable<NWFSchemeEntity> GetSchemePageList(Pagination pagination, string schemeInfoId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT t.F_Id,t.F_SchemeInfoId,t.F_Type,t.F_CreateDate,t.F_CreateUserId,t.F_CreateUserName");
                strSql.Append(" FROM LR_NWF_Scheme t ");
                strSql.Append(" WHERE t.F_SchemeInfoId = @schemeInfoId ");
                return this.BaseRepository().FindList<NWFSchemeEntity>(strSql.ToString(), new { schemeInfoId }, pagination);
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
        /// 获取模板的实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public NWFSchemeEntity GetSchemeEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<NWFSchemeEntity>(keyValue);
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
        /// 虚拟删除模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                db.Delete<NWFSchemeInfoEntity>(t => t.F_Id.Equals(keyValue));
                db.Delete<NWFSchemeAuthEntity>(t => t.F_SchemeInfoId.Equals(keyValue));
                db.Delete<NWFSchemeEntity>(t => t.F_SchemeInfoId.Equals(keyValue));
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
        /// 保存模板信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="infoEntity">模板基础信息</param>
        /// <param name="schemeEntity">模板信息</param>
        /// <param name="authList">模板权限信息</param>
        public void SaveEntity(string keyValue, NWFSchemeInfoEntity infoEntity, NWFSchemeEntity schemeEntity, List<NWFSchemeAuthEntity> authList)
        {
            IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
            try
            {
                if (string.IsNullOrEmpty(keyValue))
                {
                    infoEntity.Create();
                }
                else
                {
                    infoEntity.Modify(keyValue);
                }

                #region 模板信息
                if (schemeEntity != null)
                {
                    schemeEntity.F_SchemeInfoId = infoEntity.F_Id;
                    schemeEntity.Create();
                    db.Insert(schemeEntity);
                    infoEntity.F_SchemeId = schemeEntity.F_Id;
                }
                #endregion

                #region 模板基础信息
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(infoEntity);
                }
                else
                {
                    db.Insert(infoEntity);
                }
                #endregion

                #region 流程模板权限信息
                string schemeInfoId = infoEntity.F_Id;
                db.Delete<NWFSchemeAuthEntity>(t => t.F_SchemeInfoId == schemeInfoId);
                foreach (var item in authList)
                {
                    item.Create();
                    item.F_SchemeInfoId = schemeInfoId;
                    db.Insert(item);
                }
                #endregion

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
        /// 更新流程模板
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="schemeId">模板主键</param>
        public void UpdateScheme(string schemeInfoId, string schemeId)
        {
            try
            {
                NWFSchemeEntity nWFSchemeEntity = GetSchemeEntity(schemeId);

                NWFSchemeInfoEntity entity = new NWFSchemeInfoEntity
                {
                    F_Id = schemeInfoId,
                    F_SchemeId = schemeId
                };
                if (nWFSchemeEntity.F_Type != 1)
                {
                    entity.F_EnabledMark = 0;
                }
                this.BaseRepository().Update(entity);
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
        /// 更新自定义表单模板状态
        /// </summary>
        /// <param name="schemeInfoId">模板信息主键</param>
        /// <param name="state">状态1启用0禁用</param>
        public void UpdateState(string schemeInfoId, int state)
        {
            try
            {
                NWFSchemeInfoEntity entity = new NWFSchemeInfoEntity
                {
                    F_Id = schemeInfoId,
                    F_EnabledMark = state
                };
                this.BaseRepository().Update(entity);
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
