using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-29 14:04 
    /// 描 述：文件管理
    /// </summary> 
    public interface FileInfoIBLL
    {
        #region 获取数据

        /// <summary> 
        /// 获取表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        FileBInfoEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取文件列表实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        FilelistEntity GetListEntity(string keyValue);

        /// <summary>
        /// 获取文件列表实体
        /// </summary>
        /// <param name="fileInfoId"></param>
        /// <returns></returns>
        FilelistEntity GetListEntityByInfoId(string fileInfoId);
        /// <summary>
        /// 文件审核列表
        /// </summary>
        /// <param name="strWfSql">查询语句</param>
        /// <param name="pagination">分页参数</param>
        /// <param name="keyword">查询关键字</param>
        /// <param name="userId">当前用户Id</param>
        /// <returns></returns>
        IEnumerable<WFFileModel> GetWfPageList(string strWfSql, Pagination pagination, string keyword,string userId);
        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        IEnumerable<FileBInfoEntity> GetAllPublishPageList(string keyword, string folderId);
        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <param name="folderId">文件夹id</param>
        /// <returns></returns>
        IEnumerable<FileBInfoEntity> GetPublishList(string keyword, string folderId);
        /// <summary>
        /// 获取文件的历史信息
        /// </summary>
        /// <param name="fileInfoId">主键</param>
        /// <returns></returns>
        IEnumerable<FileBInfoEntity> GetHistoryList(string fileInfoId);

        /// <summary>
        /// 获取正式发布的文件
        /// </summary>
        /// <param name="keyword">查询关键字</param>
        /// <returns></returns>
        IEnumerable<FileBInfoEntity> GetDeleteList(string keyword);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">文件列表主键</param>
        /// <param name="fileBInfoEntity">文件主信息</param>
        /// <param name="filelistEntity">文件列表信息</param>
        void SaveEntity(string keyValue, FileBInfoEntity fileBInfoEntity, FilelistEntity filelistEntity);
        /// <summary>
        /// 更新文件发布状态
        /// </summary>
        /// <param name="processId">流程实例主键</param>
        void UpdateEntity(string processId);

        /// <summary>
        /// 虚拟删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        void VDeleteEntity(string keyValue);
        /// <summary>
        /// 还原虚拟删除文件
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RecoveryEntity(string keyValue);
        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteEntity(string keyValue);
        #endregion
    }
}
