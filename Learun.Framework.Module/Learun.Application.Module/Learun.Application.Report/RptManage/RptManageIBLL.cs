using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.Report
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 15:17
    /// 描 述：报表文件管理
    /// </summary>
    public interface RptManageIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_RPT_FileInfoEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取LR_RPT_FileInfo表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_RPT_FileInfoEntity GetLR_RPT_FileInfoEntity(string keyValue);
        /// <summary>
        /// 获取报表文件树
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetFileTree();
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
        void SaveEntity(string keyValue, LR_RPT_FileInfoEntity entity);
        #endregion

    }
}
