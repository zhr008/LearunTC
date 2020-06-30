using Learun.Util;
using Learun.Application.Base.SystemModule;
using System;
using System.Data;
using System.Collections.Generic;

namespace Learun.Application.Report
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-03-14 15:17
    /// 描 述：报表文件管理
    /// </summary>
    public class RptManageBLL : RptManageIBLL
    {
        private RptManageService rptManageService = new RptManageService();

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// <summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<LR_RPT_FileInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return rptManageService.GetPageList(pagination, queryJson);
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
        /// 获取LR_RPT_FileInfo表实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public LR_RPT_FileInfoEntity GetLR_RPT_FileInfoEntity(string keyValue)
        {
            try
            {
                return rptManageService.GetLR_RPT_FileInfoEntity(keyValue);
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
        /// 获取报表文件树
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetFileTree()
        {
            try
            {
                List<DataItemDetailEntity> list = new DataItemBLL().GetDetailList("ReportSort");
                IEnumerable<LR_RPT_FileInfoEntity> fileList = rptManageService.GetList();
                List<TreeModel> treeList = new List<TreeModel>();
                foreach (var item in list)
                {
                    TreeModel node = new TreeModel();
                    node.id = item.F_ItemValue;
                    node.text = item.F_ItemName;
                    node.value = item.F_ItemValue;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = true;
                    node.parentId = item.F_ParentId == null ? "0" : item.F_ParentId;
                    treeList.Add(node);
                }
                foreach (var file in fileList)
                {
                    TreeModel node = new TreeModel();
                    node.id = file.F_Id;
                    node.text = file.F_Name;
                    node.value = file.F_File;
                    node.showcheck = false;
                    node.checkstate = 0;
                    node.isexpand = false;
                    node.parentId = file.F_Type;
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
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                rptManageService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_RPT_FileInfoEntity entity)
        {
            try
            {
                rptManageService.SaveEntity(keyValue, entity);
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
