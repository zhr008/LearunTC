using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Extention.PortalSiteManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-02 09:35
    /// 描 述：门户网站页面配置
    /// </summary>
    public interface PageIBLL
    {
        #region 获取数据

        /// <summary> 
        /// 获取页面显示列表数据 
        /// <summary> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        IEnumerable<PageEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary> 
        /// 获取页面显示列表数据 
        /// <summary>
        /// <returns></returns> 
        IEnumerable<PageEntity> GetList();
        /// <summary> 
        /// 获取LR_PS_Page表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        PageEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        void DeleteEntity(string keyValue);
        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        void SaveEntity(string keyValue, PageEntity entity);
        #endregion
        #region 扩展方法
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        void GetImg(string keyValue);

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        void GetImg2(string keyValue);
        #endregion
    }
}
