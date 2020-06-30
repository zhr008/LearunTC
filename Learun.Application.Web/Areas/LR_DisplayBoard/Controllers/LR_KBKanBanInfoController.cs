using Learun.Application.Base.SystemModule;
using Learun.Application.Extention.DisplayBoardManage;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_DisplayBoard.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-09-20 10:10
    /// 描 述：看板信息
    /// </summary>
    public class LR_KBKanBanInfoController : MvcControllerBase
    {
        private LR_KBKanBanInfoIBLL lR_KBKanBanInfoIBLL = new LR_KBKanBanInfoBLL();
        private LR_KBConfigInfoIBLL lR_KBConfigInfoIBLL = new LR_KBConfigInfoBLL();
        private CodeRuleIBLL codeRuleIBLL = new CodeRuleBLL();
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
            if (Request["keyValue"] == null)//根据keyValue判断是否为新增
            {
                ViewBag.KanBanCode = codeRuleIBLL.GetBillCode("KanBanCode");//编号
            }
            return View();
        }
        /// <summary>
        /// 看板预览界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewForm()
        {
            return View();
        }
        #endregion

        #region 获取数据

        /// <summary>
        /// 获取列表数据
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList( string queryJson )
        {
            var data = lR_KBKanBanInfoIBLL.GetList(queryJson);
            
            return Success(data);
        }
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetTemptList()
        {
            var data = lR_KBKanBanInfoIBLL.GetTemptList();
            return Success(data);
        }
        /// <summary>
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = lR_KBKanBanInfoIBLL.GetPageList(paginationobj, queryJson);
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
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetFormData(string keyValue)
        {
            var baseinfo = lR_KBKanBanInfoIBLL.GetEntity(keyValue);
            var configinfo = lR_KBConfigInfoIBLL.GetListByKBId(keyValue);
            var data = new
            {
                baseinfo = baseinfo,
                configinfo = configinfo
            };
            return Success(data);
        }
        #endregion

        #region 提交数据

        /// <summary>
        /// 删除实体数据
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult DeleteForm(string keyValue)
        {
            lR_KBKanBanInfoIBLL.DeleteEntity(keyValue);
            return Success("删除成功！");
        }
        /// <summary>
        /// 保存实体数据（新增、修改）
        /// <param name="keyValue">主键</param>
        /// <summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue,string kanbaninfo,string kbconfigInfo)
        {
            lR_KBKanBanInfoIBLL.SaveEntity(keyValue, kanbaninfo, kbconfigInfo);
            if (string.IsNullOrEmpty(keyValue))
            {
                codeRuleIBLL.UseRuleSeed("KanBanCode");//新增占用看板编号
            }
            return Success("保存成功！");
        }
        #endregion

    }
}
