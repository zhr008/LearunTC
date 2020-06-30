using Learun.Util;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.Extention.DisplayBoardManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 10:10
    /// 描 述：看板信息
    /// </summary>
    public class LR_KBKanBanInfoBLL : LR_KBKanBanInfoIBLL
    {
        private LR_KBKanBanInfoService lR_KBKanBanInfoService = new LR_KBKanBanInfoService();
        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_KBKanBanInfoEntity> GetList( string queryJson )
        {
            try
            {
                return lR_KBKanBanInfoService.GetList(queryJson);
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
        public IEnumerable<LR_KBKanBanInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return lR_KBKanBanInfoService.GetPageList(pagination, queryJson);
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
        public LR_KBKanBanInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return lR_KBKanBanInfoService.GetEntity(keyValue);
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
                lR_KBKanBanInfoService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue,string kanbaninfo, string kbconfigInfo)
        {
            try
            {
                lR_KBKanBanInfoService.SaveEntity(keyValue, kanbaninfo, kbconfigInfo);
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
        #region
        /// <summary>
        /// 获取看板模板（加入空模板）
        /// </summary>
        /// <returns></returns>
        public List<LR_KBKanBanInfoEntity> GetTemptList()
        {
            try
            {
                List<LR_KBKanBanInfoEntity> list = new List<LR_KBKanBanInfoEntity>();
                var data = GetList(null);
                foreach(var item in data)
                {
                    list.Add(item);
                }
                LR_KBKanBanInfoEntity kanBanInfoEntity = new LR_KBKanBanInfoEntity();
                kanBanInfoEntity.F_Id = "12";
                kanBanInfoEntity.F_KanBanName = "空模板";
                kanBanInfoEntity.F_KanBanCode = "kanban00001";
                list.Add(kanBanInfoEntity);
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
    }
}
