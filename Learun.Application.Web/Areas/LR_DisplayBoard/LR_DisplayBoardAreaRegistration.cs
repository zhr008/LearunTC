using System.Web.Mvc;

namespace Learun.Application.Web.Areas.LR_DisplayBoard
{
    public class LR_DisplayBoardAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "LR_DisplayBoard";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "LR_DisplayBoard_default",
                "LR_DisplayBoard/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}