using Learun.Util;
using System.Data;
using Learun.Application.TwoDevelopment.LR_CodeDemo;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Learun.Application.Web.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2020-06-27 23:47
    /// 描 述：供应商登记
    /// </summary>
    public class ApplicantController : MvcControllerBase
    {
        private ApplicantIBLL applicantIBLL = new ApplicantBLL();

        #region 视图功能

        /// <summary>
        /// 主页面
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
             return View();
        }
        /// <summary>
        /// 表单页
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
             return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取页面显示列表数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = applicantIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        /// <summary>
        /// 获取表单数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var tc_ApplicantData = applicantIBLL.Gettc_ApplicantEntity( keyValue );
            var jsonData = new {
                tc_Applicant = tc_ApplicantData,
            };
            return Success(jsonData);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            applicantIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="strEntity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, string strEntity)
        {
            tc_ApplicantEntity entity = strEntity.ToObject<tc_ApplicantEntity>();
            applicantIBLL.SaveEntity(keyValue,entity);
            if (string.IsNullOrEmpty(keyValue))
            {
            }
            return Success("保存成功！");
        }
        #endregion

    }
}
