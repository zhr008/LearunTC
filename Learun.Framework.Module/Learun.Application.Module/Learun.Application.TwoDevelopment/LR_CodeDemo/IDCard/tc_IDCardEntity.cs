using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-06-29 21:15
    /// 描 述：身份证管理
    /// </summary>
    public class tc_IDCardEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_IDCardId
        /// </summary>
        [Column("F_IDCARDID")]
        public string F_IDCardId { get; set; }
        /// <summary>
        /// F_PersonId
        /// </summary>
        [Column("F_PERSONID")]
        public string F_PersonId { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [Column("F_USERNAME")]
        public string F_UserName { get; set; }
        /// <summary>
        /// 身份证号码
        /// </summary>
        [Column("F_IDCARDNO")]
        public string F_IDCardNo { get; set; }
        /// <summary>
        /// 省
        /// </summary>
        [Column("F_PROVINCEID")]
        public string F_ProvinceId { get; set; }
        /// <summary>
        /// 市
        /// </summary>
        [Column("F_CITYID")]
        public string F_CityId { get; set; }
        /// <summary>
        /// 区
        /// </summary>
        [Column("F_COUNTYID")]
        public string F_CountyId { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [Column("F_ADDRESS")]
        public string F_Address { get; set; }
        /// <summary>
        /// 发证时间
        /// </summary>
        [Column("F_ISSUEDATE")]
        public DateTime? F_IssueDate { get; set; }
        /// <summary>
        /// 失效时间
        /// </summary>
        [Column("F_EXPIRATIONDATE")]
        public DateTime? F_ExpirationDate { get; set; }
        /// <summary>
        /// 保管
        /// </summary>
        [Column("F_SAFEGUARDTYPE")]
        public int? F_SafeguardType { get; set; }
        /// <summary>
        /// 入库时间
        /// </summary>
        [Column("F_WAREHOUSEDATE")]
        public DateTime? F_WarehouseDate { get; set; }
        /// <summary>
        /// 删除
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 更新人
        /// </summary>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// 更新人Id
        /// </summary>
        [Column("F_MODIFYUSERID")]
        public string F_ModifyUserId { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_IDCardId = Guid.NewGuid().ToString();
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
            this.F_IDCardId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }

        #endregion
    }
}

