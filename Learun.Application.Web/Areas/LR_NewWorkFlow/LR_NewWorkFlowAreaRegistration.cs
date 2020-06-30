using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_NewWorkFlow
{
    public class LR_NewWorkFlowAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_NewWorkFlow";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_NewWorkFlow_default",
                "LR_NewWorkFlow/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}