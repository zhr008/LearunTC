using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2020-06-28 21:48
    /// 描 述：个人基本信息
    /// </summary>
    public class tc_PersonnelsEntity 
    {
        #region 实体成员
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
        /// 存档编码
        /// </summary>
        [Column("F_PLACECODE")]
        public string F_PlaceCode { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        [Column("F_APPLICANTID")]
        public string F_ApplicantId { get; set; }
        /// <summary>
        /// 证书编码
        /// </summary>
        [Column("F_CERTCODE")]
        public string F_CertCode { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [Column("F_GENDER")]
        public int? F_Gender { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [Column("F_AGE")]
        public int? F_Age { get; set; }
        /// <summary>
        /// 社保地
        /// </summary>
        [Column("F_PROVINCECODE")]
        public string F_ProvinceCode { get; set; }
        /// <summary>
        /// F_CityCode
        /// </summary>
        [Column("F_CITYCODE")]
        public string F_CityCode { get; set; }
        /// <summary>
        /// F_AreaCode
        /// </summary>
        [Column("F_AREACODE")]
        public string F_AreaCode { get; set; }
        /// <summary>
        /// F_Address
        /// </summary>
        [Column("F_ADDRESS")]
        public string F_Address { get; set; }
        /// <summary>
        /// 到场
        /// </summary>
        [Column("F_SCENETYPE")]
        public int? F_SceneType { get; set; }
        /// <summary>
        /// F_DeleteMark
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// F_Description
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// F_CreateDate
        /// </summary>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// F_CreateUserName
        /// </summary>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// F_CreateUserId
        /// </summary>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// F_ModifyDate
        /// </summary>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// F_ModifyUserName
        /// </summary>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// F_ModifyUserId
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
            this.F_PersonId = Guid.NewGuid().ToString();
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
            this.F_PersonId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

