using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 09:37
    /// 描 述：采购订单
    /// </summary>
    public class PurchaseOrderBLL : PurchaseOrderIBLL
    {
        private PurchaseOrderService purchaseOrderService = new PurchaseOrderService();

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
                return purchaseOrderService.GetPageList(pagination, queryJson);
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
        /// 获取LR_ERP_PurchaseOrderDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PurchaseOrderDetailEntity> GetLR_ERP_PurchaseOrderDetailList(string keyValue)
        {
            try
            {
                return purchaseOrderService.GetLR_ERP_PurchaseOrderDetailList(keyValue);
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
        /// 获取LR_ERP_PurchaseOrder表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PurchaseOrderEntity GetLR_ERP_PurchaseOrderEntity(string keyValue)
        {
            try
            {
                return purchaseOrderService.GetLR_ERP_PurchaseOrderEntity(keyValue);
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
        /// 获取LR_ERP_PurchaseOrderDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PurchaseOrderDetailEntity GetLR_ERP_PurchaseOrderDetailEntity(string keyValue)
        {
            try
            {
                return purchaseOrderService.GetLR_ERP_PurchaseOrderDetailEntity(keyValue);
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
        /// 获取获取当前表字段
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public DataTable GetTableField()
        {
            try
            {
                return purchaseOrderService.GetTableField();
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
        /// 获取获取当前表字段
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PurchaseOrderDetailEntity> GetTableDate()
        {
            try
            {
                return purchaseOrderService.GetTableDate();
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
                purchaseOrderService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_ERP_PurchaseOrderEntity entity,List<LR_ERP_PurchaseOrderDetailEntity> lR_ERP_PurchaseOrderDetailList)
        {
            try
            {
                purchaseOrderService.SaveEntity(keyValue, entity,lR_ERP_PurchaseOrderDetailList);
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
        public IEnumerable<LR_ERP_PurchaseOrderEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            return purchaseOrderService.GetPurchasePageList(pagination, purchaseId);
        }
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            purchaseOrderService.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
        }
        public void UpdatePurchase(string purchaseId)
        {
            purchaseOrderService.UpdatePurchase(purchaseId);
        }
        #endregion

    }
}
