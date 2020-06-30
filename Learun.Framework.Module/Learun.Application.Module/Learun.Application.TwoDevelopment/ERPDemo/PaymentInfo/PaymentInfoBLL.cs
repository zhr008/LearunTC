using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-26 11:35
    /// 描 述：付款单
    /// </summary>
    public class PaymentInfoBLL : PaymentInfoIBLL
    {
        private PaymentInfoService paymentInfoService = new PaymentInfoService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PaymentInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return paymentInfoService.GetPageList(pagination, queryJson);
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
        /// 获取LR_ERP_PaymentInfoDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_PaymentInfoDetailEntity> GetLR_ERP_PaymentInfoDetailList(string keyValue)
        {
            try
            {
                return paymentInfoService.GetLR_ERP_PaymentInfoDetailList(keyValue);
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
        /// 获取LR_ERP_PaymentInfo表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PaymentInfoEntity GetLR_ERP_PaymentInfoEntity(string keyValue)
        {
            try
            {
                return paymentInfoService.GetLR_ERP_PaymentInfoEntity(keyValue);
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
        /// 获取LR_ERP_PaymentInfoDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_PaymentInfoDetailEntity GetLR_ERP_PaymentInfoDetailEntity(string keyValue)
        {
            try
            {
                return paymentInfoService.GetLR_ERP_PaymentInfoDetailEntity(keyValue);
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
        public IEnumerable<LR_ERP_PaymentInfoDetailEntity> GetTableDate()
        {
            try
            {
                return paymentInfoService.GetTableDate();
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
                paymentInfoService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_ERP_PaymentInfoEntity entity,List<LR_ERP_PaymentInfoDetailEntity> lR_ERP_PaymentInfoDetailList)
        {
            try
            {
                paymentInfoService.SaveEntity(keyValue, entity,lR_ERP_PaymentInfoDetailList);
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
        public IEnumerable<LR_ERP_PaymentInfoEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            return paymentInfoService.GetPurchasePageList(pagination, purchaseId);
        }
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            paymentInfoService.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
        }
        public void UpdatePurchase(string purchaseId)
        {
            paymentInfoService.UpdatePurchase(purchaseId);
        }
        #endregion

    }
}
