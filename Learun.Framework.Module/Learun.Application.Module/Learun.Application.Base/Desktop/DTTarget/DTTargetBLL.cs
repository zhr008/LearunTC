using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.Desktop
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-05-28 16:08
    /// 描 述：桌面统计数据配置
    /// </summary>
    public class DTTargetBLL: DTTargetIBLL
    {
        private DTTargetService dTTargetService = new DTTargetService();

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DTTargetEntity> GetList()
        {
            try
            {
                return dTTargetService.GetList();
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
        /// </summary>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<DTTargetEntity> GetList(string keyword)
        {
            try
            {
                return dTTargetService.GetList(keyword);
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
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public DTTargetEntity GetEntity(string keyValue)
        {
            try
            {
                return dTTargetService.GetEntity(keyValue);
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
                dTTargetService.DeleteEntity(keyValue);
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
        /// <param name="entity">实体</param>
        /// </summary>
        public void SaveEntity(string keyValue, DTTargetEntity entity)
        {
            try
            {
                dTTargetService.SaveEntity(keyValue, entity);

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
