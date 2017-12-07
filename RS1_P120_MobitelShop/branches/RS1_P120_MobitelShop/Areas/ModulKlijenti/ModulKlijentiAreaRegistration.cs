using System.Web.Mvc;

namespace RS1_P120_MobitelShop.Areas.ModulKlijenti
{
    public class ModulKlijentiAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ModulKlijenti";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ModulKlijenti_default",
                "ModulKlijenti/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}