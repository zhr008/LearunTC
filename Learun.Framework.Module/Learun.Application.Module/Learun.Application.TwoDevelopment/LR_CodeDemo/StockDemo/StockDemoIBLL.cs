using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-06-12 18:49
    /// 描 述：库存
    /// </summary>
    public interface StockDemoIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_Demo_StockEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 指定库位列表数据
        /// <summary>
        /// <param name="stockArea">库位ID</param>
        /// <returns></returns>
        IEnumerable<LR_Demo_StockEntity> GetStock(string stockArea);
        /// <summary>
        /// 获取LR_Demo_Stock表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_Demo_StockEntity GetLR_Demo_StockEntity(string keyValue);
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
        void SaveEntity(string keyValue, LR_Demo_StockEntity entity);
        #endregion

    }
}
