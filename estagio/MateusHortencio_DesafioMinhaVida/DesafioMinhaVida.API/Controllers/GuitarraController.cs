using br.mateus.DesafioMinhaVida.Models.Context;
using br.mateus.DesafioMinhaVida.DAO.UnitsOfWork;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DesafioMinhaVida.API.Controllers
{
    [RoutePrefix("v1")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class GuitarraController : ApiController
    {
        private UnitOfWork _unit;
        private ProdutoContext _context;

        public GuitarraController()
        {
            _context = new ProdutoContext();
            _unit = new UnitOfWork(_context);
        }

        [HttpGet]
        [Route("guitarras")]
        public IHttpActionResult Listar()
        {
            return Ok(_unit.GuitarraRepositorio.Listar());
        }

        [HttpGet]
        [Route("guitarras/{id:int}")]
        public IHttpActionResult Buscar(int id)
        {
            return Ok(_unit.GuitarraRepositorio.ProcurarPorId(id));
        }
    }
}
