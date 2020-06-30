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
    /// 日 期：2019-09-25 09:37
    /// 描 述：采购订单
    /// </summary>
    public class PurchaseOrderService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PurchaseOrderEntity> GetPageList(Pagination pagination, string queryJson)
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
                t.F_Purchaser,
                t.F_Total,
                t.F_PaymentType,
                t.F_DeliveryDate,
                t.F_Your,
                t.F_Description,
                t.F_File
                ");
                strSql.Append("  FROM LR_ERP_PurchaseOrder t ");
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
                    dp.Add("F_PurchaseType",queryParam["F_PurchaseType"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_PurchaseType = @F_PurchaseType ");
                }
                if (!queryParam["F_Appler"].IsEmpty())
                {
                    dp.Add("F_Appler",queryParam["F_Appler"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Appler = @F_Appler ");
                }
                if (!queryParam["F_Department"].IsEmpty())
                {
                    dp.Add("F_Department",queryParam["F_Department"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Department = @F_Department ");
                }
                if (!queryParam["F_Theme"].IsEmpty())
                {
                    dp.Add("F_Theme", queryParam["F_Theme"].ToString(), DbType.String);
                    strSql.Append(" AND t.F_Theme = @F_Theme ");
                }
                return this.BaseRepository().FindList<LR_ERP_PurchaseOrderEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取LR_ERP_PurchaseOrderDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PurchaseOrderDetailEntity> GetLR_ERP_PurchaseOrderDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<LR_ERP_PurchaseOrderDetailEntity>(t=>t.F_POID == keyValue );
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
        /// 获取LR_ERP_PurchaseOrder表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PurchaseOrderEntity GetLR_ERP_PurchaseOrderEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_ERP_PurchaseOrderEntity>(keyValue);
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
        /// 获取LR_ERP_PurchaseOrderDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PurchaseOrderDetailEntity GetLR_ERP_PurchaseOrderDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_ERP_PurchaseOrderDetailEntity>(t=>t.F_POID == keyValue);
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
        /// 获取获取当前表字段
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public DataTable GetTableField()
        {
            try
            {
                //return this.BaseRepository().FindTable("SELECT C.value AS column_description FROM sys.tables A INNER JOIN sys.columns B ON B.object_id = A.object_id LEFT JOIN sys.extended_properties C ON C.major_id = B.object_id AND C.minor_id = B.column_id WHERE A.name = 'LR_ERP_PurchaseOrderDetail' and (B.name = 'F_Code' or B.name = 'F_Name' or B.name = 'F_Count'or B.name = 'F_Price')");
                return this.BaseRepository().FindTable("SELECT B.name AS column_name FROM sys.tables AS A INNER JOIN sys.columns AS B ON B.object_id = A.object_id LEFT OUTER JOIN sys.extended_properties AS C ON C.major_id = B.object_id AND C.minor_id = B.column_id WHERE (A.name = 'LR_ERP_PurchaseOrderDetail' and (B.name = 'F_Code' or B.name = 'F_Name' or B.name = 'F_Count'or B.name = 'F_Price'))");
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
        /// 获取获取当前表所有数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PurchaseOrderDetailEntity> GetTableDate()
        {
            try
            {
                return this.BaseRepository().FindList<LR_ERP_PurchaseOrderDetailEntity>();
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
                var lR_ERP_PurchaseOrderEntity = GetLR_ERP_PurchaseOrderEntity(keyValue); 
                db.Delete<LR_ERP_PurchaseOrderEntity>(t=>t.F_Id == keyValue);
                db.Delete<LR_ERP_PurchaseOrderDetailEntity>(t=>t.F_POID == lR_ERP_PurchaseOrderEntity.F_Id);
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
        public void SaveEntity(string keyValue, LR_ERP_PurchaseOrderEntity entity,List<LR_ERP_PurchaseOrderDetailEntity> lR_ERP_PurchaseOrderDetailList)
        {            
            var db = this.BaseRepository().BeginTrans();
            LR_ERP_PurchaseOrderEntity lR_ERP_PurchaseOrderEntityTmp = new LR_ERP_PurchaseOrderEntity();
            try
             {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    lR_ERP_PurchaseOrderEntityTmp = GetLR_ERP_PurchaseOrderEntity(keyValue);
                    entity.F_ModifyDate = DateTime.Now;
                    entity.F_ModifyUserName = Learun.Util.LoginUserInfo.Get().realName;
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<LR_ERP_PurchaseOrderDetailEntity>(t=>t.F_POID == lR_ERP_PurchaseOrderEntityTmp.F_Id);
                    foreach (LR_ERP_PurchaseOrderDetailEntity item in lR_ERP_PurchaseOrderDetailList)
                    {
                        item.Create();
                        item.F_POID = lR_ERP_PurchaseOrderEntityTmp.F_Id;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (LR_ERP_PurchaseOrderDetailEntity item in lR_ERP_PurchaseOrderDetailList)
                    {
                        item.Create();
                        item.F_POID = entity.F_Id;
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

            if (!string.IsNullOrEmpty(lR_ERP_PurchaseOrderEntityTmp.F_PurchaseNo) && !string.IsNullOrEmpty(keyValue))
                SaveStatus(keyValue, lR_ERP_PurchaseOrderEntityTmp);
        }
        private void SaveStatus(string keyValue, LR_ERP_PurchaseOrderEntity entity)
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
        public IEnumerable<LR_ERP_PurchaseOrderEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT t.F_Id,t.F_PurchaseNo,t.F_ApplyDate,t.F_CreateDate,t.F_ModifyDate,t.F_ModifyUserName");
                strSql.Append(" FROM LR_ERP_PurchaseOrder t ");
                strSql.Append(" WHERE t.F_Status = @purchaseId ");
                return this.BaseRepository().FindList<LR_ERP_PurchaseOrderEntity>(strSql.ToString(), new { purchaseId }, pagination);
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
                LR_ERP_PurchaseOrderEntity purchaseOrderEntity = GetLR_ERP_PurchaseOrderEntity(purchaseInfoId);
                purchaseOrderEntity.F_Status = purchaseId;
                this.BaseRepository().Update(purchaseOrderEntity);
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
                LR_ERP_PurchaseOrderEntity entity = GetLR_ERP_PurchaseOrderEntity(purchaseId);
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
