using System.Web.Http;
using br.mateus.DesafioMinhaVida.Context;
using br.mateus.DesafioMinhaVida.UnitsOfWork;
using System.Web.Script.Serialization;

namespace br.mateus.DesafioMinhaVida.API
{
    public class GuitarrasController : ApiController
    {
        private ProdutoContext _context;
        private UnitOfWork _unit;
        private JavaScriptSerializer js;

        public GuitarrasController()
        {
            _context = new ProdutoContext();
            _unit = new UnitOfWork(_context);
            js = new JavaScriptSerializer();
        }

        public string Get()
        {
            return js.Serialize(_unit.GuitarraRepositorio.Listar());
        }

        public IHttpActionResult GetGuitarra(int id)
        {
            var guitarra = _unit.GuitarraRepositorio.ProcurarPorId(id);
            if (guitarra == null)
            {
                return NotFound();
            }

            return Ok(guitarra);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
