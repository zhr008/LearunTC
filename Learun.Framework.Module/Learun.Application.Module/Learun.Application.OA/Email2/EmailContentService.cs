using Learun.DataBase.Repository;
using Learun.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Learun.Application.OA.Email2
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件内容
    /// </summary>
    public class EmailContentService : RepositoryFactory
    {
        #region 获取数据
        /// <summary>
        /// 未读邮件
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetUnreadMail(Pagination pagination, string userId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.F_AddresseeId AS F_ContentId ,
                                    c.F_Theme ,
                                    c.F_ThemeColor ,
                                    c.F_SenderId ,
                                    c.F_SenderName ,
                                    c.F_SenderTime ,
                                    c.F_SendPriority,
                                    a.F_IsHighlight,
                                    a.F_CreateDate
                            FROM    Email_Addressee a
                                    LEFT JOIN Email_Content c ON c.F_ContentId = a.F_ContentId
                            WHERE   a.F_RecipientId = @userId AND a.F_IsRead <> 1");
            strSql.Append(" AND a.F_DeleteMark = 0");
            //var parameter = new List<DbParameter>();
            //parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            string theme = "";
            if (!keyword.IsEmpty())
            {
                strSql.Append(" AND c.F_Theme like @theme");
                theme = '%' + keyword + '%';
                //parameter.Add(DbParameters.CreateDbParameter("@Theme", '%' + keyword + '%'));
            }
            return this.BaseRepository().FindList<EmailContentEntity>(strSql.ToString(),new { userId,theme }, pagination);
        }
        /// <summary>
        /// 草稿箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetDraftMail(Pagination pagination, string userId, string keyword)
        {
            var expression = LinqExtensions.True<EmailContentEntity>();
            expression = expression.And(t => t.F_SendState == 0);
            expression = expression.And(t => t.F_CreateUserId == userId);
            if (!keyword.IsEmpty())
            {
                expression = expression.And(t => t.F_Theme.Contains(keyword));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 回收箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetRecycleMail(Pagination pagination, string userId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  *
                            FROM    ( SELECT    a.F_AddresseeId AS F_ContentId ,
                                                c.F_Theme ,
                                                c.F_ThemeColor ,
                                                c.F_SenderId ,
                                                c.F_SenderName ,
                                                c.F_SenderTime ,
                                                a.F_CreateDate,
                                                a.F_ContentId as F_ParentId
                                      FROM      lr_oa_emailaddressee a
                                                LEFT JOIN lr_oa_emailcontent c ON c.F_ContentId = a.F_ContentId
                                      WHERE     a.F_RecipientId = @userId
                                                AND a.F_DeleteMark = 1
                                      UNION
                                      SELECT    c.F_ContentId ,
                                                c.F_Theme ,
                                                c.F_ThemeColor ,
                                                c.F_SenderId ,
                                                c.F_SenderName ,
                                                c.F_SenderTime ,
                                                c.F_CreateDate,
                                                c.F_ParentId
                                      FROM      lr_oa_emailcontent c
                                      WHERE     c.F_CreateUserId = @userId
                                                AND c.F_DeleteMark = 1
                                    ) t WHERE 1=1");
            //parameter.Add(DbParameters.CreateDbParameter("@userId", userId));
            string theme = "";
            if (!keyword.IsEmpty())
            {
                strSql.Append(" AND F_Theme like @theme");
                theme = '%' + keyword + '%';
            }
            return this.BaseRepository().FindList<EmailContentEntity>(strSql.ToString(), new { userId , theme }, pagination);
        }
        /// <summary>
        /// 收件箱
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetAddresseeMail(Pagination pagination, string userId, string keyword)
        {
            var strSql = new StringBuilder();
            strSql.Append(@"SELECT  a.F_AddresseeId AS F_ContentId ,
                                    c.F_Theme ,
                                    c.F_ThemeColor ,
                                    c.F_SenderId ,
                                    c.F_SenderName ,
                                    c.F_SenderTime ,
                                    c.F_CreateDate,
                                    a.F_ContentId as F_ParentId
                            FROM    lr_oa_emailaddressee a
                                    LEFT JOIN lr_oa_emailcontent c ON c.F_ContentId = a.F_ContentId
                            WHERE   a.F_RecipientId = @userId");
            strSql.Append(" AND a.F_DeleteMark = 0 ");
            string theme = "";
            if (!keyword.IsEmpty())
            {
                strSql.Append(" AND c.F_Theme like @theme");
                theme = '%' + keyword + '%';
            }
            return this.BaseRepository().FindList<EmailContentEntity>(strSql.ToString(), new { userId, theme }, pagination);
        }
        /// <summary>
        /// 已发送
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="userId">用户Id</param>
        /// <param name="keyword">关键字</param>
        /// <returns></returns>
        public IEnumerable<EmailContentEntity> GetSentMail(Pagination pagination, string userId, string keyword)
        {
            var expression = LinqExtensions.True<EmailContentEntity>();
            expression = expression.And(t => t.F_SendState == 1);
            expression = expression.And(t => t.F_CreateUserId == userId);
            expression = expression.And(t => t.F_DeleteMark == 0);
            if (!keyword.IsEmpty())
            {
                expression = expression.And(t => t.F_Theme.Contains(keyword));
            }
            return this.BaseRepository().FindList(expression, pagination);
        }
        /// <summary>
        /// 邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmailContentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity<EmailContentEntity>(keyValue);
        }
        /// <summary>
        /// 收件邮件实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public EmailAddresseeEntity GetAddresseeEntity(string keyValue)
        {
            return new RepositoryFactory().BaseRepository().FindEntity<EmailAddresseeEntity>(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveDraftForm(string keyValue)
        {
            this.BaseRepository().Delete(new EmailContentEntity { F_ContentId = keyValue });
        }
        /// <summary>
        /// 删除未读、星标、收件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveAddresseeForm(string keyValue)
        {
            EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
            emailAddresseeEntity.F_AddresseeId = keyValue;
            emailAddresseeEntity.F_DeleteMark = 1;
            new RepositoryFactory().BaseRepository().Update(emailAddresseeEntity);
        }
        /// <summary>
        /// 彻底删除未读、星标、收件
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveAddresseeForm(string keyValue)
        {
            new RepositoryFactory().BaseRepository().Delete(new EmailAddresseeEntity { F_AddresseeId = keyValue });
        }
        /// <summary>
        /// 删除回收
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="Type">类型</param>
        public void RemoveRecycleForm(string keyValue, int Type)
        {
            this.BaseRepository().Delete(keyValue);
        }
        /// <summary>
        /// 删除已发
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveSentForm(string keyValue)
        {
            EmailContentEntity emailContentEntity = new EmailContentEntity();
            emailContentEntity.F_ContentId = keyValue;
            emailContentEntity.F_DeleteMark = 1;
            this.BaseRepository().Update(emailContentEntity);
        }
        /// <summary>
        /// 彻底删除已发
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void ThoroughRemoveSentForm(string keyValue)
        {
            EmailContentEntity emailContentEntity = new EmailContentEntity();
            emailContentEntity.F_ContentId = keyValue;
            emailContentEntity.F_DeleteMark = 2;
            this.BaseRepository().Update(emailContentEntity);
        }
        /// <summary>
        /// 保存邮件表单（发送、存入草稿、草稿编辑）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="emailContentEntity">邮件实体</param>
        /// <param name="addresssIds">收件人</param>
        /// <param name="copysendIds">抄送人</param>
        /// <param name="bccsendIds">密送人</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, EmailContentEntity emailContentEntity, string[] addresssIds, string[] copysendIds, string[] bccsendIds)
        {
            if (emailContentEntity.F_SendState == 0)
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    emailContentEntity.Modify(keyValue);
                    this.BaseRepository().Update(emailContentEntity);
                }
                else
                {
                    emailContentEntity.Create();
                    this.BaseRepository().Insert(emailContentEntity);
                }
            }
            else
            {
                IRepository db = new RepositoryFactory().BaseRepository().BeginTrans();
                try
                {
                    if (!string.IsNullOrEmpty(keyValue))
                    {
                        emailContentEntity.Modify(keyValue);
                        db.Update(emailContentEntity);
                    }
                    else
                    {
                        emailContentEntity.Create();
                        db.Insert(emailContentEntity);
                    }

                    #region 收件人
                    foreach (var item in addresssIds)
                    {
                        EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
                        emailAddresseeEntity.Create();
                        emailAddresseeEntity.F_ContentId = emailContentEntity.F_ContentId;
                        emailAddresseeEntity.F_RecipientId = item;
                        emailAddresseeEntity.F_RecipientState = 0;
                        db.Insert(emailAddresseeEntity);
                    }
                    #endregion

                    #region 抄送人
                    foreach (var item in copysendIds)
                    {
                        EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
                        emailAddresseeEntity.Create();
                        emailAddresseeEntity.F_ContentId = emailContentEntity.F_ContentId;
                        emailAddresseeEntity.F_RecipientId = item;
                        emailAddresseeEntity.F_RecipientState = 1;
                        db.Insert(emailAddresseeEntity);
                    }
                    #endregion

                    #region  密送人
                    foreach (var item in bccsendIds)
                    {
                        EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
                        emailAddresseeEntity.Create();
                        emailAddresseeEntity.F_ContentId = emailContentEntity.F_ContentId;
                        emailAddresseeEntity.F_RecipientId = item;
                        emailAddresseeEntity.F_RecipientState = 2;
                        db.Insert(emailAddresseeEntity);
                    }
                    #endregion

                    db.Commit();
                }
                catch (System.Exception)
                {
                    db.Rollback();
                    throw;
                }
            }
        }
        /// <summary>
        /// 设置邮件已读/未读
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="IsRead">是否已读：0-未读1-已读</param>
        public void ReadEmail(string keyValue, int IsRead = 1)
        {
            EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
            emailAddresseeEntity.F_AddresseeId = keyValue;
            emailAddresseeEntity.F_IsRead = IsRead;
            emailAddresseeEntity.F_ReadCount = emailAddresseeEntity.F_ReadCount + 1;
            emailAddresseeEntity.F_ReadDate = DateTime.Now;
            new RepositoryFactory().BaseRepository().Update(emailAddresseeEntity);
        }
        /// <summary>
        /// 设置邮件星标/取消星标
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="asterisk">星标：0-取消星标1-星标</param>
        public void SteriskEmail(string keyValue, int sterisk = 1)
        {
            EmailAddresseeEntity emailAddresseeEntity = new EmailAddresseeEntity();
            emailAddresseeEntity.F_AddresseeId = keyValue;
            emailAddresseeEntity.F_IsHighlight = sterisk;
            new RepositoryFactory().BaseRepository().Update(emailAddresseeEntity);
        }
        #endregion
    }
}
