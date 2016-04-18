using System.Web.Mvc;

namespace br.mateus.DesafioMinhaVida.Controllers
{
    public class ErroController : Controller
    {
        //Ver a tag  customErrors no web.config
        public ActionResult NotFind()
        {
            return View();
        }

        public ActionResult InternalError()
        {
            return View();
        }
    }
}