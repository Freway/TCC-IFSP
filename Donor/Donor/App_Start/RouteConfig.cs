using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Donor
{
    public class RouteConfig
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("AdoNetAppender");

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            log4net.Config.XmlConfigurator.Configure();

            log.Debug("Teste na Rota Novo");
        }
    }
}
