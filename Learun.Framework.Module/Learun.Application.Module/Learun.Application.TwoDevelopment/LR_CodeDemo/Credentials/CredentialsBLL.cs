using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-02 23:56
    /// 描 述：个人资格证
    /// </summary>
    public class CredentialsBLL : CredentialsIBLL
    {
        private CredentialsService credentialsService = new CredentialsService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<CredentialsInfo> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return credentialsService.GetPageList(pagination, queryJson);
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
        /// 获取tc_Credentials表实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public tc_CredentialsEntity Gettc_CredentialsEntity(string keyValue)
        {
            try
            {
                return credentialsService.Gettc_CredentialsEntity(keyValue);
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


        public tc_CredentialsEntity Gettc_CredentialsEntity(string keyValue, int? F_CertType, int? F_MajorType) 
        {
            try
            {
                return credentialsService.Gettc_CredentialsEntity(keyValue, F_CertType, F_MajorType);
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
                credentialsService.DeleteEntity(keyValue);
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
        public List<TreeModel> GetTree(string PersonId, string ApplicantId)
        {
            try
            {
                DataTable list = credentialsService.GetSqlTree(PersonId, ApplicantId);

                List<TreeModel> treeList = new List<TreeModel>();
                foreach (DataRow item in list.Rows)
                {
                    TreeModel node = new TreeModel
                    {
                        id = item["id"].ToString(),
                        text = item["text"].ToString(),
                        value = item["value"].ToString(),
                        showcheck = false,
                        checkstate = 0,
                        isexpand = false,
                        parentId = item["parentid"].ToString()
                    };
                    treeList.Add(node);
                }
                return treeList.ToTree("12");
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
        public void SaveEntity(string keyValue, tc_CredentialsEntity entity)
        {
            try
            {
                credentialsService.SaveEntity(keyValue, entity);
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
