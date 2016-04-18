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
            //Traz uma coleção de Guitarras e Envia para a View
            return View(bo.ListarGuitarraViewModel());
        }

        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listar");
            }
            //Pesquisa traz uma ViewModel de Guitarra com base em um id, caso a guitarra seja nula, o tratamento é feito na view
            var guitarra = bo.ProcurarGuitarraViewModelPorId(id);
            
            return View(guitarra);
        }

        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        //Token para validação de acesso
        [ValidateAntiForgeryToken]    //Bind nos atributos necessários para o cadastro                              //imagem recebida pela view
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,PrecoString,Descricao")] GuitarraViewModel viewModel, HttpPostedFileBase file)
        {
            //Se o bind ocorrer corretamente
            if (ModelState.IsValid)
            {
                try
                {
                    string caminhoFisico = null;
                    if(file != null)
                        //Procura o caminho físico a partir de uma key do web.config que contém o caminho virtual para a pasta de imagens
                        caminhoFisico = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ImagePath"]);
                    bo.CadastrarGuitarraViewModel(viewModel, file, caminhoFisico);
                    //Se tudo der certo uma mensagem de Sucesso é atribuida na model
                    var viewModelRetorno = new GuitarraViewModel() { MensagemSucesso = "Guitarra Cadastrada com Sucesso!" };
                    //E retorna para a view
                    return View(viewModelRetorno);
                } //Em caso de Exception
                catch (ImageUploaderExceptions ex)
                {
                    //É retornada uma view model com uma mensagem de erro vinda da exception
                    viewModel.MensagemErro = ex.Message;
                    viewModel.MensagemSucesso = null;
                    return View(viewModel);
                }
            }
            //Caso o bind tenha sido errado, é emitida uma mensagem de erro pela view model
            viewModel.MensagemErro = "Erro ao cadastrar Guitarra";
            viewModel.MensagemSucesso = null;
            return View(viewModel);
        }

        public ActionResult Editar(int? id)
        {
            //Se o id for nulo ele redireciona para a Lista de Guitarras
            if (id == null)
            {
                return RedirectToAction("Listar");
            }
            
            var guitarra = bo.ProcurarGuitarraViewModelPorId(id);
            
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
                    var pathImagem = string.Empty;
                    //Busca o caminho fisico da imagem antiga da guitarra
                    if (!string.IsNullOrEmpty(viewModel.UrlImagem))
                        pathImagem = HttpContext.Server.MapPath(viewModel.UrlImagem);

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
            viewModel.MensagemErro = "Erro ao editar Guitarra";
            return View(viewModel);
        }

        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Listar");
            }
            var guitarra = bo.ProcurarGuitarraViewModelPorId(id);
            
            return View(guitarra);
        }

        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmarDeletar(int id)
        {
            var guitarra = bo.ProcurarGuitarraViewModelPorId(id);
            var urlImagem = string.Empty;
            //Caso a Guitarra tenha uma imagem, pegamos o caminho físico até a imagem
            if (!string.IsNullOrEmpty(guitarra.UrlImagem))
            {
                urlImagem = HttpContext.Server.MapPath(guitarra.UrlImagem);                
            }
            // e deletamos a imagem(caso exista), e a guitarra
            bo.DeletarGuitarraViewModel(id, urlImagem);

            return RedirectToAction("Listar");
        }

        protected override void Dispose(bool disposing)
        {
            //Liberação de recursos
            bo.DisposeContext(disposing);
            base.Dispose(disposing);
        }
    }
}
