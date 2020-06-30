using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.10
    /// 描 述：会签记录操作
    /// </summary>
    public class NWFConfluenceBLL : NWFConfluenceIBLL
    {

        private NWFConfluenceService wfConfluenceService = new NWFConfluenceService();

        #region 获取数据
        /// <summary>
        /// 获取会签记录
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        /// <param name="nodeId">节点主键</param>
        /// <returns></returns>
        public IEnumerable<NWFConfluenceEntity> GetList(string processId, string nodeId)
        {
            try
            {
                return wfConfluenceService.GetList(processId, nodeId);
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
        /// 保存成功的会前记录
        /// </summary>
        /// <param name="entity">实体</param>
        public void SaveEntity(NWFConfluenceEntity entity)
        {
            try
            {
                wfConfluenceService.SaveEntity(entity);
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
        /// 删除会签节点数据
        /// </summary>
        /// <param name="processId">实例主键</param>
        /// <param name="nodeId">节点主键</param>
        public void DeleteEntity(string processId, string nodeId)
        {
            try
            {
                wfConfluenceService.DeleteEntity(processId, nodeId);
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
