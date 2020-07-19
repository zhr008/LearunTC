using DocumentFormat.OpenXml.Vml.Office;
using GrapeCity.ActiveReports.PageReportModel;
using Learun.Application.TwoDevelopment.LR_CodeDemo;
using Learun.Util;
using System.Data;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    /// 创 建：超级管理员
    /// 日 期：2020-07-14 23:25
    /// 描 述：1231
    /// </summary>
    public class RelationController : MvcControllerBase
    {
        private RelationIBLL RelationIBLL = new RelationBLL();
        private CredentialsIBLL credentialsIBLL = new CredentialsBLL();
        #region 视图功能

        /// <summary>
        /// 主页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 表单页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Form()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]

        public ActionResult GetList(string queryJson)
        {
            var data = RelationIBLL.GetList(queryJson);
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        [HttpGet]

        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = RelationIBLL.GetPageList(paginationobj, queryJson);
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

        public ActionResult GetFormData(string keyValue)
        {
            var data = RelationIBLL.GetEntity(keyValue);
            var jsonData = new
            {
                tc_Relation = data,
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

        public ActionResult DeleteForm(string keyValue)
        {
            RelationIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SaveForm(string keyValue, tc_RelationEntity entity)
        {
            RelationIBLL.SaveEntity(keyValue, entity);
            return Success("保存成功！");
        }
        #endregion

        [HttpPost]
        public ActionResult AllocationRaltion(string CredentialsId, string ProjectDetailId, string ProjectId)
        {
            foreach (var item in CredentialsId.Split(','))
            {
                var certEntity = credentialsIBLL.Gettc_CredentialsEntity(item);
                if (certEntity == null)
                {
                    return Fail("该用户证书不存在");
                }
                tc_RelationEntity tc = new tc_RelationEntity()
                {
                    ProjectDetailId = ProjectDetailId,
                    ProjectId = ProjectDetailId,
                    F_CertId = item,
                    F_PersonId = certEntity.F_PersonId,
                    F_RelationStatus = 1 //未确认状态
                };
                RelationIBLL.SaveEntity("", tc);
            }
            return Success("分配成功");
        }



        public ActionResult GetRelationDetail(string ProjectDetailId)
        {
            var data = RelationIBLL.GetRelationDetail(ProjectDetailId);
            return Success(data);

        }
    }
}
