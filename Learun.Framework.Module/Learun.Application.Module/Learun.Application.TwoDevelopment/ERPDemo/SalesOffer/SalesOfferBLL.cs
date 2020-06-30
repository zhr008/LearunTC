using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-09-25 17:29
    /// 描 述：销售报价
    /// </summary>
    public class SalesOfferBLL : SalesOfferIBLL
    {
        private SalesOfferService salesOfferService = new SalesOfferService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_ERP_SalesOfferEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return salesOfferService.GetPageList(pagination, queryJson);
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
        /// 获取LR_ERP_SalesOfferDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_ERP_SalesOfferDetailEntity> GetLR_ERP_SalesOfferDetailList(string keyValue)
        {
            try
            {
                return salesOfferService.GetLR_ERP_SalesOfferDetailList(keyValue);
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
        /// 获取LR_ERP_SalesOffer表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_SalesOfferEntity GetLR_ERP_SalesOfferEntity(string keyValue)
        {
            try
            {
                return salesOfferService.GetLR_ERP_SalesOfferEntity(keyValue);
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
        /// 获取LR_ERP_SalesOfferDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_ERP_SalesOfferDetailEntity GetLR_ERP_SalesOfferDetailEntity(string keyValue)
        {
            try
            {
                return salesOfferService.GetLR_ERP_SalesOfferDetailEntity(keyValue);
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
        public IEnumerable<LR_ERP_SalesOfferDetailEntity> GetTableDate()
        {
            try
            {
                return salesOfferService.GetTableDate();
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
                salesOfferService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_ERP_SalesOfferEntity entity,List<LR_ERP_SalesOfferDetailEntity> lR_ERP_SalesOfferDetailList)
        {
            try
            {
                salesOfferService.SaveEntity(keyValue, entity,lR_ERP_SalesOfferDetailList);
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
        public IEnumerable<LR_ERP_SalesOfferEntity> GetPurchasePageList(Pagination pagination, string purchaseId)
        {
            return salesOfferService.GetPurchasePageList(pagination, purchaseId);
        }
        public void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId)
        {
            salesOfferService.UpdatePurchaseInfo(purchaseInfoId, purchaseId);
        }
        public void UpdatePurchase(string purchaseId)
        {
            salesOfferService.UpdatePurchase(purchaseId);
        }
        #endregion

    }
}
