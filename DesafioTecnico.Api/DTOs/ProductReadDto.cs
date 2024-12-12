namespace DesafioTecnico.Api.DTOs
{
  public class ProdutoReadDto
  {
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
  }
}