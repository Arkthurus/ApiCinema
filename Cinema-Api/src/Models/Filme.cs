namespace Cinema_Api.src.Models;

public class Filme
{
	public int Id { get; set; }

	public string Titulo { get; set; } = "";

	public int AnoLancamento { get; set; }

	public string Sinopse { get; set; } = "";

	public float NotaIMDB { get; set; }

	public required int DiretorId { get; set; }

	public required Diretor Diretor { get; set; }

	public ICollection<FilmeGenero> FilmesGeneros { get; set; } = [];

	public ICollection<FilmeAtor> FilmesAtores { get; set; } = [];
}
