using System.Web.Mvc;

namespace KVBchat_ASP.Areas.Cabinet
{
    public class CabinetAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Cabinet";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Cabinet_default",
                "Cabinet/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}