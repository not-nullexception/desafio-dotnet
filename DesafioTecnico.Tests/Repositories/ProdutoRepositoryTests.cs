using DesafioTecnico.Api.Repositories;
using DesafioTecnico.Api.Models;
using DesafioTecnico.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioDotNet.Tests.Repositories
{
  public class ProdutosRepositoryTests : IDisposable
  {
    private readonly AppDbContext _context;
    private readonly ProdutoRepository _repository;

    public ProdutosRepositoryTests()
    {
      var options = new DbContextOptionsBuilder<AppDbContext>()
                      .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                      .Options;
      _context = new AppDbContext(options);

      _repository = new ProdutoRepository(_context);
    }

    public void Dispose()
    {
      _context.Dispose();
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllProdutos()
    {
      // Arrange
      _context.Produtos.AddRange(
          new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m },
          new Produto { Id = 2, Nome = "Produto 2", Preco = 20.0m }
      );
      await _context.SaveChangesAsync();

      // Act
      var produtos = await _repository.GetAllAsync();

      // Assert
      Assert.NotNull(produtos);
      Assert.Equal(2, ((List<Produto>)produtos).Count);
      Assert.Contains(produtos, p => p.Nome == "Produto 1");
      Assert.Contains(produtos, p => p.Nome == "Produto 2");
    }

    [Fact]
    public async Task GetByIdAsync_ProdutoExists_ReturnsProduto()
    {
      // Arrange
      var produto = new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m };
      _context.Produtos.Add(produto);
      await _context.SaveChangesAsync();

      // Act
      var result = await _repository.GetByIdAsync(1);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(produto.Id, result.Id);
      Assert.Equal(produto.Nome, result.Nome);
      Assert.Equal(produto.Preco, result.Preco);
    }

    [Fact]
    public async Task GetByIdAsync_ProdutoDoesNotExist_ReturnsNull()
    {
      // Act
      var result = await _repository.GetByIdAsync(1);

      // Assert
      Assert.Null(result);
    }

    [Fact]
    public async Task AddAsync_AddsProduto()
    {
      // Arrange
      var novoProduto = new Produto { Nome = "Novo Produto", Preco = 30.0m };

      // Act
      var result = await _repository.AddAsync(novoProduto);
      await _context.SaveChangesAsync();

      // Assert
      Assert.NotNull(result);
      Assert.True(result.Id > 0);
      var produto = await _context.Produtos.FindAsync(result.Id);
      Assert.NotNull(produto);
      Assert.Equal(novoProduto.Nome, produto.Nome);
      Assert.Equal(novoProduto.Preco, produto.Preco);
    }
  }
}
