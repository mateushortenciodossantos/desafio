using System.Net;
using System.Web;
using System.Web.Mvc;
using br.mateus.DesafioMinhaVida.Context;
using System.Configuration;
using br.mateus.DesafioMinhaVida.Exceptions;
using DesafioMinhaVida.Persistencia.BO;
using br.mateus.DesafioMinhaVida.ViewModel;

namespace br.mateus.DesafioMinhaVida.Controllers
{
    public class GuitarrasController : Controller
    {
        GuitarraBO bo;
        ProdutoContext _context;

        public GuitarrasController()
        {
            bo = new GuitarraBO();
            _context = new ProdutoContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar()
        {
            return View(bo.ListarGuitarraViewModel());
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var guitarra = bo.ProcurarGuitarraViewModelPorId(id);
            if (guitarra == null)
            {
                return HttpNotFound();
            }
            return View(guitarra);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,Preco,Descricao")] GuitarraViewModel viewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var caminhoFisico = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ImagePath"]);
                    bo.CadastrarGuitarraViewModel(viewModel, file, caminhoFisico);
                }
                catch (ImageUploaderExceptions ex)
                {
                    viewModel.Erro = ex.Message;
                    return View(viewModel);
                }
            }

            return RedirectToAction("Listar");
        }

        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var guitarra = bo.ProcurarGuitarraViewModelPorId(id);
            if (guitarra == null)
            {
                return HttpNotFound();
            }
            return View(guitarra);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,Preco,Descricao,DataInclusao,UrlImagem")] GuitarraViewModel viewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                //Busca o caminho fisico da imagem antiga da guitarra
                var pathImagem = HttpContext.Server.MapPath(viewModel.UrlImagem);

                //Busca o diretorio de imagens
                var caminhoFisico = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ImagePath"]);
                bo.AtualizarGuitarraViewModel(viewModel, file, pathImagem, caminhoFisico);
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var guitarra = bo.ProcurarGuitarraViewModelPorId(id);
            if (guitarra == null)
            {
                return HttpNotFound();
            }
            return View(guitarra);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var guitarra = bo.ProcurarGuitarraViewModelPorId(id);
            var urlImagem = string.Empty;
            if (!string.IsNullOrEmpty(guitarra.UrlImagem))
            {
                urlImagem = HttpContext.Server.MapPath(guitarra.UrlImagem);                
            }

            bo.DeletarGuitarraViewModel(guitarra, urlImagem);

            return RedirectToAction("Index");
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
