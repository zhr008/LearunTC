using Learun.Application.Base.SystemModule;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Extention.PortalSiteManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-02 09:35
    /// 描 述：门户网站页面配置
    /// </summary>
    public class PageBLL: PageIBLL
    {
        private PageService pageService = new PageService();
        private ImgIBLL imgIBLL = new ImgBLL();

        #region 获取数据 

        /// <summary> 
        /// 获取页面显示列表数据 
        /// <summary> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public IEnumerable<PageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return pageService.GetPageList(pagination, queryJson);
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
        /// 获取页面显示列表数据 
        /// <summary>
        /// <returns></returns> 
        public IEnumerable<PageEntity> GetList()
        {
            try
            {
                return pageService.GetList();
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
        /// 获取LR_PS_Page表实体数据 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public PageEntity GetEntity(string keyValue)
        {
            try
            {
                return pageService.GetEntity(keyValue);
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
                pageService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, PageEntity entity)
        {
            try
            {
                pageService.SaveEntity(keyValue, entity);
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


        #region 扩展方法

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void GetImg(string keyValue)
        {
            PageEntity entity = GetEntity(keyValue);
            string img = "";
            if (entity != null && !string.IsNullOrEmpty(entity.F_Img))
            {
                ImgEntity imgEntity = imgIBLL.GetEntity(entity.F_Img);

                if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
                {
                    FileDownHelper.DownLoadBase64(imgEntity.F_Content, imgEntity.F_Name);
                    return;
                }
            }
            else
            {
                img = "/Content/images/plhome/添加图片.jpg";
            }

            if (string.IsNullOrEmpty(img))
            {
                img = "/Content/images/plhome/添加图片.jpg";
            }
            FileDownHelper.DownLoad(img);
        }


        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void GetImg2(string keyValue)
        {
            PageEntity entity = GetEntity(keyValue);
            string img = "";
            if (entity != null && !string.IsNullOrEmpty(entity.F_Img))
            {
                ImgEntity imgEntity = imgIBLL.GetEntity(entity.F_Img);

                if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
                {
                    FileDownHelper.DownLoadBase64(imgEntity.F_Content, imgEntity.F_Name);
                    return;
                }
            }
            else
            {
                img = "/Content/images/plhome/banner_df.jpg";
            }

            if (string.IsNullOrEmpty(img))
            {
                img = "/Content/images/plhome/banner_df.jpg";
            }
            FileDownHelper.DownLoad(img);
        }
        #endregion

    }
}
