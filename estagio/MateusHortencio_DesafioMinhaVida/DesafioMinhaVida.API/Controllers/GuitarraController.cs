using br.mateus.DesafioMinhaVida.Models.Context;
using br.mateus.DesafioMinhaVida.DAO.UnitsOfWork;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DesafioMinhaVida.API.Controllers
{
    //Prefixo da rota /v1
    [RoutePrefix("v1")]
    //Cors para segurança da API, controlando requisições se preciso
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
        //  /v1/guitarras
        [HttpGet]
        [Route("guitarras")]
        public IHttpActionResult Listar()
        {
            //retorna uma lista das guitarras cadastradas
            return Ok(_unit.GuitarraRepositorio.Listar());
        }
        // /v1/guitarras/1 (exemplo)
        [HttpGet]
        [Route("guitarras/{id:int}")]
        public IHttpActionResult Buscar(int id)
        {
            //retorna um OK com a guitarra
            return Ok(_unit.GuitarraRepositorio.ProcurarPorId(id));
        }
    }
}
