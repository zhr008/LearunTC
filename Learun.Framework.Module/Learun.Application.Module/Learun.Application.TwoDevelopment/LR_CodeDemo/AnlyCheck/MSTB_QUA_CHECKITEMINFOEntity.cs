using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.TwoDevelopment.LR_CodeDemo
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.3 力软敏捷开发框架
    /// Copyright (c) 2013-2018 上海力软信息技术有限公司
    /// 创 建：超级管理员
    /// 日 期：2019-01-09 10:50
    /// 描 述：检查项目
    /// </summary>
    public class MSTB_QUA_CHECKITEMINFOEntity 
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        [Column("ID")]
        public Guid ID { get; set; }
        /// <summary>
        /// HeaderID
        /// </summary>
        [Column("HEADERID")]
        public Guid HeaderID { get; set; }
        /// <summary>
        /// OrderNo
        /// </summary>
        [Column("ORDERNO")]
        public int? OrderNo { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [Column("NAME")]
        public string Name { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        [Column("TYPE")]
        public int? Type { get; set; }
        /// <summary>
        /// BaseCode
        /// </summary>
        [Column("BASECODE")]
        public string BaseCode { get; set; }
        /// <summary>
        /// ResultNum
        /// </summary>
        [Column("RESULTNUM")]
        public int? ResultNum { get; set; }
        /// <summary>
        /// GroupType
        /// </summary>
        [Column("GROUPTYPE")]
        public int? GroupType { get; set; }
        /// <summary>
        /// GroupTime
        /// </summary>
        [Column("GROUPTIME")]
        public int? GroupTime { get; set; }
        /// <summary>
        /// Class
        /// </summary>
        [Column("CLASS")]
        public string Class { get; set; }
        /// <summary>
        /// ClassOrderNo
        /// </summary>
        [Column("CLASSORDERNO")]
        public int? ClassOrderNo { get; set; }
        /// <summary>
        /// Uom
        /// </summary>
        [Column("UOM")]
        public string Uom { get; set; }
        /// <summary>
        /// Last_Act_Id
        /// </summary>
        [Column("LAST_ACT_ID")]
        public string Last_Act_Id { get; set; }
        /// <summary>
        /// Last_Act_Time
        /// </summary>
        [Column("LAST_ACT_TIME")]
        public DateTime? Last_Act_Time { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        [Column("REMARK")]
        public string Remark { get; set; }
        /// <summary>
        /// Max
        /// </summary>
        [Column("MAX")]
        public string Max { get; set; }
        /// <summary>
        /// Min
        /// </summary>
        [Column("MIN")]
        public string Min { get; set; }
        /// <summary>
        /// Stander
        /// </summary>
        [Column("STANDER")]
        public string Stander { get; set; }
        /// <summary>
        /// IsRequired
        /// </summary>
        [Column("ISREQUIRED")]
        public int? IsRequired { get; set; }
        /// <summary>
        /// Equipments
        /// </summary>
        [Column("EQUIPMENTS")]
        public string Equipments { get; set; }
        /// <summary>
        /// CategoryId
        /// </summary>
        [Column("CATEGORYID")]
        public Guid CategoryId { get; set; }
        /// <summary>
        /// Accuracy
        /// </summary>
        [Column("ACCURACY")]
        public string Accuracy { get; set; }
        /// <summary>
        /// RecordStatus
        /// </summary>
        [Column("RECORDSTATUS")]
        public int? RecordStatus { get; set; }
        /// <summary>
        /// RecordLastEditDt
        /// </summary>
        [Column("RECORDLASTEDITDT")]
        public DateTime? RecordLastEditDt { get; set; }
        /// <summary>
        /// RecordGuid
        /// </summary>
        [Column("RECORDGUID")]
        public string RecordGuid { get; set; }
        /// <summary>
        /// Segment
        /// </summary>
        [Column("SEGMENT")]
        public string Segment { get; set; }
        /// <summary>
        /// Name2
        /// </summary>
        [Column("NAME2")]
        public string Name2 { get; set; }
        /// <summary>
        /// Name3
        /// </summary>
        [Column("NAME3")]
        public string Name3 { get; set; }
        /// <summary>
        /// Name4
        /// </summary>
        [Column("NAME4")]
        public string Name4 { get; set; }
        /// <summary>
        /// Last_Act_Name
        /// </summary>
        [Column("LAST_ACT_NAME")]
        public string Last_Act_Name { get; set; }
        /// <summary>
        /// Name5
        /// </summary>
        [Column("NAME5")]
        public string Name5 { get; set; }
        /// <summary>
        /// Name6
        /// </summary>
        [Column("NAME6")]
        public string Name6 { get; set; }
        /// <summary>
        /// Name7
        /// </summary>
        [Column("NAME7")]
        public string Name7 { get; set; }
        /// <summary>
        /// Name8
        /// </summary>
        [Column("NAME8")]
        public string Name8 { get; set; }
        /// <summary>
        /// IsAvg
        /// </summary>
        [Column("ISAVG")]
        public int? IsAvg { get; set; }
        /// <summary>
        /// IsRSD
        /// </summary>
        [Column("ISRSD")]
        public int? IsRSD { get; set; }
        /// <summary>
        /// IsStVar
        /// </summary>
        [Column("ISSTVAR")]
        public int? IsStVar { get; set; }
        /// <summary>
        /// IsSum
        /// </summary>
        [Column("ISSUM")]
        public int? IsSum { get; set; }
        /// <summary>
        /// Name9
        /// </summary>
        [Column("NAME9")]
        public string Name9 { get; set; }
        /// <summary>
        /// Name10
        /// </summary>
        [Column("NAME10")]
        public string Name10 { get; set; }
        /// <summary>
        /// EditStatus
        /// </summary>
        [Column("EDITSTATUS")]
        public string EditStatus { get; set; }
        /// <summary>
        /// IsDefaultRow
        /// </summary>
        [Column("ISDEFAULTROW")]
        public int? IsDefaultRow { get; set; }
        /// <summary>
        /// DefaulRowNum
        /// </summary>
        [Column("DEFAULROWNUM")]
        public int? DefaulRowNum { get; set; }
        /// <summary>
        /// ControlRows
        /// </summary>
        [Column("CONTROLROWS")]
        public string ControlRows { get; set; }
        /// <summary>
        /// QCFlag
        /// </summary>
        [Column("QCFLAG")]
        public string QCFlag { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.ID = Guid.NewGuid();
        }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public void Modify(Guid keyValue)
        {
            this.ID = keyValue;
        }
        #endregion
        #region 扩展字段
        #endregion
    }
}

