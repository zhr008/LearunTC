using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_PortalSite
{
    public class LR_PortalSiteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_PortalSite";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_PortalSite_default",
                "LR_PortalSite/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}