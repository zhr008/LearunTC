using Learun.Util;
using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_Desktop.Controllers
{
    /// <summary>
    
    
    /// 创 建：超级管理员
    /// 日 期：2018-05-28 16:08
    /// 描 述：桌面配置
    /// </summary>
    public class DTSettingController : MvcControllerBase
    {
        #region 视图功能
        /// <summary>
        /// 移动端桌面配置（和pc桌面采用一套数据）
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult AppIndex()
        {
            return View();
        }
        /// <summary>
        /// pc端桌面设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PcIndex()
        {
            string learn_UItheme = WebHelper.GetCookie("Learn_ADMS_V7_UItheme");
            switch (learn_UItheme)
            {
                case "1":
                    return View("DefaultIndex");      // 经典版本
                case "2":
                    return View("AccordionIndex");    // 手风琴版本
                case "3":
                    return View("WindowsIndex");       // Windos版本
                case "4":
                    return View("TopIndex");          // 顶部菜单版本
                case "5":
                    return View("DefaultIndex");      // 主题五
                default:
                    return View("DefaultIndex");      // 经典版本
            }
        }
        #endregion
    }
}