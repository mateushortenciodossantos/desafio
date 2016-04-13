using br.mateus.DesafioMinhaVida.Models;
using System.Data.Entity;

namespace br.mateus.DesafioMinhaVida.Context
{
    public class ProdutoContext : DbContext
    {
        public DbSet<Guitarra> Guitarras { get; set; }
    }
}