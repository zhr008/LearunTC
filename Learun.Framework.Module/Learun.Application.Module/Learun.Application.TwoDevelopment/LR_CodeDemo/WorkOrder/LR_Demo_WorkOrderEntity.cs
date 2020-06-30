using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-06-10 17:21
    /// 描 述：工单管理
    /// </summary>
    public class LR_Demo_WorkOrderEntity 
    {
        #region 实体成员
        /// <summary>
        /// 工单ID
        /// </summary>
        [Column("F_ID")]
        public string F_Id { get; set; }
        /// <summary>
        /// 工单编号
        /// </summary>
        [Column("F_CODE")]
        public string F_Code { get; set; }
        /// <summary>
        /// 生产部门
        /// </summary>
        [Column("F_DEPARTMENTID")]
        public string F_DepartmentId { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        [Column("F_MANAGERID")]
        public string F_ManagerId { get; set; }
        /// <summary>
        /// 制程ID
        /// </summary>
        [Column("F_PROCESS")]
        public string F_Process { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [Column("F_QTY")]
        public int? F_Qty { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [Column("F_STATUS")]
        public string F_Status { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        [Column("F_SPEC")]
        public string F_Spec { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
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
        #region 扩展字段
        #endregion
    }
}

