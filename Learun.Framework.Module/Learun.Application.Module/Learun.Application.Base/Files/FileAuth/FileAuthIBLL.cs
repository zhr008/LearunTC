using System.Collections.Generic;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:23 
    /// 描 述：文件权限管理 
    /// </summary> 
    public interface FileAuthIBLL
    {

        #region 获取数据
        /// <summary>
        /// 判断当前文件夹有没有上传权限
        /// </summary>
        /// <param name="id">文件夹主键</param>
        /// <returns></returns>
        bool IsUPLoad(string id);
        /// <summary>
        /// 获取授权信息列表
        /// </summary>
        /// <param name="F_FileInfoId">文件信息主键</param>
        /// <returns></returns>
        IEnumerable<FileAuthEntity> GetList(string F_FileInfoId);

        /// <summary>
        /// 实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        FileAuthEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void DeleteEntity(string keyValue);
        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体数据</param>
        bool SaveEntity(string keyValue, FileAuthEntity entity);
        #endregion
    }
}
