using Learun.Util;
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
    public interface ReceiptInfoIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_ERP_ReceiptInfoEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取LR_ERP_ReceiptInfoDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_ERP_ReceiptInfoDetailEntity> GetLR_ERP_ReceiptInfoDetailList(string keyValue);
        /// <summary>
        /// 获取LR_ERP_ReceiptInfo表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_ReceiptInfoEntity GetLR_ERP_ReceiptInfoEntity(string keyValue);
        /// <summary>
        /// 获取LR_ERP_ReceiptInfoDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_ERP_ReceiptInfoDetailEntity GetLR_ERP_ReceiptInfoDetailEntity(string keyValue);

        IEnumerable<LR_ERP_ReceiptInfoDetailEntity> GetTableDate();
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
        void SaveEntity(string keyValue, LR_ERP_ReceiptInfoEntity entity,List<LR_ERP_ReceiptInfoDetailEntity> lR_ERP_ReceiptInfoDetailList);
        IEnumerable<LR_ERP_ReceiptInfoEntity> GetPurchasePageList(Pagination pagination, string purchaseId);
        void UpdatePurchaseInfo(string purchaseInfoId, string purchaseId);
        void UpdatePurchase(string purchaseId);
        #endregion

    }
}
