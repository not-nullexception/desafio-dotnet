using Microsoft.EntityFrameworkCore;
using DesafioTecnico.Api.Models;

namespace DesafioTecnico.Api.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Produto> Produtos { get; set; }
  }
}