using Newtonsoft.Json.Serialization;
using System.Net.Http.Headers;
using System.Web.Http;

namespace DesafioMinhaVida.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {        
            config.MapHttpAttributeRoutes();
            //liga o cors
            config.EnableCors();
            //faz com que o retorno da api seja em json
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            //mudando a rota default
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}"
            );
        }
    }
}
