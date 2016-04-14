using br.mateus.DesafioMinhaVida.Models.Context;
using br.mateus.DesafioMinhaVida.Web.CustomBinder;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using br.mateus.DesafioMinhaVida.Web;

namespace br.mateus.DesafioMinhaVida
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //ordenação nos registros
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProdutoContext>());
            ModelBinders.Binders.Add(typeof(double), new DoubleModelBinder());
        }
    }
}
