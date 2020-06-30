using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 16:07
    /// 描 述：任务计划模板信息
    /// </summary>
    public interface TSSchemeIBLL
    {
        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// <summary> 
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        IEnumerable<TSSchemeInfoEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取模板的历史数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        IEnumerable<TSSchemeEntity> GetSchemePageList(Pagination pagination, string queryJson);

        /// <summary> 
        /// 获取表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        TSSchemeInfoEntity GetSchemeInfoEntity(string keyValue);
        /// <summary> 
        /// 获取表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        TSSchemeEntity GetSchemeEntity(string keyValue);
        /// <summary> 
        /// 获取表实体数据 
        /// <param name="keyValue">模板信息主键</param> 
        /// <summary> 
        /// <returns></returns> 
        TSSchemeEntity GetSchemeEntityByInfo(string keyValue);
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
        void SaveEntity(string keyValue, TSSchemeInfoEntity schemeInfoEntity, TSSchemeEntity schemeEntity);


        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns>
        void SaveEntity(string keyValue, TSSchemeInfoEntity entity);
        #endregion

        #region 扩展应用
        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="processId">任务进程主键</param>
        void PauseJob(string processId);
        /// <summary>
        /// 启动任务
        /// </summary>
        /// <param name="processId">任务进程主键</param>
        void EnAbleJob(string processId);
        #endregion
    }
}
