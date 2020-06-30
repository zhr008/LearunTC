using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 17:55
    /// 描 述：销售订单
    /// </summary>
    public class SalesOrderBLL : SalesOrderIBLL
    {
        private SalesOrderService salesOrderService = new SalesOrderService();

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
                return salesOrderService.GetPageList(pagination, queryJson);
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
        /// 获取LR_ERP_SalesOrderDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_SalesOrderDetailEntity> GetLR_ERP_SalesOrderDetailList(string keyValue)
        {
            try
            {
                return salesOrderService.GetLR_ERP_SalesOrderDetailList(keyValue);
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
        /// 获取LR_ERP_SalesOrder表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_SalesOrderEntity GetLR_ERP_SalesOrderEntity(string keyValue)
        {
            try
            {
                return salesOrderService.GetLR_ERP_SalesOrderEntity(keyValue);
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
        /// 获取LR_ERP_SalesOrderDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_SalesOrderDetailEntity GetLR_ERP_SalesOrderDetailEntity(string keyValue)
        {
            try
            {
                return salesOrderService.GetLR_ERP_SalesOrderDetailEntity(keyValue);
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
        public IEnumerable<LR_ERP_SalesOrderDetailEntity> GetTableDate()
        {
            try
            {
                return salesOrderService.GetTableDate();
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
                salesOrderService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_ERP_SalesOrderEntity entity,List<LR_ERP_SalesOrderDetailEntity> lR_ERP_SalesOrderDetailList)
        {
            try
            {
                salesOrderService.SaveEntity(keyValue, entity,lR_ERP_SalesOrderDetailList);
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
        public IEnumerable<LR_ERP_SalesOrderEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            return salesOrderService.GetPurchasePageList(pagination, purchaseId);
        }
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            salesOrderService.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
        }
        public void UpdatePurchase(string purchaseId)
        {
            salesOrderService.UpdatePurchase(purchaseId);
        }
        #endregion

    }
}
