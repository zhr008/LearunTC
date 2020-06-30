using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Extention.DisplayBoardManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 09:41
    /// 描 述：看板配置信息
    /// </summary>
    public interface LR_KBConfigInfoIBLL
    {
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_KBConfigInfoEntity> GetList( string queryJson );
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        IEnumerable<LR_KBConfigInfoEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        LR_KBConfigInfoEntity GetEntity(string keyValue);
        /// <summary>
        /// 根据看板id获取所有配置
        /// </summary>
        /// <param name="keyValue">看板id</param>
        /// <returns></returns>
        IEnumerable<LR_KBConfigInfoEntity> GetListByKBId(string keyValue);
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 根据看板id删除其所有配置信息
        /// </summary>
        /// <param name="keyValue">看板id</param>
        /// <returns></returns>
        void DeleteByKBId(string keyValue);
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        void SaveEntity(string keyValue, LR_KBConfigInfoEntity entity);
        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取配置数据
        /// </summary>
        /// <param name="configInfoList">配置信息列表</param>
        /// <returns></returns>
        List<ConfigInfoDataModel> GetConfigData(List<ConfigInfoModel> configInfoList);
        /// <summary>
        /// 根据接口路径获取接口数据（仅限get方法）
        /// </summary>
        /// <param name="path">接口路径</param>
        /// <returns></returns>
        object GetApiData(string path);
        #endregion
    }
}
