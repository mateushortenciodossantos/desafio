using br.mateus.DesafioMinhaVida.Context;
using br.mateus.DesafioMinhaVida.Exceptions;
using br.mateus.DesafioMinhaVida.Models;
using br.mateus.DesafioMinhaVida.UnitsOfWork;
using br.mateus.DesafioMinhaVida.Utils;
using br.mateus.DesafioMinhaVida.ViewModel;
using System;
using System.Collections.Generic;
using System.Web;

namespace DesafioMinhaVida.Persistencia.BO
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
                try
                {
                    var url = new ImageUploader().Upload(file, model.Nome.Replace(" ", "_"), caminhoFisico);
                    model.UrlImagem = url;
                }
                catch (ImageUploaderExceptions ex)
                {
                    throw new ImageUploaderExceptions(ex.Message);
                }

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

        public void DeletarGuitarraViewModel(GuitarraViewModel viewModel, string urlImagem)
        {
            var model = ParseGuitarra(viewModel);

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
    }
}
