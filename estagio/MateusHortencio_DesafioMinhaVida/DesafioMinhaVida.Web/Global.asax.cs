﻿using br.mateus.DesafioMinhaVida.Models.Context;
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
            RouteConfig.RegisterRoutes(RouteTable.Routes);//Code First, Criar sempre que a base nao exista, com base no ProdutoContext
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProdutoContext>());
        }
    }
}
