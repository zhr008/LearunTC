using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;
using Learun.Cache.Base;
using Learun.Cache.Factory;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-29 21:15
    /// 描 述：身份证管理
    /// </summary>
    public class IDCardBLL : IDCardIBLL
    {
        private IDCardService iDCardService = new IDCardService();


        #region 缓存定义
        private ICache cache = CacheFactory.CaChe();
        private string cacheKey = "learun_adms_personnels";
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<IDCardEntityInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return iDCardService.GetPageList(pagination, queryJson);
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
        /// 获取tc_IDCard表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_IDCardEntity Gettc_IDCardEntity(string keyValue)
        {
            try
            {
                return iDCardService.Gettc_IDCardEntity(keyValue);
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
        /// 获取tc_Personnels表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_PersonnelsEntity Gettc_PersonnelsEntity(string keyValue)
        {
            try
            {
                return iDCardService.Gettc_PersonnelsEntity(keyValue);
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



        public List<TreeModel> GetList()
        {
            try
            {
                List<TreeModel> list = cache.Read<List<TreeModel>>(cacheKey, CacheId.personnels);
                if (list == null)
                {
                    list = (List<TreeModel>)iDCardService.GetSqlTree();
                    cache.Write<List<TreeModel>>(cacheKey, list, CacheId.personnels);
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
        /// 获取左侧树形数据
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetTree(string PersonId, string ApplicantId, string UserName)
        {
            try
            {
                //DataTable list = iDCardService.GetSqlTree(PersonId, ApplicantId, UserName);


                var data = GetList();
                //if (!string.IsNullOrEmpty(ApplicantId))
                //{
                //    data= data.FindAll(c => c.id == PersonId);
                //}
                if (!string.IsNullOrEmpty(PersonId))
                {
                    data = data.FindAll(c => c.id == PersonId);
                }
                if (!string.IsNullOrEmpty(UserName))
                {
                    data = data.FindAll(c => c.text.Contains(UserName));
                }

                TreeModel tree = new TreeModel
                {

                    id = "48741BEB-FA5E-B647-2ADA-1473A71FD524",
                    text = "全部人员",
                    value = "",
                    parentId = "",
                    icon= "fa fa-folder"
                };
                data.Add(tree);

                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in data)
                {
                    TreeModel node = new TreeModel
                    {
                        id = item.id,
                        text = item.text,
                        value = item.value,
                        showcheck = false,
                        checkstate = 0,
                        isexpand = true,
                        parentId = item.parentId,
                    };
                    treeList.Add(node);
                }
                return treeList.ToTree();
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                iDCardService.DeleteEntity(keyValue);
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        public void SaveEntity(string keyValue, tc_IDCardEntity tc_IDCardEntity)
        {
            try
            {
                iDCardService.SaveEntity(keyValue, tc_IDCardEntity);
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
