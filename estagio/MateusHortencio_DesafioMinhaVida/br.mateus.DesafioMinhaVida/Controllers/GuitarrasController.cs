using System.Net;
using System.Web;
using System.Web.Mvc;
using br.mateus.DesafioMinhaVida.Context;
using br.mateus.DesafioMinhaVida.Models;
using br.mateus.DesafioMinhaVida.UnitsOfWork;
using br.mateus.DesafioMinhaVida.Utils;
using System.Configuration;
using System;
using AutoMapper;
using br.mateus.DesafioMinhaVida.ViewModel;
using System.Collections;

namespace br.mateus.DesafioMinhaVida.Controllers
{
    public class GuitarrasController : Controller
    {
        private ProdutoContext _context;
        private UnitOfWork _unit;

        public GuitarrasController()
        {
            _context = new ProdutoContext();
            _unit = new UnitOfWork(_context);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Listar()
        {
            return View(_unit.GuitarraRepositorio.Listar());
        }
        
        public ActionResult Detalhes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitarra guitarra = _unit.GuitarraRepositorio.ProcurarPorId(id);
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
        public ActionResult Cadastrar([Bind(Include = "Id,Nome,Preco,Descricao")] Guitarra guitarra, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var caminhoFisico = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ImagePath"]);
                    var url = new ImageUploader().Upload(file, guitarra.Nome.Replace(" ", "_"), caminhoFisico);

                    guitarra.UrlImagem = url;
                }
                guitarra.DataInclusao = DateTime.Now;

                _unit.GuitarraRepositorio.Adicionar(guitarra);
                _unit.Salvar();
                return RedirectToAction("Listar");
            }

            return RedirectToAction("Listar");
        }
        
        public ActionResult Editar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitarra guitarra = _unit.GuitarraRepositorio.ProcurarPorId(id);
            if (guitarra == null)
            {
                return HttpNotFound();
            }
            return View(guitarra);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar([Bind(Include = "Id,Nome,Preco,Descricao,DataInclusao,UrlImagem")] Guitarra guitarra, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    //Busca o caminho fisico da imagem antiga da guitarra
                    var pathImagem = HttpContext.Server.MapPath(guitarra.UrlImagem);
                    //deleta a imagem antiga
                    System.IO.File.Delete(pathImagem);
                    
                    //Busca o diretorio de imagens
                    var diretorioImagens = HttpContext.Server.MapPath(ConfigurationManager.AppSettings["ImagePath"]);
                    var urlNovaImagem = new ImageUploader().Upload(file, guitarra.Nome.Replace(" ", "_"), diretorioImagens);

                    guitarra.UrlImagem = urlNovaImagem;
                }               

                _unit.GuitarraRepositorio.Atualizar(guitarra);
                _unit.Salvar();
                return RedirectToAction("Index");
            }
            return View(guitarra);
        }
        
        public ActionResult Deletar(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guitarra guitarra = _unit.GuitarraRepositorio.ProcurarPorId(id);
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
            Guitarra guitarra = _unit.GuitarraRepositorio.ProcurarPorId(id);
            var urlImagem = HttpContext.Server.MapPath(guitarra.UrlImagem);
            System.IO.File.Delete(urlImagem);
            _unit.GuitarraRepositorio.Deletar(guitarra);
            _unit.Salvar();
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
