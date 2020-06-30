using Learun.Application.Base.AuthorizeModule;
using Learun.Application.IM;
using Learun.Application.Organization;
using Learun.Application.WeChat;
using Learun.Util;
using System;
using System.Collections.Generic;
namespace Learun.Application.Message
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-10-16 16:24
    /// 描 述：消息策略
    /// </summary>
    public class LR_StrategyInfoBLL : LR_StrategyInfoIBLL
    {
        private LR_StrategyInfoService lR_StrategyInfoService = new LR_StrategyInfoService();
        private UserRelationIBLL userRelationIBLL = new UserRelationBLL();
        private UserIBLL userIBLL = new UserBLL();

        private IMSysUserIBLL iMSysUserIBLL = new IMSysUserBLL();

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        public IEnumerable<LR_MS_StrategyInfoEntity> GetList( string queryJson )
        {
            try
            {
                return lR_StrategyInfoService.GetList(queryJson);
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
        public IEnumerable<LR_MS_StrategyInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            try
            {
                return lR_StrategyInfoService.GetPageList(pagination, queryJson);
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
        public LR_MS_StrategyInfoEntity GetEntity(string keyValue)
        {
            try
            {
                return lR_StrategyInfoService.GetEntity(keyValue);
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
        /// 根据策略编码获取策略
        /// </summary>
        /// <param name="code">策略编码</param>
        /// <returns></returns>
        public LR_MS_StrategyInfoEntity GetEntityByCode(string code)
        {
            try
            {
                return lR_StrategyInfoService.GetEntityByCode(code);
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
                lR_StrategyInfoService.DeleteEntity(keyValue);
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
        public void SaveEntity(string keyValue, LR_MS_StrategyInfoEntity entity)
        {
            try
            {
                lR_StrategyInfoService.SaveEntity(keyValue, entity);
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
        ///策略编码不能重复
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="F_StrategyCode">编码</param>
        /// <returns></returns>
        public bool ExistStrategyCode(string keyValue, string F_StrategyCode)
        {
            try
            {
                return lR_StrategyInfoService.ExistStrategyCode(keyValue,F_StrategyCode);
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

        #region 扩展方法（发送消息）
        /// <summary>
        /// 消息处理，在此处处理好数据，然后调用消息发送方法
        /// </summary>
        /// <param name="code">消息策略编码</param>
        /// <param name="content">消息内容</param>
        /// <param name="userlist">用户列表信息</param>
        /// <returns></returns>
        public ResParameter SendMessage(string code,string content,string userlist)
        {
            try
            {
                ResParameter resParameter = new ResParameter();
                if (string.IsNullOrEmpty(code))//判断code编码是否输入
                {
                    resParameter.code = ResponseCode.fail;
                    resParameter.info = "code编码为空";
                }
                else if (string.IsNullOrEmpty(content))//判断是否输入信息内容
                {
                    resParameter.code = ResponseCode.fail;
                    resParameter.info = "content内容为空";
                }
                else
                {
                    LR_MS_StrategyInfoEntity strategyInfoEntity = GetEntityByCode(code);//根据编码获取消息策略
                    if (strategyInfoEntity == null)//如果获取不到消息策略则code编码无效
                    {
                        resParameter.code = ResponseCode.fail;
                        resParameter.info = "code编码无效";
                    }
                    else
                    {
                        #region 用户信息处理
                        List<UserEntity> list = new List<UserEntity>();//消息发送对象
                        if (string.IsNullOrEmpty(userlist))
                        {
                            if (string.IsNullOrEmpty(strategyInfoEntity.F_SendRole))
                            {
                                resParameter.code = ResponseCode.fail;
                                resParameter.info = "消息策略无发送角色，需要输入人员userlist信息";
                            }
                            else
                            {
                                String[] rolecontent = strategyInfoEntity.F_SendRole.Split(',');//根据角色id获取用户信息
                                foreach(var item in rolecontent)
                                {
                                    var data = userRelationIBLL.GetUserIdList(item);
                                    string userIds = "";
                                    foreach (var items in data)
                                    {
                                        if (userIds != "")
                                        {
                                            userIds += ",";
                                        }
                                        userIds += items.F_UserId;
                                    }
                                    var userList = userIBLL.GetListByUserIds(userIds);
                                    foreach(var user in userList)
                                    {
                                        list.Add(user);
                                    }
                                }
                            }
                        }
                        else
                        {
                            list = userlist.ToList<UserEntity>();
                        }
                        #endregion
                        if (list.Count<=0)//判断用户列表有一个或一个以上的用户用于发送消息
                        {
                            resParameter.code = ResponseCode.fail;
                            resParameter.info = "找不到发送人员";
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(strategyInfoEntity.F_MessageType))
                            {
                                resParameter.code = ResponseCode.fail;
                                resParameter.info = "消息类型为空,无法发送消息";
                            }
                            else
                            {
                                string[] typeList = strategyInfoEntity.F_MessageType.Split(',');

                                foreach (var type in typeList) {
                                    switch (type)
                                    {
                                        case "1"://邮箱，调用邮箱发送方法
                                            EmailSend(content, list);
                                            break;
                                        case "2"://微信，调用微信发送方法
                                            WeChatSend(content, list);
                                            break;
                                        case "3": //短信，调用短信发送方法
                                            SMSSend(content, list);
                                            break;
                                        case "4": //系统IM，效用系统IM发送方法
                                            IMSend(content, list);
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }               
                }
                resParameter.code = ResponseCode.success;
                resParameter.info = "发送成功";

                return resParameter;
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
        /// 邮件发送
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="list">用户列表信息</param>
        /// <returns></returns>
        public void EmailSend(string content,List<UserEntity> list)
        {
            try
            {
                string SystemName = Config.GetValue("SystemName");//系统名称
                foreach (var item in list)
                {
                    if (!string.IsNullOrEmpty(item.F_Email))
                    {
                        MailHelper.Send(item.F_Email, SystemName + " - 发送消息", content.Replace("-", ""));
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
        /// 微信发送（企业号）
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="list">用户列表</param>
        /// <returns></returns>
        public void WeChatSend(string content, List<UserEntity> list)
        {
            string SystemName = Config.GetValue("SystemName");//系统名称
            string SalesManager = "";
            foreach (var item in list)
            {
                if (SalesManager != "")
                {
                    SalesManager += "|";
                }
                SalesManager += item.F_Account;
            }
            var text = new SendText()
            {
                agentid = Config.GetValue("ApplicationId"),//应用ID
                touser = SalesManager,//@all:所有人,多个用户名用 “|”隔开
                text = new SendText.SendItem()
                {
                    content = content
                }
            };
            MessageSendResult result = text.Send();//发送消息，并返回结果
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="list">用户列表</param>
        /// <returns></returns>
        public void SMSSend(string content, List<UserEntity> list)
        {   

        }
        /// <summary>
        /// 系统IM发送
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="list">用户列表</param>
        /// <returns></returns>
        public void IMSend(string content, List<UserEntity> list)
        {
            List<string> userList = new List<string>();
            foreach (var user in list) {
                userList.Add(user.F_UserId);
            }
            iMSysUserIBLL.SendMsg("IMSystem", userList, content);
        }
        #endregion
    }
}
