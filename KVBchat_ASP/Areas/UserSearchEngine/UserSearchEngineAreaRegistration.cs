using System.Web.Mvc;

namespace KVBchat_ASP.Areas.UserSearchEngine
{
    public class UserSearchEngineAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserSearchEngine";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UserSearchEngine_default",
                "UserSearchEngine/{controller}/{action}/{id}",
                new { controller = "UserSearch", action = "UserSearch", id = UrlParameter.Optional }
            );
        }
    }
}