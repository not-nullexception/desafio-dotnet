using System.ComponentModel.DataAnnotations;

namespace DesafioTecnico.Api.DTOs
{
  public class ProdutoCreateDto
  {
    [Required(ErrorMessage = "O nome é obrigatório.")]
    public string Nome { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal Preco { get; set; }
  }
}