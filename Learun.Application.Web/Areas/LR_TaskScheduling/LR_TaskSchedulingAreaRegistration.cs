using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_TaskScheduling
{
    public class LR_TaskSchedulingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_TaskScheduling";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_TaskScheduling_default",
                "LR_TaskScheduling/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}