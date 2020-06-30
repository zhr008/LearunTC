using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_Message
{
    public class LR_MessageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_Message";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_Message_default",
                "LR_Message/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}