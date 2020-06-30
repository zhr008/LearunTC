using Learun.Util;
using System;
using System.Collections.Generic;

namespace Learun.Application.IM
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.05.30
    /// 描 述：即时通讯用户注册
    /// </summary>
    public class IMSysUserBLL: IMSysUserIBLL
    {

        private IMSysUserService iMSysUserService = new IMSysUserService();
        private IMMsgService iMMsgService = new IMMsgService();

        #region 获取数据
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="keyWord">查询关键字</param>
        /// <returns></returns>
        public IEnumerable<IMSysUserEntity> GetList(string keyWord)
        {
            try
            {
                return iMSysUserService.GetList(keyWord);
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
        /// <param name="keyWord">查询关键字</param>
        /// <summary>
        /// <returns></returns>
        public IEnumerable<IMSysUserEntity> GetPageList(Pagination pagination, string keyWord)
        {
            try
            {
                return iMSysUserService.GetPageList(pagination,keyWord);
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
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public IMSysUserEntity GetEntity(string keyValue)
        {
            try
            {
                return iMSysUserService.GetEntity(keyValue);
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
        /// 删除实体数据(虚拟)
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        public void DeleteEntity(string keyValue)
        {
            try
            {
                iMSysUserService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, IMSysUserEntity entity)
        {
            try
            {
                iMSysUserService.SaveEntity(keyValue, entity);
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

        #region 验证数据
        /// <summary>
        /// 规则编号不能重复
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public bool ExistEnCode(string code, string keyValue)
        {
            try
            {
                return iMSysUserService.ExistEnCode(code, keyValue);
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
        /// 发送消息
        /// </summary>
        /// <param name="code">编码</param>
        /// <param name="userIdList">用户列表</param>
        /// <param name="content">消息内容</param>
        public void SendMsg(string code, List<string> userIdList, string content) {
            try
            {
                if (!string.IsNullOrEmpty(content) && userIdList.Count >0) {
                    IMSysUserEntity entity = iMSysUserService.GetEntityByCode(code);
                    if (entity != null) {
                        foreach (var userId in userIdList) {

                            IMMsgEntity iMMsgEntity = new IMMsgEntity();
                            iMMsgEntity.F_SendUserId = code;
                            iMMsgEntity.F_RecvUserId = userId;
                            iMMsgEntity.F_Content = content;
                            iMMsgService.SaveEntity(iMMsgEntity);

                            SendHubs.callMethod("sendMsg2", code, userId, content, 1);
                        }
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
        #endregion 
    }
}
