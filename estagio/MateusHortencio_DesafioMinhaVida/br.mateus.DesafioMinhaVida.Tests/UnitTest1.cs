using Microsoft.VisualStudio.TestTools.UnitTesting;
using br.mateus.DesafioMinhaVida.UnitsOfWork;
using br.mateus.DesafioMinhaVida.Context;
using br.mateus.DesafioMinhaVida.Models;
using System;

namespace br.mateus.DesafioMinhaVida.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private ProdutoContext context;
        private UnitOfWork unit;

        public UnitTest1()
        {
            context = new ProdutoContext();
            unit = new UnitOfWork(context);
        }

        [TestMethod]
        public void AdicionarTest()
        {
            var guitarra = InsertTest999();

            unit.GuitarraRepositorio.Adicionar(guitarra);
            var guitar = unit.GuitarraRepositorio.ProcurarPorId(999);
            Assert.AreEqual(guitar, guitarra);
        }

        [TestMethod]
        public void DeletarTest()
        {
            var guitarra = InsertTest999();

            unit.GuitarraRepositorio.Adicionar(guitarra);
            unit.GuitarraRepositorio.Deletar(guitarra);
            var guitar = unit.GuitarraRepositorio.ProcurarPorId(999);
            Assert.IsNull(guitar);
        }

        [TestMethod]
        public void ListarTest()
        {
            var guitarras = unit.GuitarraRepositorio.Listar();

            Assert.IsNotNull(guitarras);
        }

        public Guitarra InsertTest999()
        {
            return new Guitarra()
            {
                Id = 999,
                Nome = "Teste",
                DataInclusao = DateTime.Now,
                Descricao = "descricao teste",
                Preco = 15.0,
                UrlImagem = "urlteste"
            };
        }
    }
}
