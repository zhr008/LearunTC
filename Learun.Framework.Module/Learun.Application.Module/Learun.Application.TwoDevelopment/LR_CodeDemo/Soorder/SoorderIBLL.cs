using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-29 15:28
    /// 描 述：销售订单
    /// </summary>
    public interface SoorderIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表分页数据
        /// <summary>
        /// <param name="pagination">查询参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<AllotAssetsEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<AllotAssetsEntity> GetList(string queryJson);
        /// <summary>
        /// 获取ApplyAssets表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<ApplyAssetsEntity> GetApplyAssetsList(string keyValue);
        /// <summary>
        /// 获取AllotAssets表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        AllotAssetsEntity GetAllotAssetsEntity(string keyValue);
        /// <summary>
        /// 获取ApplyAssets表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        ApplyAssetsEntity GetApplyAssetsEntity(string keyValue);
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
        void SaveEntity(UserInfo userInfo, string keyValue, AllotAssetsEntity entity,List<ApplyAssetsEntity> applyAssetsList);
        #endregion

    }
}
