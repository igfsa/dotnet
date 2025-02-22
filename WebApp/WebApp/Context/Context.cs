// Responsável pelo contexto da aplicação

using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Context
{
    public class AppDbContext : DbContext //DbContext representa uma sessão com o banco de dados sendo a ponte entre as entidades e o banco
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { }
        public DbSet<Categoria> Categorias { get; set; }
        // Mapeando do banco (Categorias) para a entidade do domínio (Categoria)
        public DbSet<Produto> Produtos { get; set; }    

    }
}
