using AutoMapper;
using DesafioTecnico.Api.Models;
using DesafioTecnico.Api.DTOs;

namespace DesafioTecnico.Api.Mappings
{
  public class ProdutoProfile : Profile
  {
    public ProdutoProfile()
    {
      CreateMap<Produto, ProdutoReadDto>();
      CreateMap<ProdutoCreateDto, Produto>();
    }
  }
}