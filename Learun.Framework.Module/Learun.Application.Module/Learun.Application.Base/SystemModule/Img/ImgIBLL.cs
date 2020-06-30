using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-02 12:03
    /// 描 述：图片保存
    /// </summary>
    public interface ImgIBLL
    {
        #region 获取数据

        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        IEnumerable<ImgEntity> GetPageList(Pagination pagination, string queryJson);

        /// <summary> 
        /// 获取LR_Base_Img表实体数据
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        ImgEntity GetEntity(string keyValue);

        #endregion

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        void DeleteEntity(string keyValue);

        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        void SaveEntity(string keyValue, ImgEntity entity);

        #endregion

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        void GetImg(string keyValue);
    }
}
