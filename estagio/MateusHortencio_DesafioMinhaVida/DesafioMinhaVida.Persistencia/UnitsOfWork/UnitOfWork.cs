using br.mateus.DesafioMinhaVida.Context;
using br.mateus.DesafioMinhaVida.Models;
using br.mateus.DesafioMinhaVida.Repositorios;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace br.mateus.DesafioMinhaVida.UnitsOfWork
{
    public class UnitOfWork
    {
        private ProdutoContext _context;

        public UnitOfWork(ProdutoContext context)
        {
            _context = context;
        }

        private IRepositorioBase<Guitarra> _guitarraRepositorio;

        public IRepositorioBase<Guitarra> GuitarraRepositorio
        {
            get
            {
                if (_guitarraRepositorio == null)
                    return new RepositorioBase<Guitarra>(_context);

                return _guitarraRepositorio;
            }
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}