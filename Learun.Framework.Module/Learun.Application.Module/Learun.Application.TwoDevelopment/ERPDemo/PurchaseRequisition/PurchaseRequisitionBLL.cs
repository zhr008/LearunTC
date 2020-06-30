using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 13:15
    /// 描 述：采购申请
    /// </summary>
    public class PurchaseRequisitionBLL : PurchaseRequisitionIBLL
    {
        private PurchaseRequisitionService purchaseRequisitionService = new PurchaseRequisitionService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PurchaseRequisitionEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return purchaseRequisitionService.GetPageList(pagination, queryJson);
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
        /// 获取LR_ERP_ProductInfo表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_ProductInfoEntity> GetLR_ERP_ProductInfoList(string keyValue)
        {
            try
            {
                return purchaseRequisitionService.GetLR_ERP_ProductInfoList(keyValue);
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
        /// 获取LR_ERP_PurchaseRequisition表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PurchaseRequisitionEntity GetLR_ERP_PurchaseRequisitionEntity(string keyValue)
        {
            try
            {
                return purchaseRequisitionService.GetLR_ERP_PurchaseRequisitionEntity(keyValue);
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
        /// 获取LR_ERP_ProductInfo表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_ProductInfoEntity GetLR_ERP_ProductInfoEntity(string keyValue)
        {
            try
            {
                return purchaseRequisitionService.GetLR_ERP_ProductInfoEntity(keyValue);
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


        public IEnumerable<LR_ERP_ProductInfoEntity> GetTableDate()
        {
            try
            {
                return purchaseRequisitionService.GetTableDate();
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
                purchaseRequisitionService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_ERP_PurchaseRequisitionEntity entity,List<LR_ERP_ProductInfoEntity> lR_ERP_ProductInfoList)
        {
            try
            {
                purchaseRequisitionService.SaveEntity(keyValue, entity,lR_ERP_ProductInfoList);
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
        public IEnumerable<LR_ERP_PurchaseRequisitionEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            return purchaseRequisitionService.GetPurchasePageList(pagination, purchaseId);
        }
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            purchaseRequisitionService.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
        }
        public void UpdatePurchase(string purchaseId)
        {
            purchaseRequisitionService.UpdatePurchase(purchaseId);
        }
        #endregion

    }
}
