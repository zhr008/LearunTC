using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeGeneratorModule.Controllers
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.09
    /// 描 述：字体图标查看
    /// </summary>
    public class IconController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 图标查看
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 手机图标查看
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppIndex()
        {
            return View();
        }
        #endregion
    }
}