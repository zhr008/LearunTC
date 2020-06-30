using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.Base.SystemModule
{

    /// <summary> 
     
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司 
    /// 创 建：超级管理员 
    /// 日 期：2018-07-30 14:53 
    /// 描 述：logo设置 
    /// </summary> 

    public class LogoImgBLL: LogoImgIBLL
    {
        private LogoImgService logoImgService = new LogoImgService();

        #region 获取数据 
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">条件参数</param>
        /// <returns></returns>
        public IEnumerable<LogoImgEntity> GetList(string queryJson)
        {
            try
            {
                return logoImgService.GetList(queryJson);
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
        /// 获取实体数据 
        /// </summary> 
        /// <param name="keyValue">主键</param> 
        /// <returns></returns> 
        public LogoImgEntity GetEntity(string keyValue)
        {
            try
            {
                return logoImgService.GetEntity(keyValue);
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
        /// 获取logo图片
        /// </summary>
        /// <param name="code">编码</param>
        public void GetImg(string code)
        {
            LogoImgEntity entity = GetEntity(code);
            string fileImg = "";
            string fileHeadImg = Config.GetValue("fileLogoImg");
            if (entity != null)
            {
                fileImg = string.Format("{0}/{1}{2}", fileHeadImg, entity.F_Code, entity.F_FileName);

            }
            else
            {
                fileImg = string.Format("{0}/{1}.png", fileHeadImg, code);
            }
            FileDownHelper.DownLoadnew(fileImg);
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
                logoImgService.DeleteEntity(keyValue);
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
        /// <param name="entity">实体</param> 

        public void SaveEntity(LogoImgEntity entity)
        {
            try
            {
                logoImgService.SaveEntity(entity);
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
    }
}
