using DesafioTecnico.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioTecnico.Api.Services
{
  public interface IProdutoService
  {
    Task<IEnumerable<Produto>> GetAllProdutosAsync();
    Task<Produto?> GetProdutoByIdAsync(int id);
    Task<Produto> CreateProdutoAsync(Produto produto);
  }
}
