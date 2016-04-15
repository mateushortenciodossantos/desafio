using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using br.mateus.DesafioMinhaVida.Web.Exceptions;
using DesafioMinhaVida.Web.BO;
using br.mateus.DesafioMinhaVida.Web.ViewModel;

namespace br.mateus.DesafioMinhaVida.Web.Controllers
{
    public class GuitarrasController : Controller
    {
        GuitarraBO bo;

        public GuitarrasController()
        {
            bo = new GuitarraBO();
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
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,PrecoString,Descricao")] GuitarraViewModel viewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string caminhoFisico = null;
                    if(file != null)
                        caminhoFisico = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ImagePath"]);
                    bo.CadastrarGuitarraViewModel(viewModel, file, caminhoFisico);
                    var viewModelRetorno = new GuitarraViewModel() { MensagemSucesso = "Guitarra Cadastrada com Sucesso!" };

                    return View(viewModelRetorno);
                }
                catch (ImageUploaderExceptions ex)
                {
                    viewModel.MensagemErro = ex.Message;
                    viewModel.MensagemSucesso = null;
                    return View(viewModel);
                }
            }

            viewModel.MensagemErro = "Erro ao cadastrar Guitarra";
            viewModel.MensagemSucesso = null;
            return View(viewModel);
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
        public ActionResult Editar([Bind(Include = "Id,Nome,PrecoString,Descricao,DataInclusao,UrlImagem")] GuitarraViewModel viewModel, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Busca o caminho fisico da imagem antiga da guitarra
                    var pathImagem = HttpContext.Server.MapPath(viewModel.UrlImagem);

                    //Busca o diretorio de imagens
                    var caminhoFisico = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ImagePath"]);
                    bo.AtualizarGuitarraViewModel(viewModel, file, pathImagem, caminhoFisico);
                    return RedirectToAction("Listar");
                }
                catch (ImageUploaderExceptions ex)
                {
                    viewModel.MensagemErro = ex.Message;
                    viewModel.MensagemSucesso = null;
                    return View(viewModel);
                }
            }

            viewModel = bo.ProcurarGuitarraViewModelPorId(viewModel.Id);
            viewModel.MensagemErro = "Erro ao cadastrar Guitarra";
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

            bo.DeletarGuitarraViewModel(id, urlImagem);

            return RedirectToAction("Listar");
        }

        protected override void Dispose(bool disposing)
        {
            bo.DisposeContext(disposing);
            base.Dispose(disposing);
        }
    }
}
