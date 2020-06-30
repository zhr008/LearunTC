using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-02 12:03
    /// 描 述：图片保存
    /// </summary>
    public class ImgBLL: ImgIBLL
    {

        private ImgService imgService = new ImgService();

        #region 获取数据

        /// <summary> 
        /// 获取页面显示列表数据 
        /// </summary> 
        /// <param name="pagination">分页参数</param> 
        /// <param name="queryJson">查询参数</param> 
        /// <returns></returns> 
        public IEnumerable<ImgEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return imgService.GetPageList(pagination, queryJson);
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
        /// 获取LR_Base_Img表实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public ImgEntity GetEntity(string keyValue)
        {
            try
            {
                return imgService.GetEntity(keyValue);
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

        #endregion

        #region 提交数据 

        /// <summary> 
        /// 删除实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        public void DeleteEntity(string keyValue)
        {
            try
            {
                imgService.DeleteEntity(keyValue);
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
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <param name="entity">实体</param> 
        public void SaveEntity(string keyValue, ImgEntity entity)
        {
            try
            {
                imgService.SaveEntity(keyValue, entity);
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


        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void GetImg(string keyValue)
        {
            ImgEntity imgEntity = GetEntity(keyValue);

            if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
            {
                FileDownHelper.DownLoadBase64(imgEntity.F_Content.Replace("data:image/png;base64,", ""), imgEntity.F_Name);
            }
        }

    }
}
