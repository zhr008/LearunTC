using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.07
    /// 描 述：流程委托
    /// </summary>
    public class NWFDelegateBLL: NWFDelegateIBLL
    {
        private NWFDelegateService nWFDelegateService = new NWFDelegateService();

        #region 获取数据
        /// <summary>
        /// 获取分页列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">关键字(被委托人)</param>
        /// <param name="userInfo">用户信息</param>
        /// <returns></returns>
        public IEnumerable<NWFDelegateRuleEntity> GetPageList(Pagination pagination, string keyword, UserInfo userInfo)
        {
            try
            {
                return nWFDelegateService.GetPageList(pagination, keyword, userInfo);
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
        /// 根据委托人获取委托记录
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NWFDelegateRuleEntity> GetList(UserInfo userInfo)
        {
            try
            {
                return nWFDelegateService.GetList(userInfo);
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
        /// 获取关联的模板数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<NWFDelegateRelationEntity> GetRelationList(string keyValue)
        {
            try
            {
                return nWFDelegateService.GetRelationList(keyValue);
            }
            catch (Exception ex)
            {
                if (ex is ExceptionEx)
                {
                    throw;
                }
                else
                {
                    throw ExceptionEx.ThrowServiceException(ex);
                }
            }
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                nWFDelegateService.DeleteEntity(keyValue);
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
        /// 保存实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="wfDelegateRuleEntity">实体数据</param>
        /// <param name="schemeInfoList">关联模板主键</param>
        public void SaveEntity(string keyValue, NWFDelegateRuleEntity wfDelegateRuleEntity, string[] schemeInfoList)
        {
            try
            {
                nWFDelegateService.SaveEntity(keyValue, wfDelegateRuleEntity, schemeInfoList);
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
        /// 更新委托规则状态信息
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state"></param>
        public void UpdateState(string keyValue, int state)
        {
            try
            {
                nWFDelegateService.UpdateState(keyValue, state);
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
