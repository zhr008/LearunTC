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
    /// 日 期：2019-09-25 15:03
    /// 描 述：采购入库
    /// </summary>
    public class PurchaseWarehousingService : RepositoryFactory
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PurchaseWarehousingEntity> GetPageList(Pagination pagination, string queryJson)
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
                t.F_File,
                t.F_PurchaseWarehousinger,
                t.F_PurchaseWarehousingDate,
                t.F_Order,
                t.F_Your,
                t.F_FromAddress,
                t.F_ToAddress,
                t.F_Description
                ");
                strSql.Append("  FROM LR_ERP_PurchaseWarehousing t ");
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
                return this.BaseRepository().FindList<LR_ERP_PurchaseWarehousingEntity>(strSql.ToString(),dp, pagination);
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
        /// 获取LR_ERP_PurchaseWarehousingDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PurchaseWarehousingDetailEntity> GetLR_ERP_PurchaseWarehousingDetailList(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindList<LR_ERP_PurchaseWarehousingDetailEntity>(t=>t.F_PWID == keyValue );
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
        /// 获取LR_ERP_PurchaseWarehousing表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PurchaseWarehousingEntity GetLR_ERP_PurchaseWarehousingEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_ERP_PurchaseWarehousingEntity>(keyValue);
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
        /// 获取LR_ERP_PurchaseWarehousingDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PurchaseWarehousingDetailEntity GetLR_ERP_PurchaseWarehousingDetailEntity(string keyValue)
        {
            try
            {
                return this.BaseRepository().FindEntity<LR_ERP_PurchaseWarehousingDetailEntity>(t=>t.F_PWID == keyValue);
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
        public IEnumerable<LR_ERP_PurchaseWarehousingDetailEntity> GetTableDate()
        {
            try
            {
                return this.BaseRepository().FindList<LR_ERP_PurchaseWarehousingDetailEntity>();
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
                var lR_ERP_PurchaseWarehousingEntity = GetLR_ERP_PurchaseWarehousingEntity(keyValue); 
                db.Delete<LR_ERP_PurchaseWarehousingEntity>(t=>t.F_Id == keyValue);
                db.Delete<LR_ERP_PurchaseWarehousingDetailEntity>(t=>t.F_PWID == lR_ERP_PurchaseWarehousingEntity.F_Id);
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
        public void SaveEntity(string keyValue, LR_ERP_PurchaseWarehousingEntity entity,List<LR_ERP_PurchaseWarehousingDetailEntity> lR_ERP_PurchaseWarehousingDetailList)
        {
            var db = this.BaseRepository().BeginTrans();
            LR_ERP_PurchaseWarehousingEntity lR_ERP_PurchaseWarehousingEntityTmp = new LR_ERP_PurchaseWarehousingEntity();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    lR_ERP_PurchaseWarehousingEntityTmp = GetLR_ERP_PurchaseWarehousingEntity(keyValue);
                    entity.F_ModifyDate = DateTime.Now;
                    entity.F_ModifyUserName = Learun.Util.LoginUserInfo.Get().realName;
                    entity.Modify(keyValue);
                    db.Update(entity);
                    db.Delete<LR_ERP_PurchaseWarehousingDetailEntity>(t=>t.F_PWID == lR_ERP_PurchaseWarehousingEntityTmp.F_Id);
                    foreach (LR_ERP_PurchaseWarehousingDetailEntity item in lR_ERP_PurchaseWarehousingDetailList)
                    {
                        item.Create();
                        item.F_PWID = lR_ERP_PurchaseWarehousingEntityTmp.F_Id;
                        db.Insert(item);
                    }
                }
                else
                {
                    entity.Create();
                    db.Insert(entity);
                    foreach (LR_ERP_PurchaseWarehousingDetailEntity item in lR_ERP_PurchaseWarehousingDetailList)
                    {
                        item.Create();
                        item.F_PWID = entity.F_Id;
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
            if (!string.IsNullOrEmpty(lR_ERP_PurchaseWarehousingEntityTmp.F_PurchaseNo) && !string.IsNullOrEmpty(keyValue))
                SaveStatus(keyValue, lR_ERP_PurchaseWarehousingEntityTmp);
        }
        private void SaveStatus(string keyValue, LR_ERP_PurchaseWarehousingEntity entity)
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
        public IEnumerable<LR_ERP_PurchaseWarehousingEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            try
            {
                var strSql = new StringBuilder();
                strSql.Append("SELECT t.F_Id,t.F_PurchaseNo,t.F_ApplyDate,t.F_CreateDate,t.F_ModifyDate,t.F_ModifyUserName");
                strSql.Append(" FROM LR_ERP_PurchaseWarehousing t ");
                strSql.Append(" WHERE t.F_Status = @purchaseId ");
                return this.BaseRepository().FindList<LR_ERP_PurchaseWarehousingEntity>(strSql.ToString(), new { purchaseId }, pagination);
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
                LR_ERP_PurchaseWarehousingEntity purchaseRequisitionEntity = GetLR_ERP_PurchaseWarehousingEntity(purchaseInfoId);
                purchaseRequisitionEntity.F_Status = purchaseId;
                this.BaseRepository().Update(purchaseRequisitionEntity);
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
                LR_ERP_PurchaseWarehousingEntity entity = GetLR_ERP_PurchaseWarehousingEntity(purchaseId);
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
