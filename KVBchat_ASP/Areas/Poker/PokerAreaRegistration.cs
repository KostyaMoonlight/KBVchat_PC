using System.Web.Mvc;

namespace KVBchat_ASP.Areas.Poker
{
    public class PokerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Poker";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Poker_default",
                "Poker/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}