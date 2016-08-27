using System.Web.Mvc;

namespace NFine.Web.Areas.MenuSys
{
    public class MenuSysAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "MenuSys";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "MenuSys_default",
                "MenuSys/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
