using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using OnlineAlbum.App_Start;

namespace OnlineAlbum
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
