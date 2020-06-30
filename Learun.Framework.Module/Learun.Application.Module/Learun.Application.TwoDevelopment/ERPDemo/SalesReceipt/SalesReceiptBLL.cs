using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 18:18
    /// 描 述：销售出库
    /// </summary>
    public class SalesReceiptBLL : SalesReceiptIBLL
    {
        private SalesReceiptService salesReceiptService = new SalesReceiptService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_SalesReceiptEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return salesReceiptService.GetPageList(pagination, queryJson);
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
        /// 获取LR_ERP_SalesReceiptDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_SalesReceiptDetailEntity> GetLR_ERP_SalesReceiptDetailList(string keyValue)
        {
            try
            {
                return salesReceiptService.GetLR_ERP_SalesReceiptDetailList(keyValue);
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
        /// 获取LR_ERP_SalesReceipt表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_SalesReceiptEntity GetLR_ERP_SalesReceiptEntity(string keyValue)
        {
            try
            {
                return salesReceiptService.GetLR_ERP_SalesReceiptEntity(keyValue);
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
        /// 获取LR_ERP_SalesReceiptDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_SalesReceiptDetailEntity GetLR_ERP_SalesReceiptDetailEntity(string keyValue)
        {
            try
            {
                return salesReceiptService.GetLR_ERP_SalesReceiptDetailEntity(keyValue);
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
        public IEnumerable<LR_ERP_SalesReceiptDetailEntity> GetTableDate()
        {
            try
            {
                return salesReceiptService.GetTableDate();
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
                salesReceiptService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_ERP_SalesReceiptEntity entity,List<LR_ERP_SalesReceiptDetailEntity> lR_ERP_SalesReceiptDetailList)
        {
            try
            {
                salesReceiptService.SaveEntity(keyValue, entity,lR_ERP_SalesReceiptDetailList);
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
        public IEnumerable<LR_ERP_SalesReceiptEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            return salesReceiptService.GetPurchasePageList(pagination, purchaseId);
        }
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            salesReceiptService.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
        }
        public void UpdatePurchase(string purchaseId)
        {
            salesReceiptService.UpdatePurchase(purchaseId);
        }
        #endregion

    }
}
