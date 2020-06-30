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
    /// 日 期：2019-09-25 17:55
    /// 描 述：销售订单
    /// </summary>
    public class SalesOrderService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_SalesOrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT ");
                strSql.Append(@"
                t.F_Id,
                t.F_PurchaseNo,
                t.F_ApplyDate,
                t.F_Theme,
                t.F_PurchaseType,
                t.F_Appler,
                t.F_Department,
                t.F_PaymentType,
                t.F_Total,
                t.F_DeliveryDate,
                t.F_Your,
                t.F_Description,
                t.F_File
                ");
                strSql.Append("  FROM LR_ERP_SalesOrder t ");
                strSql.Append("  WHERE (t.F_Status is null or F_Status='') ");
                var queryParam = queryJson.ToJObject();
                // 虚拟参数
                var dp = new DynamicParameters(new { });
                if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
                {
                    dp.Add("startTime", queryParam["StartTime"].ToDate(), DbType.DateTime);
                    dp.Add("endTime", queryParam["EndTime"].ToDate(), DbType.DateTime);
                    strSql.Append(" AND ( t.F_ApplyDate >= @startTime AND t.F_ApplyDate <= @endTime ) ");
                }
                if (!queryParam["F_PurchaseNo"].IsEmpty())
                {
                    //dp.Add("F_PurchaseNo", "%" + queryParam["F_PurchaseNo"].ToString() + "%", DbType.String);
                    dp.Add("F_PurchaseNo", queryParam["F_PurchaseNo"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PurchaseNo = @F_PurchaseNo ");
                }
                if (!queryParam["F_PurchaseType"].IsEmpty())
                {
                    dp.Add("F_PurchaseType", queryParam["F_PurchaseType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PurchaseType = @F_PurchaseType ");
                }
                if (!queryParam["F_Appler"].IsEmpty())
                {
                    dp.Add("F_Appler", queryParam["F_Appler"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Appler = @F_Appler ");
                }
                if (!queryParam["F_Department"].IsEmpty())
                {
                    dp.Add("F_Department", queryParam["F_Department"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Department = @F_Department ");
                }
                if (!queryParam["F_Theme"].IsEmpty())
                {
                    dp.Add("F_Theme", queryParam["F_Theme"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Theme = @F_Theme ");
                }
                return this.BaseRepository().FindList<LR_ERP_SalesOrderEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取LR_ERP_SalesOrderDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_SalesOrderDetailEntity> GetLR_ERP_SalesOrderDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<LR_ERP_SalesOrderDetailEntity>(t=>t.F_SOID == keyValue );
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
        /// 获取LR_ERP_SalesOrder表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_SalesOrderEntity GetLR_ERP_SalesOrderEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_ERP_SalesOrderEntity>(keyValue);
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
        /// 获取LR_ERP_SalesOrderDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_SalesOrderDetailEntity GetLR_ERP_SalesOrderDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_ERP_SalesOrderDetailEntity>(t=>t.F_SOID == keyValue);
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
        public IEnumerable<LR_ERP_SalesOrderDetailEntity> GetTableDate()
        {
            try
            {
                return this.BaseRepository().FindList<LR_ERP_SalesOrderDetailEntity>();
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
                var lR_ERP_SalesOrderEntity = GetLR_ERP_SalesOrderEntity(keyValue); 
                db.Delete<LR_ERP_SalesOrderEntity>(t=>t.F_Id == keyValue);
                db.Delete<LR_ERP_SalesOrderDetailEntity>(t=>t.F_SOID == lR_ERP_SalesOrderEntity.F_Id);
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
        public void SaveEntity(string keyValue, LR_ERP_SalesOrderEntity entity,List<LR_ERP_SalesOrderDetailEntity> lR_ERP_SalesOrderDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            LR_ERP_SalesOrderEntity lR_ERP_SalesOrderEntityTmp = new LR_ERP_SalesOrderEntity();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    lR_ERP_SalesOrderEntityTmp = GetLR_ERP_SalesOrderEntity(keyValue);
                    entity.F_ModifyDate = DateTime.Now;
                    entity.F_ModifyUserName = Learun.Util.LoginUserInfo.Get().realName;
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<LR_ERP_SalesOrderDetailEntity>(t=>t.F_SOID == lR_ERP_SalesOrderEntityTmp.F_Id);
                    foreach (LR_ERP_SalesOrderDetailEntity item in lR_ERP_SalesOrderDetailList)
                    {
                        item.Create();
                        item.F_SOID = lR_ERP_SalesOrderEntityTmp.F_Id;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (LR_ERP_SalesOrderDetailEntity item in lR_ERP_SalesOrderDetailList)
                    {
                        item.Create();
                        item.F_SOID = entity.F_Id;
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
            if (!string.IsNullOrEmpty(lR_ERP_SalesOrderEntityTmp.F_PurchaseNo) && !string.IsNullOrEmpty(keyValue))
                SaveStatus(keyValue, lR_ERP_SalesOrderEntityTmp);
        }
        private void SaveStatus(string keyValue, LR_ERP_SalesOrderEntity entity)
        {
            var db = this.BaseRepository().BeginTrans();
            try
            {
                entity.F_ModifyDate = DateTime.Now;
                entity.F_ModifyUserName = Learun.Util.LoginUserInfo.Get().realName;
                entity.F_Id = Guid.NewGuid().ToString();
                entity.F_Status = keyValue;
                db.Insert(entity);
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
        public IEnumerable<LR_ERP_SalesOrderEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT t.F_Id,t.F_PurchaseNo,t.F_ApplyDate,t.F_CreateDate,t.F_ModifyDate,t.F_ModifyUserName");
                strSql.Append(" FROM LR_ERP_SalesOrder t ");
                strSql.Append(" WHERE t.F_Status = @purchaseId ");
                return this.BaseRepository().FindList<LR_ERP_SalesOrderEntity>(strSql.ToString(), new { purchaseId }, pagination);
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
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            try
            {
                LR_ERP_SalesOrderEntity PurchaseRequisitionEntity = GetLR_ERP_SalesOrderEntity(purchaseInfoId);
                PurchaseRequisitionEntity.F_Status = purchaseId;
                this.BaseRepository().Update(PurchaseRequisitionEntity);
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
        public void UpdatePurchase(string purchaseId)
        {
            try
            {
                LR_ERP_SalesOrderEntity entity = GetLR_ERP_SalesOrderEntity(purchaseId);
                entity.F_Status = String.Empty;
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
