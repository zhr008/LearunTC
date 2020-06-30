using Learun.Util;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-05-08 18:30
    /// 描 述：甘特图应用
    /// </summary>
    public interface GantProjectIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<LR_OA_ProjectEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取LR_OA_ProjectDetail表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_OA_ProjectDetailEntity> GetLR_OA_ProjectDetailList(string keyValue);
        /// <summary>
        /// 获取LR_OA_Project表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_OA_ProjectEntity GetLR_OA_ProjectEntity(string keyValue);
        /// <summary>
        /// 获取LR_OA_ProjectDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_OA_ProjectDetailEntity GetLR_OA_ProjectDetailEntity(string keyValue);
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        IEnumerable<LR_OA_ProjectEntity> GetList(string keyValue);
        /// <summary>
        /// 获取项目明细列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IEnumerable<LR_OA_ProjectDetailEntity> GetDetailList(string parentId);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 删除明细数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void DeleteDetail(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void SaveEntity(string keyValue, LR_OA_ProjectEntity entity, List<LR_OA_ProjectDetailEntity> lR_OA_ProjectDetailList);
        /// <summary>
        /// 保存表头实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void SaveGant(string keyValue, LR_OA_ProjectEntity entity);
        /// <summary>
        /// 保存明细实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void SaveDetail(string keyValue, LR_OA_ProjectDetailEntity entity);
        #endregion

    }
}
