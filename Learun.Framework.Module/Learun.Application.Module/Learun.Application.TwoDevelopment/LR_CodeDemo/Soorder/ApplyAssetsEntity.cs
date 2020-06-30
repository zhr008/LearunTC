using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-29 15:28
    /// 描 述：销售订单
    /// </summary>
    public class ApplyAssetsEntity 
    {
        #region 实体成员
        /// <summary>
        /// F_Id
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        [Column("F_TITLE")]
        public string F_Title { get; set; }
        /// <summary>
        /// 紧急程度
        /// </summary>
        [Column("F_DEGREE")]
        public string F_Degree { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        [Column("F_APPLICANT")]
        public string F_Applicant { get; set; }
        /// <summary>
        /// 申请人部门
        /// </summary>
        [Column("F_DEPT")]
        public string F_Dept { get; set; }
        /// <summary>
        /// 资产名称
        /// </summary>
        [Column("F_ASSETNAME")]
        public string F_AssetName { get; set; }
        /// <summary>
        /// 资产数量
        /// </summary>
        [Column("F_ASSETQTY")]
        public int? F_AssetQty { get; set; }
        /// <summary>
        /// 资产品牌
        /// </summary>
        [Column("F_ASSETBRAND")]
        public string F_AssetBrand { get; set; }
        /// <summary>
        /// 规格及型号
        /// </summary>
        [Column("F_SPECIFICATION")]
        public string F_Specification { get; set; }
        /// <summary>
        /// 需要日期
        /// </summary>
        [Column("F_REQUIREDDATE")]
        public DateTime? F_RequiredDate { get; set; }
        /// <summary>
        /// 使用原因
        /// </summary>
        [Column("F_REQUIREDREASON")]
        public string F_RequiredReason { get; set; }
        /// <summary>
        /// 相关流程
        /// </summary>
        [Column("F_PROCESS")]
        public string F_Process { get; set; }
        /// <summary>
        /// 相关文档
        /// </summary>
        [Column("F_FILE")]
        public string F_File { get; set; }
        /// <summary>
        /// 相关项目
        /// </summary>
        [Column("F_PROJECT")]
        public string F_Project { get; set; }
        /// <summary>
        /// 相关客户
        /// </summary>
        [Column("F_CUSTOMER")]
        public string F_Customer { get; set; }
        /// <summary>
        /// 签字意见
        /// </summary>
        [Column("F_OPINION")]
        public string F_Opinion { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create(UserInfo userInfo)
      {
            this.F_Id = Guid.NewGuid().ToString();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(string keyValue, UserInfo userInfo)
        {
            this.F_Id = keyValue;
        }
        #endregion
    }
}

