using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Extention.TaskScheduling
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 16:07
    /// 描 述：任务计划模板信息
    /// </summary>
    public class TSSchemeBLL : TSSchemeIBLL
    {
        private TSSchemeService schemeService = new TSSchemeService();
        private TSProcessIBLL tSProcessIBLL = new TSProcessBLL();

        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// <summary> 
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public IEnumerable<TSSchemeInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                var list  = (List<TSSchemeInfoEntity>)schemeService.GetPageList(pagination, queryJson);

                var list2 = list.FindAll(t=>(t.F_State == 1 || t.F_State == 2) && t.F_EndTime < DateTime.Now);
                foreach (var item in list2) {
                    item.F_State = 4;
                    TSProcessEntity tSProcessEntity = new TSProcessEntity()
                    {
                        F_Id = item.F_PorcessId,
                        F_State = 4
                    };
                    tSProcessIBLL.SaveEntity(item.F_PorcessId, tSProcessEntity);
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
        /// 获取模板的历史数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<TSSchemeEntity> GetSchemePageList(Pagination pagination, string queryJson)
        {
            try
            {
                return schemeService.GetSchemePageList(pagination, queryJson);
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
        /// 获取表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public TSSchemeInfoEntity GetSchemeInfoEntity(string keyValue)
        {
            try
            {
                return schemeService.GetSchemeInfoEntity(keyValue);
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
        /// 获取表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public TSSchemeEntity GetSchemeEntity(string keyValue)
        {
            try
            {
                return schemeService.GetSchemeEntity(keyValue);
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
        /// 获取表实体数据 
        /// <param name="keyValue">模板信息主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public TSSchemeEntity GetSchemeEntityByInfo(string keyValue)
        {
            try
            {
                return schemeService.GetSchemeEntityByInfo(keyValue);
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
                schemeService.DeleteEntity(keyValue);
                QuartzHelper.DeleteJob(keyValue);
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
        public void SaveEntity(string keyValue, TSSchemeInfoEntity schemeInfoEntity, TSSchemeEntity schemeEntity)
        {
            try
            {
                TSProcessEntity tSProcessEntity =  schemeService.SaveEntity(keyValue,schemeInfoEntity, schemeEntity);
                if (tSProcessEntity != null) {
                    QuartzHelper.DeleteJob(keyValue);
                    QuartzHelper.AddJob(schemeInfoEntity.F_Id, tSProcessEntity.F_Id, schemeEntity.F_Scheme.ToObject<TSSchemeModel>());
                }

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
        public void SaveEntity(string keyValue, TSSchemeInfoEntity entity)
        {
            try
            {
                schemeService.SaveEntity(keyValue, entity);
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

        #region 扩展应用
        /// <summary>
        /// 暂停任务
        /// </summary>
        /// <param name="processId">任务进程主键</param>
        public void PauseJob(string processId) {
            try
            {
                TSProcessEntity tSProcessEntity = tSProcessIBLL.GetProcessEntity(processId);
                if (tSProcessEntity.F_State == 1 || tSProcessEntity.F_State == 2)
                {
                    tSProcessEntity.F_State = 3;
                    tSProcessIBLL.SaveEntity(processId, tSProcessEntity);
                    QuartzHelper.DeleteJob(tSProcessEntity.F_SchemeInfoId);
                }

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
        /// 启动任务
        /// </summary>
        /// <param name="processId">任务进程主键</param>
        public void EnAbleJob(string processId)
        {
            try
            {
                TSProcessEntity tSProcessEntity = tSProcessIBLL.GetProcessEntity(processId);
                TSSchemeEntity tSSchemeEntity = GetSchemeEntityByInfo(tSProcessEntity.F_SchemeInfoId);

                TSSchemeModel tSSchemeModel = tSSchemeEntity.F_Scheme.ToObject<TSSchemeModel>();
                if (tSProcessEntity.F_SchemeId != tSSchemeEntity.F_Id)
                {
                    tSProcessEntity.F_State = 10;
                    tSProcessIBLL.SaveEntity(tSProcessEntity.F_Id, tSProcessEntity);

                    // 如果模板更改需要重新创建一个任务进程
                    tSProcessEntity = new TSProcessEntity()
                    {
                        F_SchemeId = tSSchemeEntity.F_Id,
                        F_SchemeInfoId = tSProcessEntity.F_SchemeInfoId,
                        F_State = 2,
                        F_EndType = tSSchemeModel.endType,
                        F_EndTime = tSSchemeModel.endTime
                    };
                    if (tSSchemeModel.startType == 1)
                    {
                        tSProcessEntity.F_BeginTime = DateTime.Now;
                    }
                    else
                    {
                        tSProcessEntity.F_BeginTime = tSSchemeModel.startTime;
                    }
                    if (tSSchemeModel.endType == 1)
                    {
                        tSProcessEntity.F_EndTime = DateTime.MaxValue;
                    }

                    tSProcessIBLL.SaveEntity("", tSProcessEntity);

                    

                }
                else
                {
                    tSProcessEntity.F_State = 2;
                    tSProcessIBLL.SaveEntity(tSProcessEntity.F_Id, tSProcessEntity);

                }
                QuartzHelper.AddJob(tSProcessEntity.F_SchemeInfoId, tSProcessEntity.F_Id, tSSchemeModel);
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
