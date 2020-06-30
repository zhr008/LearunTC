using System.Web.Mvc;

namespace Learun.Application.Web
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：过滤器
    /// </summary>
    public class FilterConfig
    {
        /// <summary>
        /// 注册全局注册器
        /// </summary>
        /// <param name="filters">过滤控制器</param>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandlerErrorAttribute());
            //filters.Add(new ResultFillters());
        }
    }
}
