using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:50
    /// 描 述：检查项目
    /// </summary>
    public interface AnlyCheckIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<MSTB_QUA_CHECKITEMINFOEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取MSTB_QUA_CHECKITEMINFO表实体数据
        /// <summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        MSTB_QUA_CHECKITEMINFOEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        void SaveEntity(string keyValue, MSTB_QUA_CHECKITEMINFOEntity entity);
        /// <summary>
        /// 保存实体列表数据
        /// <summary>
        /// <param name="list">实体列表</param>
        /// <returns></returns>
        void SaveList(List<MSTB_QUA_CHECKITEMINFOEntity> list);
        #endregion

    }
}
