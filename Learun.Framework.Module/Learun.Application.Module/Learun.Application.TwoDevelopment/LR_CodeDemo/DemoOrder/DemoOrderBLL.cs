using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-06-04 10:38
    /// 描 述：订单信息
    /// </summary>
    public class DemoOrderBLL : DemoOrderIBLL
    {
        private DemoOrderService demoOrderService = new DemoOrderService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_Demo_OrderEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return demoOrderService.GetPageList(pagination, queryJson);
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
        /// 获取LR_Demo_OrderDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_Demo_OrderDetailEntity> GetLR_Demo_OrderDetailList(string keyValue)
        {
            try
            {
                return demoOrderService.GetLR_Demo_OrderDetailList(keyValue);
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
        /// 获取LR_Demo_Order表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_Demo_OrderEntity GetLR_Demo_OrderEntity(string keyValue)
        {
            try
            {
                return demoOrderService.GetLR_Demo_OrderEntity(keyValue);
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
        /// 获取LR_Demo_OrderDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_Demo_OrderDetailEntity GetLR_Demo_OrderDetailEntity(string keyValue)
        {
            try
            {
                return demoOrderService.GetLR_Demo_OrderDetailEntity(keyValue);
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
        /// 获取主表实体数据
        /// <param name="processId">流程实例ID</param>
        /// <summary>
        /// <returns></returns>
        public LR_Demo_OrderEntity GetEntityByProcessId(string processId)
        {
            try
            {
                return demoOrderService.GetEntityByProcessId(processId);
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
                demoOrderService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_Demo_OrderEntity entity,List<LR_Demo_OrderDetailEntity> lR_Demo_OrderDetailList)
        {
            try
            {
                demoOrderService.SaveEntity(keyValue, entity,lR_Demo_OrderDetailList);
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
