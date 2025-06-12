namespace Cinema_Api.src.Models;

public class Genero
{
	public int Id { get; set; }

	public string Nome { get; set; } = "";

	public ICollection<FilmeGenero> FilmesGeneros { get; set; } = [];
}
