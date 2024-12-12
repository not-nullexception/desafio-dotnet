using AutoMapper;
using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Models;
using DesafioTecnico.Api.Repositories;
using DesafioTecnico.Api.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DesafioTecnico.Tests
{
    public class ProdutosServiceTests
    {
        private readonly Mock<IProdutoRepository> _produtoRepositoryMock;
        private readonly IProdutoService _produtoService;

        public ProdutosServiceTests()
        {
            _produtoRepositoryMock = new Mock<IProdutoRepository>();
            _produtoService = new ProdutoService(_produtoRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllProdutosAsync_DeveRetornarTodosProdutos()
        {
            // Arrange
            var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m },
                new Produto { Id = 2, Nome = "Produto 2", Preco = 20.0m }
            };
            _produtoRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(produtos);

            // Act
            var result = await _produtoService.GetAllProdutosAsync();

            // Assert
            Assert.Equal(2, ((List<Produto>)result).Count);
            _produtoRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
        }

        [Fact]
        public async Task CreateProdutoAsync_ProdutoValido_DeveAdicionarProduto()
        {
            // Arrange
            var produto = new Produto { Nome = "Novo Produto", Preco = 15.5m };
            _produtoRepositoryMock.Setup(repo => repo.AddAsync(produto)).Returns(Task.CompletedTask);

            // Act
            var result = await _produtoService.CreateProdutoAsync(produto);

            // Assert
            Assert.Equal(produto, result);
            _produtoRepositoryMock.Verify(repo => repo.AddAsync(produto), Times.Once);
        }

        [Fact]
        public async Task GetProdutoByIdAsync_ProdutoExiste_DeveRetornarProduto()
        {
            // Arrange
            var produto = new Produto { Id = 1, Nome = "Produto Existente", Preco = 30.0m };
            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(produto);

            // Act
            var result = await _produtoService.GetProdutoByIdAsync(1);

            // Assert
            Assert.Equal(produto, result);
            _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task GetProdutoByIdAsync_ProdutoNaoExiste_DeveRetornarNull()
        {
            // Arrange
            _produtoRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync((Produto?)null);

            // Act
            var result = await _produtoService.GetProdutoByIdAsync(1);

            // Assert
            Assert.Null(result);
            _produtoRepositoryMock.Verify(repo => repo.GetByIdAsync(1), Times.Once);
        }
    }
}
