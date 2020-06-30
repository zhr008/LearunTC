using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-26 09:44
    /// 描 述：收款单
    /// </summary>
    public class ReceiptInfoBLL : ReceiptInfoIBLL
    {
        private ReceiptInfoService receiptInfoService = new ReceiptInfoService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_ReceiptInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return receiptInfoService.GetPageList(pagination, queryJson);
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
        /// 获取LR_ERP_ReceiptInfoDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_ReceiptInfoDetailEntity> GetLR_ERP_ReceiptInfoDetailList(string keyValue)
        {
            try
            {
                return receiptInfoService.GetLR_ERP_ReceiptInfoDetailList(keyValue);
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
        /// 获取LR_ERP_ReceiptInfo表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_ReceiptInfoEntity GetLR_ERP_ReceiptInfoEntity(string keyValue)
        {
            try
            {
                return receiptInfoService.GetLR_ERP_ReceiptInfoEntity(keyValue);
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
        /// 获取LR_ERP_ReceiptInfoDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_ReceiptInfoDetailEntity GetLR_ERP_ReceiptInfoDetailEntity(string keyValue)
        {
            try
            {
                return receiptInfoService.GetLR_ERP_ReceiptInfoDetailEntity(keyValue);
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
        public IEnumerable<LR_ERP_ReceiptInfoDetailEntity> GetTableDate()
        {
            try
            {
                return receiptInfoService.GetTableDate();
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
                receiptInfoService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_ERP_ReceiptInfoEntity entity,List<LR_ERP_ReceiptInfoDetailEntity> lR_ERP_ReceiptInfoDetailList)
        {
            try
            {
                receiptInfoService.SaveEntity(keyValue, entity,lR_ERP_ReceiptInfoDetailList);
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
        public IEnumerable<LR_ERP_ReceiptInfoEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            return receiptInfoService.GetPurchasePageList(pagination, purchaseId);
        }
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            receiptInfoService.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
        }
        public void UpdatePurchase(string purchaseId)
        {
            receiptInfoService.UpdatePurchase(purchaseId);
        }
        #endregion

    }
}
