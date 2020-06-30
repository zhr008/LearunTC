using Learun.Util;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learun.Application.OA.LR_StampManage
{
    public class LR_StampManageEntity
    {

        /// <summary>
        /// 印章编号
        /// </summary>
        [Column("F_STAMPID")]
        public string F_StampId { get; set; }


        /// <summary>
        /// 印章名称
        /// </summary>
        [Column("F_STAMPNAME")]
        public string F_StampName { get; set; }

        /// <summary>
        /// 印章备注
        /// </summary>
        [Column("F_DESCRIPTION")]
        public string F_Description { get; set; }

        /// <summary>
        /// 印章分类
        /// </summary>
        [Column("F_STAMPTYPE")]
        public string F_StampType { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Column("F_PASSWORD")]
        public string F_Password { get; set; }

        /// <summary>
        /// 图片文件
        /// </summary>
        [Column("F_IMGFILE")]
        public string F_ImgFile { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Column("F_SORT")]
        public string F_Sort { get; set; }

        /// <summary>
        /// 印章状态
        /// </summary>
        [Column("F_ENABLEDMARK")]
        public int? F_EnabledMark { get; set; }

        #region 扩展方法

        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void Modify(string keyValue)
        {
            this.F_StampId = keyValue;
          
        }
        /// <summary>
        /// 新增调用
        /// </summary>
        public void Create()
        {
            this.F_StampId = Guid.NewGuid().ToString(); //产生印章编号
            this.F_EnabledMark = 1;//默认状态为启用
        }
        #endregion

    }
}
