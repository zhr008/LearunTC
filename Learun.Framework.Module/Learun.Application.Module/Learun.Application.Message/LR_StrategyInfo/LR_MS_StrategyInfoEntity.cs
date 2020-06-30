using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Learun.Application.Message

{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-10-16 16:24
    /// 描 述：消息策略
    /// </summary>
    public class LR_MS_StrategyInfoEntity 
    {
        #region 实体成员
        /// <summary>
        /// 策略主键
        /// </summary>
        /// <returns></returns>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 策略名称
        /// </summary>
        /// <returns></returns>
        [Column("F_STRATEGYNAME")]
        public string F_StrategyName { get; set; }
        /// <summary>
        /// 策略编码
        /// </summary>
        /// <returns></returns>
        [Column("F_STRATEGYCODE")]
        public string F_StrategyCode { get; set; }
        /// <summary>
        /// 发送角色
        /// </summary>
        /// <returns></returns>
        [Column("F_SENDROLE")]
        public string F_SendRole { get; set; }
        /// <summary>
        /// 消息类型1邮箱2微信3短信4系统IM
        /// </summary>
        /// <returns></returns>
        [Column("F_MESSAGETYPE")]
        public string F_MessageType { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        /// <returns></returns>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <returns></returns>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户主键
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <returns></returns>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_CreateUserId = userInfo.userId;
            this.F_CreateUserName = userInfo.realName;
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue)
        {
            this.F_Id = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
    }
}

