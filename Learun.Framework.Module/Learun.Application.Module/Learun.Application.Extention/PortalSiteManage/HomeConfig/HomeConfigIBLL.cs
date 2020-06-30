using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Extention.PortalSiteManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-04 09:35
    /// 描 述：门户网站首页配置
    /// </summary>
    public interface HomeConfigIBLL
    {
        #region 获取数据 
        /// <summary> 
        /// 获取LR_PS_HomeConfig表实体数据 
        /// <param name="keyValue">主键</param>
        /// <summary> 
        /// <returns></returns> 
        HomeConfigEntity GetEntity(string keyValue);
        /// <summary>
        /// 获取配置列表
        /// </summary>
        /// <param name="type">1.顶部文字2.底部文字3.底部地址4.logo图片5.微信图片6.顶部菜单7.底部菜单8.轮播图片9.模块 10底部logo</param>
        /// <returns></returns>
        IEnumerable<HomeConfigEntity> GetList(string type);
        /// <summary>
        /// 获取所有的配置列表
        /// </summary>
        /// <returns></returns>
        IEnumerable<HomeConfigEntity> GetALLList();
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
        void SaveEntity(string keyValue, HomeConfigEntity entity);


        /// <summary>
        /// 保存文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        void SaveText(string text, string type);
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strBase64">图片字串</param>
        /// <param name="fileNmae">文件名</param>
        /// <param name="fileExName">文件扩展名</param>
        /// <param name="type">类型</param>
        void SaveImg(string strBase64, string fileNmae, string fileExName, string type);
        /// <summary>
        /// 保存轮播图片
        /// </summary>
        /// <param name="strBase64">图片字串</param>
        /// <param name="fileNmae">文件名</param>
        /// <param name="fileExName">文件扩展名</param>
        /// <param name="keyValue">主键</param>
        void SaveImg2(string strBase64, string fileNmae, string fileExName, string keyValue, int sort);
        #endregion

        #region 扩展方法

        /// <summary>
        /// 获取顶部菜单树形数据
        /// </summary>
        /// <returns></returns>
        List<TreeModel> GetTree();
        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="type">类型</param>
        void GetImg(string type);

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        void GetImg2(string keyValue);
        #endregion
    }
}
