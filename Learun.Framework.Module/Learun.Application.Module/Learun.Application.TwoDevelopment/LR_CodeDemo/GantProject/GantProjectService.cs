using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-05-08 18:30
    /// 描 述：甘特图应用
    /// </summary>
    public class GantProjectService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_OA_ProjectEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_ProjectName,
                t.F_StartTime,
                t.F_EndTime,
                t.F_Remark,
                t.F_Status
                ");
                strSql.Append("  FROM LR_OA_Project t ");
                strSql.Append("  WHERE 1=1 ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["F_ProjectName"].IsEmpty())
                {
                    dp.Add("F_ProjectName", "%" + queryParam["F_ProjectName"].ToString() + "%", DbType.String);
                    strSql.Append(" AND t.F_ProjectName Like @F_ProjectName ");
                }
                return this.BaseRepository().FindList<LR_OA_ProjectEntity>(strSql.ToString(), dp, pagination);
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
        /// 获取LR_OA_ProjectDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_OA_ProjectDetailEntity> GetLR_OA_ProjectDetailList(string parentId)
        {
            try
            {
                return this.BaseRepository().FindList<LR_OA_ProjectDetailEntity>("select * from LR_OA_ProjectDetail where F_ParentId='" + parentId + "' order by F_StartTime");
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
        /// 获取LR_OA_Project表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_OA_ProjectEntity GetLR_OA_ProjectEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_OA_ProjectEntity>(keyValue);
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
        /// 获取LR_OA_ProjectDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_OA_ProjectDetailEntity GetLR_OA_ProjectDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_OA_ProjectDetailEntity>(t => t.F_Id == keyValue);
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
        /// 获取项目列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<LR_OA_ProjectEntity> GetList(string keyValue)
        {
            if (string.IsNullOrEmpty(keyValue))
            {
                return this.BaseRepository().FindList<LR_OA_ProjectEntity>();
            }
            else
            {
                return this.BaseRepository().FindList<LR_OA_ProjectEntity>(t => t.F_ProjectName.Contains(keyValue));
            }
        }
        /// <summary>
        /// 获取项目明细列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<LR_OA_ProjectDetailEntity> GetDetailList(string parentId)
        {
            return this.BaseRepository().FindList<LR_OA_ProjectDetailEntity>(t => t.F_ParentId == parentId);
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
                var lR_OA_ProjectEntity = GetLR_OA_ProjectEntity(keyValue);
                db.Delete<LR_OA_ProjectEntity>(t => t.F_Id == keyValue);
                db.Delete<LR_OA_ProjectDetailEntity>(t => t.F_ParentId == lR_OA_ProjectEntity.F_Id);
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
        /// 删除明细数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteDetail(string keyValue)
        {
            try
            {
                this.BaseRepository().Delete<LR_OA_ProjectDetailEntity>(t => t.F_Id == keyValue);
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
        public void SaveEntity(string keyValue, LR_OA_ProjectEntity entity, List<LR_OA_ProjectDetailEntity> lR_OA_ProjectDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var lR_OA_ProjectEntityTmp = GetLR_OA_ProjectEntity(keyValue);
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<LR_OA_ProjectDetailEntity>(t => t.F_ParentId == lR_OA_ProjectEntityTmp.F_Id);
                    foreach (LR_OA_ProjectDetailEntity item in lR_OA_ProjectDetailList)
                    {
                        item.Create();
                        item.F_ParentId = lR_OA_ProjectEntityTmp.F_Id;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (LR_OA_ProjectDetailEntity item in lR_OA_ProjectDetailList)
                    {
                        item.Create();
                        item.F_ParentId = entity.F_Id;
                        db.Insert(item);
                    }
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
        /// 保存表头实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveGant(string keyValue, LR_OA_ProjectEntity entity)
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
        /// <summary>
        /// 保存明细实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveDetail(string keyValue, LR_OA_ProjectDetailEntity entity)
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
