using System.Web.Mvc;

namespace Learun.Application.Web.Areas.ERPDemo
{
    public class ERPDemoAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ERPDemo";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ERPDemo_default",
                "ERPDemo/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}