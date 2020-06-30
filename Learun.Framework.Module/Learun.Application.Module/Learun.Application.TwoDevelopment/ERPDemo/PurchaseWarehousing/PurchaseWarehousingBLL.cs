using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 15:03
    /// 描 述：采购入库
    /// </summary>
    public class PurchaseWarehousingBLL : PurchaseWarehousingIBLL
    {
        private PurchaseWarehousingService purchaseWarehousingService = new PurchaseWarehousingService();

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
                return purchaseWarehousingService.GetPageList(pagination, queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
                return purchaseWarehousingService.GetLR_ERP_PurchaseWarehousingDetailList(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
                return purchaseWarehousingService.GetLR_ERP_PurchaseWarehousingEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
                return purchaseWarehousingService.GetLR_ERP_PurchaseWarehousingDetailEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public IEnumerable<LR_ERP_PurchaseWarehousingDetailEntity> GetTableDate()
        {
            try
            {
                return purchaseWarehousingService.GetTableDate();
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
                purchaseWarehousingService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
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
            try
            {
                purchaseWarehousingService.SaveEntity(keyValue, entity,lR_ERP_PurchaseWarehousingDetailList);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        public IEnumerable<LR_ERP_PurchaseWarehousingEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            return purchaseWarehousingService.GetPurchasePageList(pagination, purchaseId);
        }
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            purchaseWarehousingService.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
        }
        public void UpdatePurchase(string purchaseId)
        {
            purchaseWarehousingService.UpdatePurchase(purchaseId);
        }
        #endregion

    }
}
