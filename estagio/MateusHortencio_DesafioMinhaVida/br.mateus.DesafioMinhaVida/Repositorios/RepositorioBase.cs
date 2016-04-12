using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace br.mateus.DesafioMinhaVida.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : class
    {
        private DbContext _context;
        private DbSet<T> _dbSet;
        
        public RepositorioBase(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Adicionar(T entidade)
        {
            _dbSet.Add(entidade);
        }

        public void Atualizar(T entidade)
        {
            _context.Entry(entidade).State = EntityState.Modified;
        }

        public void Deletar(T entidade)
        {
            _dbSet.Remove(entidade);
        }

        public ICollection<T> Listar()
        {
            return _dbSet.ToList();
        }
        
        public ICollection<T> ListarPor(Expression<Func<T, bool>> lambda)
        {
            return _dbSet.Where(lambda).ToList();
        }

        public T ProcurarPorId(int? id)
        {
            if (id == null)
                return null;

            return _dbSet.Find(id);
        }
    }
}