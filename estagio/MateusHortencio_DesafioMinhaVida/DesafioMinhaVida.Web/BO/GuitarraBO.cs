using br.mateus.DesafioMinhaVida.Models.Context;
using br.mateus.DesafioMinhaVida.DAO.UnitsOfWork;
using br.mateus.DesafioMinhaVida.Web.Utils;
using br.mateus.DesafioMinhaVida.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Web;
using br.mateus.DesafioMinhaVida.Models.Models;

namespace DesafioMinhaVida.Web.BO
{
    public class GuitarraBO
    {
        private ProdutoContext _context;
        private UnitOfWork _unit;

        public GuitarraBO()
        {
            _context = new ProdutoContext();
            _unit = new UnitOfWork(_context);
        }

        public Guitarra ParseGuitarra(GuitarraViewModel viewModel)
        {
            var guitarra = new Guitarra()
            {
                Id = viewModel.Id,
                Nome = viewModel.Nome,
                Descricao = viewModel.Descricao,
                DataInclusao = viewModel.DataInclusao,
                Preco = viewModel.Preco,
                UrlImagem = viewModel.UrlImagem
            };
            return guitarra;
        }

        public GuitarraViewModel ParseGuitarraViewModel(Guitarra model)
        {
            var guitarraViewModel = new GuitarraViewModel()
            {
                Id = model.Id,
                Nome = model.Nome,
                Descricao = model.Descricao,
                DataInclusao = model.DataInclusao,
                Preco = model.Preco,
                UrlImagem = model.UrlImagem
            };
            return guitarraViewModel;
        }

        public IEnumerable<GuitarraViewModel> ListarGuitarraViewModel()
        {
            var viewModels = new List<GuitarraViewModel>();
            var models = _unit.GuitarraRepositorio.Listar();

            foreach (var model in models)
            {
                viewModels.Add(ParseGuitarraViewModel(model));
            }

            return viewModels;
        }

        public void CadastrarGuitarraViewModel(GuitarraViewModel viewModel, HttpPostedFileBase file, string caminhoFisico = null)
        {
            var model = ParseGuitarra(viewModel);

            if (file != null)
            {
                var url = new ImageUploader().Upload(file, model.Nome.Replace(" ", "_"), caminhoFisico);
                model.UrlImagem = url;
            }
            model.DataInclusao = DateTime.Now;

            _unit.GuitarraRepositorio.Adicionar(model);
            _context.SaveChanges();
        }

        public void AtualizarGuitarraViewModel(GuitarraViewModel viewModel, HttpPostedFileBase file, string pathImagem, string caminhoFisico)
        {
            var model = ParseGuitarra(viewModel);

            if (file != null)
            {
                //deleta a imagem antiga
                System.IO.File.Delete(pathImagem);
                var urlNovaImagem = new ImageUploader().Upload(file, model.Nome.Replace(" ", "_"), caminhoFisico);

                model.UrlImagem = urlNovaImagem;
            }

            _unit.GuitarraRepositorio.Atualizar(model);
            _context.SaveChanges();
        }

        public void DeletarGuitarraViewModel(int id, string urlImagem)
        {
            var model = _unit.GuitarraRepositorio.ProcurarPorId(id);

            if (!string.IsNullOrEmpty(model.UrlImagem))
            {
                System.IO.File.Delete(urlImagem);
            }

            _unit.GuitarraRepositorio.Deletar(model);
            _unit.Salvar();
        }

        public GuitarraViewModel ProcurarGuitarraViewModelPorId(int? id)
        {
            var model = _unit.GuitarraRepositorio.ProcurarPorId(id);
            var viewModel = ParseGuitarraViewModel(model);

            return viewModel;
        }

        public void DisposeContext(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

    }
}
