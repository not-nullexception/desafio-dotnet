using Moq;
using DesafioTecnico.Api.Services;
using DesafioTecnico.Api.Repositories;
using DesafioTecnico.Api.Models;

namespace DesafioDotNet.Tests.Services
{
  public class ProdutoServiceTests
  {
    private readonly Mock<IProdutoRepository> _mockRepository;
    private readonly IProdutoService _service;

    public ProdutoServiceTests()
    {
      _mockRepository = new Mock<IProdutoRepository>();
      _service = new ProdutoService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllProdutosAsync_ReturnsAllProdutos()
    {
      // Arrange
      var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m },
                new Produto { Id = 2, Nome = "Produto 2", Preco = 20.0m }
            };
      _mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(produtos);

      // Act
      var result = await _service.GetAllProdutosAsync();

      // Assert
      Assert.NotNull(result);
      Assert.Equal(2, ((List<Produto>)result).Count);
      Assert.Contains(result, p => p.Nome == "Produto 1");
      Assert.Contains(result, p => p.Nome == "Produto 2");
    }

    [Fact]
    public async Task GetProdutoByIdAsync_ProdutoExists_ReturnsProduto()
    {
      // Arrange
      var produto = new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m };
      _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

      // Act
      var result = await _service.GetProdutoByIdAsync(1);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(produto.Id, result.Id);
      Assert.Equal(produto.Nome, result.Nome);
      Assert.Equal(produto.Preco, result.Preco);
    }

    [Fact]
    public async Task GetProdutoByIdAsync_ProdutoDoesNotExist_ReturnsNull()
    {
      // Arrange
      _mockRepository.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Produto?)null);

      // Act
      var result = await _service.GetProdutoByIdAsync(1);

      // Assert
      Assert.Null(result);
    }

    [Fact]
    public async Task CreateProdutoAsync_ValidProduto_ReturnsCreatedProduto()
    {
      // Arrange
      var novoProduto = new Produto { Nome = "Novo Produto", Preco = 30.0m };
      var produtoCriado = new Produto { Id = 3, Nome = "Novo Produto", Preco = 30.0m };
      _mockRepository.Setup(repo => repo.AddAsync(novoProduto)).ReturnsAsync(produtoCriado);

      // Act
      var result = await _service.CreateProdutoAsync(novoProduto);

      // Assert
      Assert.NotNull(result);
      Assert.Equal(produtoCriado.Id, result.Id);
      Assert.Equal(produtoCriado.Nome, result.Nome);
      Assert.Equal(produtoCriado.Preco, result.Preco);
    }

    [Fact]
    public async Task CreateProdutoAsync_InvalidProduto_ThrowsArgumentException()
    {
      // Arrange
      var produtoInvalido1 = new Produto { Nome = "", Preco = 10.0m };
      var produtoInvalido2 = new Produto { Nome = "Produto Inválido", Preco = -5.0m };

      // Act & Assert for produtoInvalido1
      var exception1 = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateProdutoAsync(produtoInvalido1));
      Assert.Equal("Produto inválido.", exception1.Message);

      // Act & Assert for produtoInvalido2
      var exception2 = await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateProdutoAsync(produtoInvalido2));
      Assert.Equal("Produto inválido.", exception2.Message);
    }
  }
}
