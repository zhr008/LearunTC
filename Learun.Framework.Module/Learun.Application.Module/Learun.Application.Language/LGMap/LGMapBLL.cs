using Learun.Cache.Base;
using Learun.Cache.Factory;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Data;

namespace Learun.Application.Language
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-04-10 15:00
    /// 描 述：语言映照
    /// </summary>
    public class LGMapBLL : LGMapIBLL
    {
        private LGMapService lGMapService = new LGMapService();

        #region 缓存设置
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_lg_";       // + 语言类型编码
        #endregion


        #region 获取数据
        /// <summary>
        /// 获取语言包映射关系数据集合
        /// <param name="Code">语言包编码</param>
        /// <param name="isMain">是否是主语言</param>
        /// <summary>
        /// <returns></returns>
        public Dictionary<string, string> GetMap(string Code,bool isMain)
        {
            try
            {
                Dictionary<string,string> list = cache.Read<Dictionary<string, string>>(cacheKey + Code, CacheId.language);
                if (list == null)
                {
                    list = new Dictionary<string, string>();
                    var _list = lGMapService.GetDataList(Code);

                    if (isMain)
                    {
                        foreach (var item in _list)
                        {
                            if (!list.ContainsKey(item.name))
                            {
                                list.Add(item.name, item.id);
                            }
                        }
                    }
                    else {
                        foreach (var item in _list)
                        {
                            if (!list.ContainsKey(item.id))
                            {
                                list.Add(item.id, item.name);
                            }
                        }
                    }
                    cache.Write<Dictionary<string, string>>(cacheKey + Code, list, CacheId.language);
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
        /// <summary>
        /// 获取列表数据
        /// <param name="TypeCode">编码</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGMapEntity> GetListByTypeCode(string TypeCode)
        {
            try
            {
                return lGMapService.GetListByTypeCode(TypeCode);
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
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGMapEntity> GetList(string queryJson)
        {
            try
            {
                return lGMapService.GetList(queryJson);
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
        /// <param name="typeList">语言类型列表</param>
        /// <summary>
        /// <returns></returns>
        public DataTable GetPageList(Pagination pagination, string queryJson, string typeList)
        {
            try
            {
                return lGMapService.GetPageList(pagination, queryJson, typeList);
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
        public LGMapEntity GetEntity(string keyValue)
        {
            try
            {
                return lGMapService.GetEntity(keyValue);
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
        /// 根据名称获取列表
        /// <param name="keyValue">F_Name</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGMapEntity> GetListByName(string keyValue)
        {
            try
            {
                return lGMapService.GetListByName(keyValue);
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
        /// 根据名称与类型获取列表
        /// <param name="keyValue">F_Name</param>
        /// <param name="typeCode">typeCode</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LGMapEntity> GetListByNameAndType(string keyValue, string typeCode)
        {
            try
            {
                return lGMapService.GetListByNameAndType(keyValue, typeCode);
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

                lGMapService.DeleteEntity(keyValue);
                cache.RemoveAll(CacheId.language);
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
        /// <param name="nameList">原列表</param>
        /// <param name="newNameList">新列表</param>
        /// <param name="code">F_Code</param>
        /// <summary>
        /// <returns></returns>
        public void SaveEntity(string nameList, string newNameList, string code)
        {
            try
            {
                lGMapService.SaveEntity(nameList, newNameList, code);
                cache.RemoveAll(CacheId.language);
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
