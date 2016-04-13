using br.mateus.DesafioMinhaVida.App_Start;
using br.mateus.DesafioMinhaVida.Context;
using br.mateus.DesafioMinhaVida.CustomBinder;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace br.mateus.DesafioMinhaVida
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //ordenação nos registros
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProdutoContext>());
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
        }
    }
}
