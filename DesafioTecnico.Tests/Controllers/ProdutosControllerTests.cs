using Moq;
using DesafioTecnico.Api.Controllers;
using DesafioTecnico.Api.Services;
using DesafioTecnico.Api.Models;
using DesafioTecnico.Api.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace DesafioDotNet.Tests.Controllers
{
  public class ProdutosControllerTests
  {
    private readonly Mock<IProdutoService> _mockService;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ProdutosController _controller;

    public ProdutosControllerTests()
    {
      _mockService = new Mock<IProdutoService>();
      _mockMapper = new Mock<IMapper>();
      _controller = new ProdutosController(_mockService.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithListOfProdutoReadDto()
    {
      // Arrange
      var produtos = new List<Produto>
            {
                new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m },
                new Produto { Id = 2, Nome = "Produto 2", Preco = 20.0m }
            };
      var produtosReadDto = new List<ProdutoReadDto>
            {
                new ProdutoReadDto { Id = 1, Nome = "Produto 1", Preco = 10.0m },
                new ProdutoReadDto { Id = 2, Nome = "Produto 2", Preco = 20.0m }
            };

      _mockService.Setup(service => service.GetAllProdutosAsync())
                  .ReturnsAsync(produtos);

      _mockMapper.Setup(mapper => mapper.Map<IEnumerable<ProdutoReadDto>>(produtos))
                 .Returns(produtosReadDto);

      // Act
      var result = await _controller.Get();

      // Assert
      var okResult = Assert.IsType<OkObjectResult>(result.Result);
      var returnProdutos = Assert.IsType<List<ProdutoReadDto>>(okResult.Value);
      Assert.Equal(2, returnProdutos.Count);
      Assert.Contains(returnProdutos, p => p.Nome == "Produto 1");
      Assert.Contains(returnProdutos, p => p.Nome == "Produto 2");
    }

    [Fact]
    public async Task GetById_ProdutoExists_ReturnsOkResult_WithProdutoReadDto()
    {
      // Arrange
      var produto = new Produto { Id = 1, Nome = "Produto 1", Preco = 10.0m };
      var produtoReadDto = new ProdutoReadDto { Id = 1, Nome = "Produto 1", Preco = 10.0m };

      _mockService.Setup(service => service.GetProdutoByIdAsync(1))
                  .ReturnsAsync(produto);

      _mockMapper.Setup(mapper => mapper.Map<ProdutoReadDto>(produto))
                 .Returns(produtoReadDto);

      // Act
      var result = await _controller.GetById(1);

      // Assert
      var okResult = Assert.IsType<OkObjectResult>(result.Result);
      var returnProduto = Assert.IsType<ProdutoReadDto>(okResult.Value);
      Assert.Equal(produtoReadDto.Id, returnProduto.Id);
      Assert.Equal(produtoReadDto.Nome, returnProduto.Nome);
      Assert.Equal(produtoReadDto.Preco, returnProduto.Preco);
    }

    [Fact]
    public async Task GetById_ProdutoDoesNotExist_ReturnsNotFoundResult()
    {
      // Arrange
      _mockService.Setup(service => service.GetProdutoByIdAsync(1))
                  .ReturnsAsync((Produto?)null);

      // Act
      var result = await _controller.GetById(1);

      // Assert
      Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Post_ValidProdutoCreateDto_ReturnsCreatedAtActionResult_WithProdutoReadDto()
    {
      // Arrange
      var produtoCreateDto = new ProdutoCreateDto { Nome = "Novo Produto", Preco = 30.0m };
      var produto = new Produto { Nome = "Novo Produto", Preco = 30.0m };
      var createdProduto = new Produto { Id = 3, Nome = "Novo Produto", Preco = 30.0m };
      var produtoReadDto = new ProdutoReadDto { Id = 3, Nome = "Novo Produto", Preco = 30.0m };

      _mockMapper.Setup(mapper => mapper.Map<Produto>(produtoCreateDto))
                 .Returns(produto);

      _mockService.Setup(service => service.CreateProdutoAsync(produto))
                  .ReturnsAsync(createdProduto);

      _mockMapper.Setup(mapper => mapper.Map<ProdutoReadDto>(createdProduto))
                 .Returns(produtoReadDto);

      // Act
      var result = await _controller.Post(produtoCreateDto);

      // Assert
      var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
      var returnProduto = Assert.IsType<ProdutoReadDto>(createdAtActionResult.Value);
      Assert.Equal(produtoReadDto.Id, returnProduto.Id);
      Assert.Equal(produtoReadDto.Nome, returnProduto.Nome);
      Assert.Equal(produtoReadDto.Preco, returnProduto.Preco);
      Assert.Equal(nameof(_controller.GetById), createdAtActionResult.ActionName);
      Assert.Equal(produtoReadDto.Id, createdAtActionResult.RouteValues?["id"]);
    }

    [Fact]
    public async Task Post_InvalidProdutoCreateDto_ReturnsBadRequestResult()
    {
      // Arrange
      var produtoCreateDto = new ProdutoCreateDto { Nome = "", Preco = -10.0m };
      _controller.ModelState.AddModelError("Nome", "O campo Nome é obrigatório.");
      _controller.ModelState.AddModelError("Preco", "O campo Preco deve ser um valor positivo.");

      // Act
      var result = await _controller.Post(produtoCreateDto);

      // Assert
      var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
      var modelState = Assert.IsType<SerializableError>(badRequestResult.Value);
      Assert.True(modelState.ContainsKey("Nome"));
      Assert.True(modelState.ContainsKey("Preco"));
    }
  }
}
