using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.Files
{
    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2019-11-28 09:23 
    /// 描 述：文件夹管理 
    /// </summary> 
    public interface FolderIBLL
    {
        #region 获取数据 
        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="keyWord">关键字</param>
        /// <param name="pId">父级id</param>
        /// <returns></returns>
        IEnumerable<FolderEntity> GetList(string keyWord, string pId);

        /// <summary> 
        /// 获取lr_base_folder表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        FolderEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取树形数据
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetTree();
        #endregion

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        bool DeleteEntity(string keyValue);

        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        void SaveEntity(string keyValue, FolderEntity entity);
        #endregion
    }
}
