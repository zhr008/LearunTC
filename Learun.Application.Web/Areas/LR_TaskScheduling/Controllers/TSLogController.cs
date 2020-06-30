using Learun.Application.Extention.TaskScheduling;
using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_TaskScheduling.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-10-24 14:57
    /// 描 述：任务日志
    /// </summary>
    public class TSLogController : MvcControllerBase
    {
        private TSLogIBLL tSkLogIBLL = new TSLogBLL();

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
        /// 获取列表分页数据
        /// <param name="pagination">分页参数</param>
        /// <summary>
        /// <returns></returns>
        [HttpGet]
        [AjaxOnly]
        public ActionResult GetPageList(string pagination, string queryJson)
        {
            Pagination paginationobj = pagination.ToObject<Pagination>();
            var data = tSkLogIBLL.GetPageList(paginationobj, queryJson);
            var jsonData = new
            {
                rows = data,
                total = paginationobj.total,
                page = paginationobj.page,
                records = paginationobj.records
            };
            return Success(jsonData);
        }
        #endregion
    }
}
