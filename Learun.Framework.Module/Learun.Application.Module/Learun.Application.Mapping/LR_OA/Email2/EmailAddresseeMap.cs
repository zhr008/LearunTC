using Learun.Application.OA.Email2;
using System.Data.Entity.ModelConfiguration;

namespace LeaRun.Application.Mapping.PublicInfoManage
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2017.04.17
    /// 描 述：邮件
    /// </summary>
    public class EmailAddresseeMap : EntityTypeConfiguration<EmailAddresseeEntity>
    {
        public EmailAddresseeMap()
        {
            #region 表、主键
            //表
            this.ToTable("LR_OA_EMAILADDRESSEE");//Email_Addressee
            //主键
            this.HasKey(t => t.F_AddresseeId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
