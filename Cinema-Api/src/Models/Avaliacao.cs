namespace Cinema_Api.src.Models;

public class Avaliacao(string autor, float estrelas, string resenha)
{
	public string Autor { get; set; } = autor;

	public float Estrelas { get; set; } = estrelas;

	public string Resenha { get; set; } = resenha;
}
