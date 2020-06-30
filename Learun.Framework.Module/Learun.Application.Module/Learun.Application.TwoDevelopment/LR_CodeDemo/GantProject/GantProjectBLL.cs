using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-05-08 18:30
    /// 描 述：甘特图应用
    /// </summary>
    public class GantProjectBLL : GantProjectIBLL
    {
        private GantProjectService gantProjectService = new GantProjectService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_OA_ProjectEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return gantProjectService.GetPageList(pagination, queryJson);
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
        /// 获取LR_OA_ProjectDetail表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_OA_ProjectDetailEntity> GetLR_OA_ProjectDetailList(string keyValue)
        {
            try
            {
                return gantProjectService.GetLR_OA_ProjectDetailList(keyValue);
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
        /// 获取LR_OA_Project表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_OA_ProjectEntity GetLR_OA_ProjectEntity(string keyValue)
        {
            try
            {
                return gantProjectService.GetLR_OA_ProjectEntity(keyValue);
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
        /// 获取LR_OA_ProjectDetail表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_OA_ProjectDetailEntity GetLR_OA_ProjectDetailEntity(string keyValue)
        {
            try
            {
                return gantProjectService.GetLR_OA_ProjectDetailEntity(keyValue);
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
        /// 获取项目列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public IEnumerable<LR_OA_ProjectEntity> GetList(string keyValue)
        {
            return gantProjectService.GetList(keyValue);
        }
        /// <summary>
        /// 获取项目列表
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public IEnumerable<LR_OA_ProjectDetailEntity> GetDetailList(string parentId)
        {
            return gantProjectService.GetLR_OA_ProjectDetailList(parentId);
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
                gantProjectService.DeleteEntity(keyValue);
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
        /// 删除明细数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteDetail(string keyValue)
        {
            try
            {
                gantProjectService.DeleteDetail(keyValue);
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
        public void SaveEntity(string keyValue, LR_OA_ProjectEntity entity,List<LR_OA_ProjectDetailEntity> lR_OA_ProjectDetailList)
        {
            try
            {
                gantProjectService.SaveEntity(keyValue, entity,lR_OA_ProjectDetailList);
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
        /// 保存表头实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveGant(string keyValue, LR_OA_ProjectEntity entity)
        {
            try
            {
                gantProjectService.SaveGant(keyValue, entity);
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
        /// 保存明细实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void SaveDetail(string keyValue, LR_OA_ProjectDetailEntity entity)
        {
            try
            {
                gantProjectService.SaveDetail(keyValue, entity);
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
