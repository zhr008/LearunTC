using Learun.Application.Base.SystemModule;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.Extention.DisplayBoardManage
{
    // Learun.Application.Extention.DisplayBoardManage
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 09:41
    /// 描 述：看板配置信息
    /// </summary>
    public class LR_KBConfigInfoBLL : LR_KBConfigInfoIBLL
    {
        private LR_KBConfigInfoService lR_KBConfigInfoService = new LR_KBConfigInfoService();
        private DatabaseLinkIBLL databaseLinkIBLL = new DatabaseLinkBLL();
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_KBConfigInfoEntity> GetList(string queryJson)
        {
            try
            {
                return lR_KBConfigInfoService.GetList(queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_KBConfigInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return lR_KBConfigInfoService.GetPageList(pagination, queryJson);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        /// <summary>
        /// 获取实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_KBConfigInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return lR_KBConfigInfoService.GetEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据看板id获取所有配置
        /// </summary>
        /// <param name="keyValue">看板id</param>
        /// <returns></returns>
        public IEnumerable<LR_KBConfigInfoEntity> GetListByKBId(string keyValue)
        {
            try
            {
                return lR_KBConfigInfoService.GetListByKBId(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                lR_KBConfigInfoService.DeleteEntity(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 根据看板id删除其所有配置信息
        /// </summary>
        /// <param name="keyValue">看板id</param>
        /// <returns></returns>
        public void DeleteByKBId(string keyValue)
        {
            try
            {
                lR_KBConfigInfoService.DeleteByKBId(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string keyValue, LR_KBConfigInfoEntity entity)
        {
            try
            {
                lR_KBConfigInfoService.SaveEntity(keyValue, entity);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }

        #endregion

        #region 扩展方法
        /// <summary>
        /// 获取配置数据
        /// </summary>
        /// <param name="configInfoList">配置信息列表</param>
        /// <returns></returns>
        public List<ConfigInfoDataModel> GetConfigData(List<ConfigInfoModel> configInfoList) {
            try
            {
                List<ConfigInfoDataModel> list = new List<ConfigInfoDataModel>();
                foreach (var item in configInfoList) {
                    ConfigInfoDataModel configInfoDataModel = new ConfigInfoDataModel();
                    configInfoDataModel.id = item.id;
                    configInfoDataModel.modelType = item.modelType;
                    configInfoDataModel.type = item.type;
                    configInfoDataModel.data = null;
                    configInfoDataModel.dataType = item.dataType;
                    if (item.type == "1")
                    {
                        DataTable dt = databaseLinkIBLL.FindTable(item.dbId, item.sql);
                        if (dt.Rows.Count > 0)
                        {
                            configInfoDataModel.data = dt;
                        }
                    }
                    else {
                        var result = HttpMethods.Get(item.url);
                        ResParameter resParameter = result.ToObject<ResParameter>();
                        if (resParameter != null)
                        {
                            if (resParameter.code.ToString() == "success")
                            {
                                configInfoDataModel.data = resParameter.data;
                            }
                        }
                    }
                    list.Add(configInfoDataModel);
                }
                return list;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

        #region 获取接口数据
        /// <summary>
        /// 根据接口路径获取接口数据（仅限get方法）
        /// </summary>
        /// <param name="path">接口路径</param>
        /// <returns></returns>
        public object GetApiData(string path)
        {
            try
            {
                var data = new object();
                var result = HttpMethods.Get(path);
                ResParameter resParameter = result.ToObject<ResParameter>();
                if (resParameter != null)
                {
                    if (resParameter.code.ToString() == "success")
                    {
                        data = resParameter.data;
                    }
                    else
                    {
                        data = "";
                    }
                }
                else
                {
                    data = "";
                }
                return data;
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowBusinessException(ex);
                }
            }
        }
        #endregion

    }
}
