using Learun.Application.TwoDevelopment.SystemDemo;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeDemo.Controllers
{
    /// <summary>
    /// 版 本 Learun-ADMS V7.0.6 力软敏捷开发框架
    /// Copyright (c) 2013-2020 上海力软信息技术有限公司
    /// 创建人：力软-框架开发组
    /// 日 期：2018.04.12
    /// 描 述：表格演示
    /// </summary>
    public class GridDemoController : MvcControllerBase
    {
        /// <summary>
        /// 页面demo1
        /// </summary>
        [HttpGet]
        public ActionResult LayoutDemo1()
        { 
              return View();
        }
        /// <summary>
        /// 页面demo1的表单
        /// </summary>
        [HttpGet]
        public ActionResult LayoutFromDemo1()
        {
            return View();
        }
        /// <summary>
        /// 页面demo1的表单2
        /// </summary>
        [HttpGet]
        public ActionResult LayoutFromDemo2()
        {
            return View();
        }
        /// <summary>
        /// 页面demo1的表单3
        /// </summary>
        [HttpGet]
        public ActionResult LayoutFromDemo3()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LayoutFromDemo4()
        {
            return View();
        }
        [HttpGet]
        public ActionResult LayoutFromDemo5()
        {
            return View();
        }


        /// <summary>
        /// 普通表格
        /// </summary>
        /// <returns></returns>
        public ActionResult CommonIndex()
        {
            return View();
        }


        /// <summary>
        /// 编辑表格
        /// </summary>
        /// <returns></returns>
        public ActionResult EditIndex()
        {
            return View();
        }

        /// <summary>
        /// 报表表格
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportIndex()
        {
            return View();
        }

        /// <summary>
        /// 列头查询
        /// </summary>
        /// <returns></returns>
        public ActionResult jqGridIndex()
        {
            return View();
        }

        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetList()
        {
            DemoleaveIBLL demoleaveIBLL = new DemoleaveBLL();
            var data = demoleaveIBLL.GetList();
            return Success(data);
        }
        /// <summary>
        /// 保存表格
        /// </summary>
        /// <param name="jsondata">表格数据 </param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        public ActionResult SaveList(string jsondata)
        {
            if (!string.IsNullOrEmpty(jsondata))
            {
                DemoleaveIBLL demoleaveIBLL = new DemoleaveBLL();
                demoleaveIBLL.SaveList(jsondata);
            }
            return Success("操作成功。");
        }



    }
}