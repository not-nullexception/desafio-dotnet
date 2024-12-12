using DesafioTecnico.Api.Data;
using DesafioTecnico.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioTecnico.Api.Repositories
{
  public class ProdutoRepository : IProdutoRepository
  {
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Produto>> GetAllAsync()
    {
      return await _context.Produtos.ToListAsync();
    }

    public async Task<Produto?> GetByIdAsync(int id)
    {
      return await _context.Produtos.FindAsync(id);
    }

    public async Task AddAsync(Produto produto)
    {
      await _context.Produtos.AddAsync(produto);
      await _context.SaveChangesAsync();
    }
  }
}
