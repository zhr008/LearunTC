using Dapper;
using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Learun.Application.Extention.DisplayBoardManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 10:10
    /// 描 述：看板信息
    /// </summary>
    public class LR_KBKanBanInfoService : RepositoryFactory
    {
        #region 构造函数和属性

        private string fieldSql;
        public LR_KBKanBanInfoService()
        {
            fieldSql= @"
                t.F_Id,
                t.F_KanBanName,
                t.F_KanBanCode,
                t.F_RefreshTime,
                t.F_TemplateId,
                t.F_Description,
                t.F_CreateDate,
                t.F_CreateUserId,
                t.F_CreateUserName,
                t.F_ModifyDate,
                t.F_ModifyUserId,
                t.F_ModifyUserName
            ";
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_KBKanBanInfoEntity> GetList( string queryJson )
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
                strSql.Append(" FROM LR_KBKanBanInfo t ");
                return this.BaseRepository().FindList<LR_KBKanBanInfoEntity>(strSql.ToString());
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
        public IEnumerable<LR_KBKanBanInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(fieldSql);
                strSql.Append(" FROM LR_KBKanBanInfo t ");
                return this.BaseRepository().FindList<LR_KBKanBanInfoEntity>(strSql.ToString(), pagination);
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
        public LR_KBKanBanInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_KBKanBanInfoEntity>(keyValue);
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
            var db = this.BaseRepository("BaseDb").BeginTrans();
            try
            {
                db.Delete<LR_KBKanBanInfoEntity>(t=>t.F_Id == keyValue);
                db.Delete<LR_KBConfigInfoEntity>(t => t.F_KanBanId == keyValue);
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
        public void SaveEntity(string keyValue, string kanbaninfo, string kbconfigInfo)
        {
            var db = this.BaseRepository("BaseDb").BeginTrans();
            try
            {
                LR_KBKanBanInfoEntity lR_KBKanBanInfoEntity = kanbaninfo.ToObject<LR_KBKanBanInfoEntity>();
                List<LR_KBConfigInfoEntity> list = kbconfigInfo.ToObject<List<LR_KBConfigInfoEntity>>();
                /////新增编辑看板信息
                if (string.IsNullOrEmpty(keyValue))
                {
                    lR_KBKanBanInfoEntity.Create();
                    db.Insert<LR_KBKanBanInfoEntity>(lR_KBKanBanInfoEntity);
                }
                else
                {
                    lR_KBKanBanInfoEntity.Modify(keyValue);
                    db.Update<LR_KBKanBanInfoEntity>(lR_KBKanBanInfoEntity);
                }
                if (list.Count == 0)
                {
                    db.Delete<LR_KBConfigInfoEntity>(t => t.F_KanBanId == keyValue);
                }
                ///处理看板配置信息
                foreach(var item in list)
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        db.Delete<LR_KBConfigInfoEntity>(t => t.F_KanBanId == keyValue);//编辑中先删除原来的配置信息
                    }
                    item.Create();
                    item.F_KanBanId = lR_KBKanBanInfoEntity.F_Id;
                    db.Insert<LR_KBConfigInfoEntity>(item);
                }
                db.Commit();//事务提交
            }
            catch (Exception ex)
            {
                db.Rollback();//事务回滚
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
