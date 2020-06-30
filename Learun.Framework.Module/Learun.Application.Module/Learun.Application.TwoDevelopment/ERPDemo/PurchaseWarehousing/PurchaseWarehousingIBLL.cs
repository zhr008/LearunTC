using Learun.Util;
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
    public interface PurchaseWarehousingIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_ERP_PurchaseWarehousingEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取LR_ERP_PurchaseWarehousingDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_ERP_PurchaseWarehousingDetailEntity> GetLR_ERP_PurchaseWarehousingDetailList(string keyValue);
        /// <summary>
        /// 获取LR_ERP_PurchaseWarehousing表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_PurchaseWarehousingEntity GetLR_ERP_PurchaseWarehousingEntity(string keyValue);
        /// <summary>
        /// 获取LR_ERP_PurchaseWarehousingDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_PurchaseWarehousingDetailEntity GetLR_ERP_PurchaseWarehousingDetailEntity(string keyValue);

        IEnumerable<LR_ERP_PurchaseWarehousingDetailEntity> GetTableDate();
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void SaveEntity(string keyValue, LR_ERP_PurchaseWarehousingEntity entity,List<LR_ERP_PurchaseWarehousingDetailEntity> lR_ERP_PurchaseWarehousingDetailList);
        IEnumerable<LR_ERP_PurchaseWarehousingEntity> GetPurchasePageList(Pagination pagination, string purchaseId);
        void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId);
        void UpdatePurchase(string purchaseId);
        #endregion

    }
}
