using br.mateus.DesafioMinhaVida.Models.Models;
using System.Data.Entity;

namespace br.mateus.DesafioMinhaVida.Models.Context
{
    public class ProdutoContext : DbContext
    {
        public DbSet<Guitarra> Guitarras { get; set; }
    }
}