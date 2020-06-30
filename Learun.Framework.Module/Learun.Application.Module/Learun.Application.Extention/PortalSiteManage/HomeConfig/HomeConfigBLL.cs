using Learun.Application.Base.SystemModule;
using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Extention.PortalSiteManage
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-04 09:35
    /// 描 述：门户网站首页配置
    /// </summary>
    public class HomeConfigBLL: HomeConfigIBLL
    {
        private HomeConfigService homeConfigService = new HomeConfigService();
        private ImgIBLL imgIBLL = new ImgBLL();
        #region 获取数据 
        /// <summary> 
        /// 获取LR_PS_HomeConfig表实体数据 
        /// <param name="keyValue">主键</param>
        /// <summary> 
        /// <returns></returns> 
        public HomeConfigEntity GetEntity(string keyValue)
        {
            try
            {
                return homeConfigService.GetEntity(keyValue);
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
        /// 获取实体根据类型
        /// </summary>
        /// <param name="type">1.顶部文字2.底部文字3.底部地址4.logo图片5.微信图片6.顶部菜单7.底部菜单8.轮播图片9.模块 10底部logo</param>
        /// <returns></returns>
        public HomeConfigEntity GetEntityByType(string type) {
            try
            {
                return homeConfigService.GetEntityByType(type);
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
        /// 获取配置列表
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        public IEnumerable<HomeConfigEntity> GetList(string type)
        {
            try
            {
                return homeConfigService.GetList(type);
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
        /// 获取所有的配置列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<HomeConfigEntity> GetALLList()
        {
            try
            {
                return homeConfigService.GetALLList();
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
                var entity = homeConfigService.GetEntity(keyValue);
                if (entity != null) {
                    homeConfigService.DeleteEntity(keyValue);

                    if (!string.IsNullOrEmpty(entity.F_Img))
                    {
                        imgIBLL.DeleteEntity(entity.F_Img);
                    }
                }
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
        /// 保存文字
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type"></param>
        public void SaveText(string text, string type) {
            HomeConfigEntity entity = GetEntityByType(type);
            string keyValue = null;
            if (entity == null)
            {
                entity = new HomeConfigEntity();
                entity.Create();
            }
            else {
                keyValue = entity.F_Id;
            }
            entity.F_Type = type;
            entity.F_Name = text;

            homeConfigService.SaveEntity(keyValue, entity);
        }
        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="strBase64">图片字串</param>
        /// <param name="fileNmae">文件名</param>
        /// <param name="fileExName">文件扩展名</param>
        /// <param name="type">类型</param>
        public void SaveImg(string strBase64,string fileNmae,string fileExName, string type)
        {
            HomeConfigEntity entity = GetEntityByType(type);
            string keyValue = null;
            if (entity == null)
            {
                entity = new HomeConfigEntity();
                entity.Create();
                entity.F_Type = type;
            }
            else
            {
                keyValue = entity.F_Id;
            }

            string imgKey = null;
            ImgEntity imgEntity = null;
            if (!string.IsNullOrEmpty(entity.F_Img))
            {
                imgEntity = imgIBLL.GetEntity(entity.F_Img);
                if (imgEntity != null)
                {
                    imgKey = entity.F_Img;
                }
                else
                {
                    imgEntity = new ImgEntity();
                }
            }
            else {
                imgEntity = new ImgEntity();
            }

            imgEntity.F_Content = strBase64;
            imgEntity.F_Name = fileNmae;
            imgEntity.F_ExName = fileExName;

            imgIBLL.SaveEntity(imgKey, imgEntity);

            entity.F_Img = imgEntity.F_Id;
            homeConfigService.SaveEntity(keyValue, entity);
        }
        /// <summary>
        /// 保存轮播图片
        /// </summary>
        /// <param name="strBase64">图片字串</param>
        /// <param name="fileNmae">文件名</param>
        /// <param name="fileExName">文件扩展名</param>
        /// <param name="keyValue">主键</param>
        public void SaveImg2(string strBase64, string fileNmae, string fileExName, string keyValue,int sort) {
            HomeConfigEntity entity = null;
            if (string.IsNullOrEmpty(keyValue))
            {
                entity = new HomeConfigEntity();
                entity.Create();
                entity.F_Type = "8";
            }
            else
            {
                entity = GetEntity(keyValue);

                if (entity == null) {
                    entity = new HomeConfigEntity();
                    entity.F_Id = keyValue;
                    entity.F_Type = "8";

                    keyValue = "";
                }

            }
            entity.F_Sort = sort;

            string imgKey = null;
            ImgEntity imgEntity = null;
            if (!string.IsNullOrEmpty(entity.F_Img))
            {
                imgEntity = imgIBLL.GetEntity(entity.F_Img);
                if (imgEntity != null)
                {
                    imgKey = entity.F_Img;
                }
                else
                {
                    imgEntity = new ImgEntity();
                }
            }
            else
            {
                imgEntity = new ImgEntity();
            }

            imgEntity.F_Content = strBase64;
            imgEntity.F_Name = fileNmae;
            imgEntity.F_ExName = fileExName;

            imgIBLL.SaveEntity(imgKey, imgEntity);

            entity.F_Img = imgEntity.F_Id;
            homeConfigService.SaveEntity(keyValue, entity);
        }


        /// <summary> 
        /// 保存实体数据（新增、修改） 
        /// <param name="keyValue">主键</param> 
        /// <summary> 
        /// <returns></returns> 
        public void SaveEntity(string keyValue, HomeConfigEntity entity)
        {
            try
            {
                homeConfigService.SaveEntity(keyValue, entity);
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
        /// 获取顶部菜单树形数据
        /// </summary>
        /// <returns></returns>
        public List<TreeModel> GetTree()
        {
            List<HomeConfigEntity> alllist= (List<HomeConfigEntity>)GetList("6");
            List<HomeConfigEntity> list = alllist.FindAll(t=>t.F_Img == "0" || t.F_Img == "1");

            List<TreeModel> treeList = new List<TreeModel>();
            foreach (var item in list)
            {
                TreeModel node = new TreeModel();
                node.id = item.F_Id;
                node.text = item.F_Name;
                node.value = item.F_Id;
                node.showcheck = false;
                node.checkstate = 0;
                node.isexpand = true;
                node.parentId = item.F_ParentId;
                treeList.Add(node);
            }
            return treeList.ToTree();
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="type">类型</param>
        public void GetImg(string type)
        {
            HomeConfigEntity entity = GetEntityByType(type);
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
                img = "/Content/images/plhome/addImg.png";
            }

            if (string.IsNullOrEmpty(img))
            {
                img = "/Content/images/plhome/addImg.png";
            }
            FileDownHelper.DownLoad(img);
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void GetImg2(string keyValue)
        {
            HomeConfigEntity entity = GetEntity(keyValue);
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
                img = "/Content/images/plhome/addImg.png";
            }

            if (string.IsNullOrEmpty(img))
            {
                img = "/Content/images/plhome/addImg.png";
            }
            FileDownHelper.DownLoad(img);
        }
        #endregion
    }
}
