using DesafioTecnico.Api.Models;

namespace DesafioTecnico.Api.Repositories
{
  public interface IProdutoRepository
  {
    Task<IEnumerable<Produto>> GetAllAsync();
    Task<Produto?> GetByIdAsync(int id);
    Task AddAsync(Produto produto);
  }
}