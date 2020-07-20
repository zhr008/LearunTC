using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-05 19:59
    /// 描 述：培训记录
    /// </summary>
    public class tc_PersonnelTrainEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_PersonnelTrainId
        /// </summary>
        [Column("F_PERSONNELTRAINID")]
        public string F_PersonnelTrainId { get; set; }
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
        /// 个人信息
        /// </summary>
        [Column("F_PERSONID")]
        public string F_PersonId { get; set; }
        /// <summary>
        /// 证书类型
        /// </summary>
        [Column("F_CERTTYPE")]
        public int? F_CertType { get; set; }
        /// <summary>
        /// 专业序列
        /// </summary>
        [Column("F_MAJORTYPE")]
        public int? F_MajorType { get; set; }
        /// <summary>
        /// 证书专业
        /// </summary>
        [Column("F_MAJOR")]
        public string F_Major { get; set; }
        /// <summary>
        /// 发证机构
        /// </summary>
        [Column("F_CERTORGANIZATION")]
        public string F_CertOrganization { get; set; }
        /// <summary>
        /// 资格发证日
        /// </summary>
        [Column("F_CERTDATEBEGIN")]
        public DateTime? F_CertDateBegin { get; set; }
        /// <summary>
        /// 资格失效日
        /// </summary>
        [Column("F_CERTDATEEND")]
        public DateTime? F_CertDateEnd { get; set; }
        /// <summary>
        /// 资格证形式
        /// </summary>
        [Column("F_CERTSTYLE")]
        public int? F_CertStyle { get; set; }
        /// <summary>
        /// 库存状态
        /// </summary>
        [Column("F_CERTSTATUS")]
        public int? F_CertStatus { get; set; }
        /// <summary>
        /// 培训状态
        /// </summary>
        [Column("F_TRAINSTATUS")]
        public int? F_TrainStatus { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        [Column("F_CHECKINDATE")]
        public DateTime? F_CheckInDate { get; set; }
        /// <summary>
        /// 报名截止日期
        /// </summary>
        [Column("F_APPLYDATE")]
        public DateTime? F_ApplyDate { get; set; }
        /// <summary>
        /// 预计培训日期
        /// </summary>
        [Column("F_EXPECTEDTRAINDATE")]
        public DateTime? F_ExpectedTrainDate { get; set; }
        /// <summary>
        /// 报考收费标准
        /// </summary>
        [Column("F_FEESTANDARD")]
        public string F_FeeStandard { get; set; }
        /// <summary>
        /// 报考收费状态
        /// </summary>
        [Column("F_TRAINCOLLECTSTATUS")]
        public int? F_TrainCollectStatus { get; set; }
        /// <summary>
        /// 付费代理机构
        /// </summary>
        [Column("F_TRAINORGNAME")]
        public string F_TrainOrgName { get; set; }
        /// <summary>
        /// 代理机构开户名
        /// </summary>
        [Column("F_TRAINORGACCOUNTNAME")]
        public string F_TrainOrgAccountName { get; set; }
        /// <summary>
        /// 代理机构开户行
        /// </summary>
        [Column("F_TRAINORGBANKNAME")]
        public string F_TrainOrgBankName { get; set; }
        /// <summary>
        /// 代理机构银行账号
        /// </summary>
        [Column("F_TRAINORGBANKACCOUNT")]
        public string F_TrainOrgBankAccount { get; set; }
        /// <summary>
        /// 付费金额
        /// </summary>
        [Column("F_TRAINPAYAMOUNT")]
        public int? F_TrainPayAmount { get; set; }
        /// <summary>
        /// 付费凭证
        /// </summary>
        [Column("F_TRAINPAYVOUCHER")]
        public int? F_TrainPayVoucher { get; set; }
        /// <summary>
        /// F_TrainPayStatus
        /// </summary>
        [Column("F_TRAINPAYSTATUS")]
        public int? F_TrainPayStatus { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }
        /// <summary>
        /// 删除标记
        /// </summary>
        [Column("F_DELETEMARK")]
        public int? F_DeleteMark { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        [Column("F_CREATEDATE")]
        public DateTime? F_CreateDate { get; set; }
        /// <summary>
        /// 创建用户
        /// </summary>
        [Column("F_CREATEUSERNAME")]
        public string F_CreateUserName { get; set; }
        /// <summary>
        /// 创建用户主键
        /// </summary>
        [Column("F_CREATEUSERID")]
        public string F_CreateUserId { get; set; }
        /// <summary>
        /// 修改日期
        /// </summary>
        [Column("F_MODIFYDATE")]
        public DateTime? F_ModifyDate { get; set; }
        /// <summary>
        /// 修改用户
        /// </summary>
        [Column("F_MODIFYUSERNAME")]
        public string F_ModifyUserName { get; set; }
        /// <summary>
        /// 修改用户主键
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
            this.F_PersonnelTrainId = Guid.NewGuid().ToString();
            this.F_CreateDate = DateTime.Now;
            this.F_DeleteMark = 0;
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
            this.F_PersonnelTrainId = keyValue;
            this.F_ModifyDate = DateTime.Now;
            UserInfo userInfo = LoginUserInfo.Get();
            this.F_ModifyUserId = userInfo.userId;
            this.F_ModifyUserName = userInfo.realName;
        }
        #endregion
        #region 扩展字段
        #endregion
    }


    [NotMapped]
    public class PersonnelTrainInfo : tc_PersonnelTrainEntity
    {
        [NotMapped]
        public string F_ApplicantId { get; set; }
    }
}

