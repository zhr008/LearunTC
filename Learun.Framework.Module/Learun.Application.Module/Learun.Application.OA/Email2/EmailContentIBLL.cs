using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.OA.Email2
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件内容
    /// </summary>
    public interface EmailContentIBLL
    {
        #region 提交数据
        /// <summary>
        /// 收件箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetAddresseeMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 草稿箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetDraftMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 已发送
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetSentMail(Pagination pagination, string userId, string keyword);
        /// <summary>
        /// 回收箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        IEnumerable<EmailContentEntity> GetRecycleMail(Pagination pagination, string userId, string keyword);

        /// <summary>
        /// 邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        EmailContentEntity GetEntity(string keyValue);
        #endregion

        #region 提交数据
        /// <summary>
        /// 保存邮件表单（发送、存入草稿、草稿编辑）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailContentEntity">邮件实体</param>
        /// <param name="addresssIds">收件人</param>
        /// <param name="copysendIds">抄送人</param>
        /// <param name="bccsendIds">密送人</param>
        /// <returns></returns>
        void SaveForm(string keyValue, EmailContentEntity emailContentEntity, string addresssIds, string copysendIds, string bccsendIds);

        /// <summary>
        /// 彻底删除邮件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="emailType">邮件类型：unreadMail  starredMail  draftMail  recycleMail  addresseeMail  sendMail</param>
        void ThoroughRemoveForm(string keyValue, string emailType);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="emailType">邮件类型：unreadMail  starredMail  draftMail  recycleMail  addresseeMail  sendMail</param>
        void RemoveForm(string keyValue, string emailType);
        #endregion
    }
}
