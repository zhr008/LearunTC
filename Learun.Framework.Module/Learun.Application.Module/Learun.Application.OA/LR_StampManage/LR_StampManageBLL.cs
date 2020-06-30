using Learun.Application.Base.SystemModule;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Linq;



namespace Learun.Application.OA.LR_StampManage
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组（王飞）
    /// 日 期：2018.11.19
    /// 描 述：印章管理（接口）
    /// </summary>
    public class LR_StampManageBLL : LR_StampManageIBLL
    {
        private LR_StampManageService lr_StampManageService = new LR_StampManageService();
        private ImgIBLL imgIBLL = new ImgBLL();
        #region 获取数据

        /// <summary>
        /// 获取所有的印章信息/模糊查询（根据名称/状态（启用或者停用））
        /// </summary>
        /// <param name="keyWord">查询的关键字</param>
        /// <returns></returns>
        public IEnumerable<LR_StampManageEntity> GetList(string keyWord)
        {
            try
            {
                return lr_StampManageService.GetList(keyWord);

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
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_StampManageEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {

                return lr_StampManageService.GetPageList(pagination, queryJson);
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
        /// 获取实体
        /// </summary>
        /// <param name="parentId">主键</param>
        /// <returns></returns>
        public LR_StampManageEntity GetEntity(string keyValue)
        {
            try
            {
                return lr_StampManageService.GetEntity(keyValue);
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
        /// 保存印章信息（新增/编辑）
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="stampEntity"></param>
        public void SaveEntity(string keyValue, LR_StampManageEntity entity)
        {
            try
            {
                lr_StampManageService.SaveEntity(keyValue, entity);
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
        /// 删除印章信息
        /// </summary>
        /// <param name="keyVlaue"></param>
        public void DeleteEntity(string keyVlaue)
        {
            try
            {
                lr_StampManageService.DeleteEntity(keyVlaue);
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
            //首先获取实体
            LR_StampManageEntity entity = GetEntity(keyValue);
            string img = "";
            //实体是否存在
            if (entity != null && !string.IsNullOrEmpty(entity.F_ImgFile))
            {
                ImgEntity imgEntity = imgIBLL.GetEntity(entity.F_ImgFile);

                if (imgEntity != null && !string.IsNullOrEmpty(imgEntity.F_Content))
                {
                    FileDownHelper.DownLoadBase64(imgEntity.F_Content, imgEntity.F_Name);
                    return;
                }
            }
            else
            {
                img = "/Content/images/add.jpg";
            }
            if (string.IsNullOrEmpty(img))
            {
                img = "/Content/images/add.jpg";
            }
            FileDownHelper.DownLoad(img);
        }

        /// <summary>
        /// 更新数据状态
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="state">状态 1启用 0禁用</param>
        public void UpdateState(string keyValue, int state)
        {
            LR_StampManageEntity entity = new LR_StampManageEntity();
            entity.F_EnabledMark = state;
            SaveEntity(keyValue, entity);
        }

        /// <summary>
        /// 密码验证
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public bool EqualPassword(string keyValue, string password)
        {
             LR_StampManageEntity entity = GetEntity(keyValue);
            if (entity.F_Password.Equals(password))//加密后进行对比
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
