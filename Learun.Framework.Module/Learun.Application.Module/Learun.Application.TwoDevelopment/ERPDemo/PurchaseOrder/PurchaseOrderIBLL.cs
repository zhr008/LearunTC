using Learun.Util;
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
    public interface PurchaseOrderIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_ERP_PurchaseOrderEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取LR_ERP_PurchaseOrderDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_ERP_PurchaseOrderDetailEntity> GetLR_ERP_PurchaseOrderDetailList(string keyValue);
        /// <summary>
        /// 获取LR_ERP_PurchaseOrder表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_PurchaseOrderEntity GetLR_ERP_PurchaseOrderEntity(string keyValue);
        /// <summary>
        /// 获取LR_ERP_PurchaseOrderDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_PurchaseOrderDetailEntity GetLR_ERP_PurchaseOrderDetailEntity(string keyValue);

        /// <summary>
        /// 获取表的所有字段
        /// </summary>
        /// <returns></returns>
        DataTable GetTableField();

        /// <summary>
        /// 获取表的所有数据
        /// </summary>
        /// <returns></returns>
        IEnumerable<LR_ERP_PurchaseOrderDetailEntity> GetTableDate();
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
        void SaveEntity(string keyValue, LR_ERP_PurchaseOrderEntity entity,List<LR_ERP_PurchaseOrderDetailEntity> lR_ERP_PurchaseOrderDetailList);

        IEnumerable<LR_ERP_PurchaseOrderEntity> GetPurchasePageList(Pagination pagination, string purchaseId);
        void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId);
        void UpdatePurchase(string purchaseId);
        #endregion

    }
}
