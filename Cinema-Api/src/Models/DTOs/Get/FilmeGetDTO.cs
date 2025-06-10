namespace Cinema_Api.src.Models.DTOs.Get;

public class FilmeGetDTO
{
	public required int Id { get; set; }

	public required string Titulo { get; set; }

	public required int AnoLancamento { get; set; }

	public required string Sinopse { get; set; }

	public required float NotaIMDB { get; set; }

	public required List<string> Generos { get; set; }

	public required DiretorGetDTO Diretor { get; set; }

	public required List<FilmeAtorGetDTO> Atores { get; set; }
}
