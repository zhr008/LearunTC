using Learun.Application.OA.Email2;
using System.Web.Mvc;
using Learun.Util;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.LR_OAModule.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V6.1.6.0 力软敏捷开发框架
    /// Copyright (c) 2013-2017 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.06.04
    /// 描 述：邮件管理
    /// </summary>
    public class EmailController : MvcControllerBase
    {
        private EmailContentIBLL emailContentIBLL = new EmailContentBLL();

        #region 视图功能
        /// <summary>
        /// 管理页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 写邮件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }

        /// <summary>
        /// 收件详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DetailForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="categoryId">类型</param>
        /// <param name="keyword">关键词</param>
        /// <returns></returns>
        public ActionResult GetPageList(string pagination,string type, string keyword)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            IEnumerable<EmailContentEntity> data = new List<EmailContentEntity>();
            UserInfo userInfo = LoginUserInfo.Get();
            switch (type) {
                case "1":
                    data = emailContentIBLL.GetAddresseeMail(paginationobj, userInfo.userId, keyword);
                    break;
                case "2":
                    data = emailContentIBLL.GetDraftMail(paginationobj, userInfo.userId, keyword);
                    break;
                case "3":
                    data = emailContentIBLL.GetSentMail(paginationobj, userInfo.userId, keyword);
                    break;
                case "4":
                    data = emailContentIBLL.GetRecycleMail(paginationobj, userInfo.userId, keyword);
                    break;
            }
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records,
            };
            return Success(jsonData);
        }

        /// <summary>
        /// 获取实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        public ActionResult GetEntity(string keyValue)
        {
            var data = emailContentIBLL.GetEntity(keyValue);
            return Success(data);
        }
        #endregion 

        #region 提交数据
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="content">邮件内容</param>
        /// <param name="addresssIds">收件人</param>
        /// <param name="copysendIds">抄手人</param>
        /// <param name="bccsendIds">秘密抄手人</param>
        /// <returns></returns>
        [HttpPost, ValidateAntiForgeryToken, AjaxOnly, ValidateInput(false)]
        public ActionResult SaveForm(string keyValue, string content, string addresssIds, string copysendIds, string bccsendIds)
        {
            EmailContentEntity emailContentEntity = content.ToObject<EmailContentEntity>();
            emailContentIBLL.SaveForm(keyValue, emailContentEntity, addresssIds, copysendIds, bccsendIds);
            return Success("保存成功！");
        }

        /// <summary>
        /// 删除草稿
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteDraftForm(string keyValue)
        {
            emailContentIBLL.ThoroughRemoveForm(keyValue, "draftMail");
            return Success("删除成功！");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteForm(string keyValue,string type)
        {
            string emailType = type == "1" ? "addresseeMail" : "sendMail";
            emailContentIBLL.RemoveForm(keyValue, emailType);
            return Success("删除成功！");
        }
        /// <summary>
        /// 彻底删除
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ThoroughRemoveForm(string keyValue, string type)
        {
            string emailType = "";
            switch (type) {
                case "1":
                    emailType = "addresseeMail";
                    break;
                case "3":
                    emailType = "sendMail";
                    break;
                case "4":
                    emailType = "recycleMail";
                    break;
            }
            emailContentIBLL.ThoroughRemoveForm(keyValue, emailType);
            return Success("删除成功！");
        }
        #endregion
    }
}