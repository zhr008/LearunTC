using Learun.Util;
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
    public interface SalesReceiptIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_ERP_SalesReceiptEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取LR_ERP_SalesReceiptDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_ERP_SalesReceiptDetailEntity> GetLR_ERP_SalesReceiptDetailList(string keyValue);
        /// <summary>
        /// 获取LR_ERP_SalesReceipt表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_SalesReceiptEntity GetLR_ERP_SalesReceiptEntity(string keyValue);
        /// <summary>
        /// 获取LR_ERP_SalesReceiptDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_SalesReceiptDetailEntity GetLR_ERP_SalesReceiptDetailEntity(string keyValue);

        IEnumerable<LR_ERP_SalesReceiptDetailEntity> GetTableDate();
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
        void SaveEntity(string keyValue, LR_ERP_SalesReceiptEntity entity,List<LR_ERP_SalesReceiptDetailEntity> lR_ERP_SalesReceiptDetailList);
        IEnumerable<LR_ERP_SalesReceiptEntity> GetPurchasePageList(Pagination pagination, string purchaseId);
        void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId);
        void UpdatePurchase(string purchaseId);
        #endregion

    }
}
