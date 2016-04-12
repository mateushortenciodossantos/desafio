using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using br.mateus.DesafioMinhaVida.Context;
using br.mateus.DesafioMinhaVida.Models;
using br.mateus.DesafioMinhaVida.UnitsOfWork;
using System.Web.Script.Serialization;
using System.Web.Http.Cors;

namespace br.mateus.DesafioMinhaVida.API
{
    [EnableCors(origins: "*", headers: "*", methods:"GET")]
    public class GuitarrasController : ApiController
    {
        private ProdutoContext _context;
        private UnitOfWork _unit;
        private JavaScriptSerializer js;

        public GuitarrasController()
        {
            
        }

        public string Get()
        {
            _context = new ProdutoContext();
            _unit = new UnitOfWork(_context);
            js = new JavaScriptSerializer();
            return js.Serialize(_unit.GuitarraRepositorio.Listar());
        }

        [ResponseType(typeof(string))]
        public IHttpActionResult GetGuitarra(int id)
        {
            Guitarra guitarra = _unit.GuitarraRepositorio.ProcurarPorId(id);
            if (guitarra == null)
            {
                return NotFound();
            }

            return Ok(js.Serialize(guitarra));
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