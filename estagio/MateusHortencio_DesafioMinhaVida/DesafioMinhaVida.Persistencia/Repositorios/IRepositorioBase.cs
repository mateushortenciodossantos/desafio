using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace br.mateus.DesafioMinhaVida.DAO.Repositorios
{
    public interface IRepositorioBase<T>
    {
        void Adicionar(T entidade);
        void Atualizar(T entidade);
        void Deletar(T entidade);
        ICollection<T> Listar();
        ICollection<T> ListarPor(Expression<Func<T, bool>> lambda);
        T ProcurarPorId(int? id);
    }
}
