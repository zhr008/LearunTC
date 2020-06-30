using Learun.Util;
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
    public interface SalesOrderIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_ERP_SalesOrderEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取LR_ERP_SalesOrderDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_ERP_SalesOrderDetailEntity> GetLR_ERP_SalesOrderDetailList(string keyValue);
        /// <summary>
        /// 获取LR_ERP_SalesOrder表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_SalesOrderEntity GetLR_ERP_SalesOrderEntity(string keyValue);
        /// <summary>
        /// 获取LR_ERP_SalesOrderDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_SalesOrderDetailEntity GetLR_ERP_SalesOrderDetailEntity(string keyValue);

        IEnumerable<LR_ERP_SalesOrderDetailEntity> GetTableDate();
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
        void SaveEntity(string keyValue, LR_ERP_SalesOrderEntity entity,List<LR_ERP_SalesOrderDetailEntity> lR_ERP_SalesOrderDetailList);
        IEnumerable<LR_ERP_SalesOrderEntity> GetPurchasePageList(Pagination pagination, string purchaseId);
        void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId);
        void UpdatePurchase(string purchaseId);
        #endregion

    }
}
