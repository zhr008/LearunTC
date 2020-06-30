using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-29 14:04 
    /// 描 述：文件管理
    /// </summary> 
    public class FileInfoBLL: FileInfoIBLL
    {
        private FileInfoService fileInfoService = new FileInfoService();

        #region 获取数据

        /// <summary> 
        /// 获取表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public FileBInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return fileInfoService.GetEntity(keyValue);
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
        /// 获取文件列表实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public FilelistEntity GetListEntity(string keyValue)
        {
            try
            {
                return fileInfoService.GetListEntity(keyValue);
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
        /// 获取文件列表实体
        /// </summary>
        /// <param name="fileInfoId"></param>
        /// <returns></returns>
        public FilelistEntity GetListEntityByInfoId(string fileInfoId)
        {
            try
            {
                return fileInfoService.GetListEntityByInfoId(fileInfoId);
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

        /// <summary>
        /// 文件审核列表
        /// </summary>
        /// <param name="strWfSql">查询语句</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键字</param>
        /// <param name="userId">当前登陆者用户</param>
        /// <returns></returns>
        public IEnumerable<WFFileModel> GetWfPageList(string strWfSql, Pagination pagination, string keyword,string userId)
        {
            return fileInfoService.GetWfPageList(strWfSql, pagination, keyword, userId);
        }

        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetAllPublishPageList(string keyword, string folderId)
        {
            return fileInfoService.GetAllPublishPageList(keyword, folderId);
        }

        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetPublishList(string keyword, string folderId) {
            return fileInfoService.GetPublishList(keyword, folderId);
        }


        /// <summary>
        /// 获取文件的历史信息
        /// </summary>
        /// <param name="fileInfoId">主键</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetHistoryList(string fileInfoId) {
            return fileInfoService.GetHistoryList(fileInfoId);
        }


        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        public IEnumerable<FileBInfoEntity> GetDeleteList(string keyword) {
            return fileInfoService.GetDeleteList(keyword);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">文件列表主键</param>
        /// <param name="fileBInfoEntity">文件主信息</param>
        /// <param name="filelistEntity">文件列表信息</param>
        public void SaveEntity(string keyValue, FileBInfoEntity fileBInfoEntity, FilelistEntity filelistEntity)
        {
            try
            {
                fileInfoService.SaveEntity(keyValue, fileBInfoEntity, filelistEntity);
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
        /// 更新文件发布状态
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        public void UpdateEntity(string processId)
        {
            fileInfoService.UpdateEntity(processId);
        }


        /// <summary>
        /// 虚拟删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void VDeleteEntity(string keyValue)
        {
            fileInfoService.VDeleteEntity(keyValue);
        }
        /// <summary>
        /// 还原虚拟删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RecoveryEntity(string keyValue) {
            fileInfoService.RecoveryEntity(keyValue);
        }
        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteEntity(string keyValue)
        {
            fileInfoService.DeleteEntity(keyValue);
        }
        #endregion
    }
}
