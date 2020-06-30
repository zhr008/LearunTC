using System.Web.Mvc;
using System.Web.Routing;

namespace Learun.Application.Web
{
    /// <summary>
    
    
    /// 创建人：力软-框架开发组
    /// 日 期：2017.03.08
    /// 描 述：数据库类型枚举
    /// </summary>
    public class RouteConfig
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes">路由控制器</param>
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("WebService1.asmx/{*pathInfo}");
            routes.IgnoreRoute("ActiveReports.ReportService.asmx/{*pathInfo}");
            routes.IgnoreRoute("{*allActiveReport}", new { allActiveReport = @".*\.ar13(/.*)?" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
