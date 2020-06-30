using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 16:07
    /// 描 述：任务计划模板信息
    /// </summary>
    public class TSSchemeService : RepositoryFactory
    {
        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// <summary> 
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns> 
        public IEnumerable<TSSchemeInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append(@" 
                    SELECT
	                    t.F_Id,
	                    t.F_Name,
	                    t.F_Description,
	                    p.F_State,
	                    p.F_BeginTime,
	                    p.F_EndTime,
                        p.F_Id as F_PorcessId
                    FROM
	                    LR_TS_SchemeInfo t
                    LEFT JOIN LR_TS_Process p ON p.F_SchemeInfoId = t.F_Id
                    AND p.F_State != 10
                ");
                strSql.Append("  WHERE 1=1 AND F_DeleteMark = 0 ");
                var queryParam = queryJson.ToJObject();
                string keyWord = "";
                if (!queryParam["keyWord"].IsEmpty()) {
                    keyWord ="%" + queryParam["keyWord"].ToString()+ "%";
                    strSql.Append("  AND t.F_Name like @keyWord ");
                }

                return this.BaseRepository().FindList<TSSchemeInfoEntity>(strSql.ToString(), new { keyWord }, pagination);
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
        /// 获取模板的历史数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<TSSchemeEntity> GetSchemePageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@" 
                t.F_Id, 
                t.F_SchemeInfoId,
                t.F_IsActive,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName
                ");
                strSql.Append("  FROM LR_TS_Scheme t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                return this.BaseRepository().FindList<TSSchemeEntity>(strSql.ToString(), pagination);
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
        /// 获取表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public TSSchemeInfoEntity GetSchemeInfoEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<TSSchemeInfoEntity>(keyValue);
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
        /// 获取表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public TSSchemeEntity GetSchemeEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<TSSchemeEntity>(keyValue);
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
        /// 获取表实体数据 
        /// <param name="keyValue">模板信息主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public TSSchemeEntity GetSchemeEntityByInfo(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<TSSchemeEntity>(t=>t.F_IsActive == 1 && t.F_SchemeInfoId == keyValue);
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
            TSProcessEntity entity = this.BaseRepository().FindEntity<TSProcessEntity>(t=>t.F_SchemeInfoId == keyValue && (t.F_State == 1 || t.F_State == 2) );
           var db = this.BaseRepository().BeginTrans();

            try
            {
                if (entity != null) {
                    entity.F_State = 10;
                    db.Update(entity);
                }

                TSSchemeInfoEntity tSSchemeInfoEntity = new TSSchemeInfoEntity() {
                    F_Id = keyValue,
                    F_DeleteMark = 1
                };
                db.Update(tSSchemeInfoEntity);
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
        /// <summary> 
        /// <returns></returns>
        public TSProcessEntity SaveEntity(string keyValue, TSSchemeInfoEntity schemeInfoEntity, TSSchemeEntity schemeEntity)
        {
            TSSchemeModel tSSchemeModel = schemeEntity.F_Scheme.ToObject<TSSchemeModel>();
            TSProcessEntity tSProcessEntity = null;


            TSSchemeEntity schemeEntity2 = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                schemeEntity2 = this.BaseRepository().FindEntity<TSSchemeEntity>(t=>t.F_IsActive == 1 && t.F_SchemeInfoId == keyValue);
            }
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    schemeInfoEntity.Modify(keyValue);
                    db.Update(schemeInfoEntity);

                    if (schemeEntity2 == null || schemeEntity2.F_Scheme != schemeEntity.F_Scheme) {

                        schemeEntity.Create();
                        schemeEntity.F_SchemeInfoId = schemeInfoEntity.F_Id;
                        schemeEntity.F_IsActive = 1;
                        db.Insert(schemeEntity);

                        if (schemeEntity2 != null) {
                            schemeEntity2.F_IsActive = 2;
                            db.Update(schemeEntity2);
                        }

                        // 关闭老的任务进程
                        TSProcessEntity tSProcessOldEntity = this.BaseRepository().FindEntity<TSProcessEntity>(t => t.F_SchemeInfoId == keyValue && t.F_State != 10);
                        if (tSProcessOldEntity.F_State != 3) {
                            if (tSProcessOldEntity.F_State == 1 || tSProcessOldEntity.F_State == 2)
                            {
                                tSProcessOldEntity.F_State = 10;
                                db.Update(tSProcessOldEntity);
                            }

                            // 新增一个任务进程
                            tSProcessEntity = new TSProcessEntity()
                            {
                                F_State = 1,
                                F_SchemeId = schemeEntity.F_Id,
                                F_SchemeInfoId = schemeInfoEntity.F_Id,
                                F_EndType = tSSchemeModel.endType,
                                F_EndTime = tSSchemeModel.endTime
                            };
                            tSProcessEntity.Create();

                            if (tSSchemeModel.startType == 1)
                            {
                                tSProcessEntity.F_BeginTime = DateTime.Now;
                            }
                            else
                            {
                                tSProcessEntity.F_BeginTime = tSSchemeModel.startTime;
                            }

                            if (tSSchemeModel.endType == 1)
                            {
                                tSProcessEntity.F_EndTime = DateTime.MaxValue;
                            }

                            db.Insert(tSProcessEntity);
                        }
                    }
                }
                else
                {
                    schemeInfoEntity.Create();
                    db.Insert(schemeInfoEntity);

                    schemeEntity.Create();
                    schemeEntity.F_SchemeInfoId = schemeInfoEntity.F_Id;
                    schemeEntity.F_IsActive = 1;
                    db.Insert(schemeEntity);

                    // 新增一个任务进程
                    tSProcessEntity = new TSProcessEntity() {
                        F_State = 1,
                        F_SchemeId = schemeEntity.F_Id,
                        F_SchemeInfoId = schemeInfoEntity.F_Id,
                        F_EndType = tSSchemeModel.endType,
                        F_EndTime = tSSchemeModel.endTime
                    };
                    tSProcessEntity.Create();

                    if (tSSchemeModel.startType == 1)
                    {
                        tSProcessEntity.F_BeginTime = DateTime.Now;
                    }
                    else {
                        tSProcessEntity.F_BeginTime = tSSchemeModel.startTime;
                    }

                    if (tSSchemeModel.endType == 1) {
                        tSProcessEntity.F_EndTime = DateTime.MaxValue;
                    }

                    db.Insert(tSProcessEntity);
                }

                db.Commit();

                return tSProcessEntity;
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
        /// <summary> 
        /// <returns></returns>
        public void SaveEntity(string keyValue, TSSchemeInfoEntity entity)
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
