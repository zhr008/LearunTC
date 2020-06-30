using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.WorkFlow
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.12.06
    /// 描 述：工作流委托规则(新)
    /// </summary>
    public class NWFDelegateRuleEntity
    {
        #region 实体成员 
        /// <summary> 
        /// 主键 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary> 
        /// 被委托人Id 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TOUSERID")]
        public string F_ToUserId { get; set; }
        /// <summary> 
        /// 被委托人名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_TOUSERNAME")]
        public string F_ToUserName { get; set; }
        /// <summary> 
        /// 委托开始时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_BEGINDATE")]
        public DateTime? F_BeginDate { get; set; }
        /// <summary> 
        /// 委托结束时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ENDDATE")]
        public DateTime? F_EndDate { get; set; }
        /// <summary> 
        /// 委托人Id 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary> 
        /// 委托人名称 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary> 
        /// 创建时间 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary> 
        /// 有效标志1有效 0 无效 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }
        /// <summary> 
        /// 备注 
        /// </summary> 
        /// <returns></returns> 
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        #endregion

        #region 扩展操作 
        /// <summary> 
        /// 新增调用 
        /// </summary> 
        public void Create()
        {
            this.F_Id = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_EnabledMark = 1;
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
        }
        #endregion
    }
}
