using br.mateus.DesafioMinhaVida.Models.Context;
using br.mateus.DesafioMinhaVida.Models.Models;
using br.mateus.DesafioMinhaVida.DAO.Repositorios;

namespace br.mateus.DesafioMinhaVida.DAO.UnitsOfWork
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