namespace Cinema_Api.src.Models.DTOs.Filter;

public class FilmeFilterDTO
{
	public string? Titulo { get; set; }

	public int? AnoLancamento { get; set; }

	public string? Sinopse { get; set; }

	public List<string>? Generos { get; set; }

	public string? Diretor { get; set; }
}