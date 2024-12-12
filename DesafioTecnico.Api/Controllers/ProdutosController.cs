using AutoMapper;
using DesafioTecnico.Api.DTOs;
using DesafioTecnico.Api.Models;
using DesafioTecnico.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioTecnico.Api.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class ProdutosController : ControllerBase
  {
    private readonly IProdutoService _produtoService;
    private readonly IMapper _mapper;

    public ProdutosController(IProdutoService produtoService, IMapper mapper)
    {
      _produtoService = produtoService;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoReadDto>>> Get()
    {
      var produtos = await _produtoService.GetAllProdutosAsync();
      return Ok(_mapper.Map<IEnumerable<ProdutoReadDto>>(produtos));
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoReadDto>> Post([FromBody] ProdutoCreateDto produtoCreateDto)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var produto = _mapper.Map<Produto>(produtoCreateDto);
      var createdProduto = await _produtoService.CreateProdutoAsync(produto);
      var produtoReadDto = _mapper.Map<ProdutoReadDto>(createdProduto);

      return CreatedAtAction(nameof(GetById), new { id = produtoReadDto.Id }, produtoReadDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoReadDto>> GetById(int id)
    {
      var produto = await _produtoService.GetProdutoByIdAsync(id);
      if (produto == null)
      {
        return NotFound();
      }

      return Ok(_mapper.Map<ProdutoReadDto>(produto));
    }
  }
}
