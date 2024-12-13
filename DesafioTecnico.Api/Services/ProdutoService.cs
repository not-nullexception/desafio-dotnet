using DesafioTecnico.Api.Models;
using DesafioTecnico.Api.Repositories;

namespace DesafioTecnico.Api.Services
{
  public class ProdutoService : IProdutoService
  {
    private readonly IProdutoRepository _produtoRepository;

    public ProdutoService(IProdutoRepository produtoRepository)
    {
      _produtoRepository = produtoRepository;
    }

    public async Task<IEnumerable<Produto>> GetAllProdutosAsync()
    {
      return await _produtoRepository.GetAllAsync();
    }

    public async Task<Produto?> GetProdutoByIdAsync(int id)
    {
      return await _produtoRepository.GetByIdAsync(id);
    }

    public async Task<Produto> CreateProdutoAsync(Produto produto)
    {
      if (string.IsNullOrWhiteSpace(produto.Nome) || produto.Preco < 0)
        throw new ArgumentException("Produto inválido.");

      return await _produtoRepository.AddAsync(produto);
    }
  }
}
