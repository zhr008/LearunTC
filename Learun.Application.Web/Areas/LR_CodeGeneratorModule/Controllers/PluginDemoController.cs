using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_CodeGeneratorModule.Controllers
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.09
    /// 描 述：JS插件Demo
    /// </summary>
    public class PluginDemoController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// JS插件展示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        #endregion
        
    }
}