namespace Cinema_Api.src.Models;

public class Ator(int id, string nome, DateOnly dataNasc)
{
	public Ator()
		: this(default, "", default) { }

	public int Id { get; set; } = id;

	public string Nome { get; set; } = nome;

	public DateOnly DataNasc { get; set; } = dataNasc;
}
