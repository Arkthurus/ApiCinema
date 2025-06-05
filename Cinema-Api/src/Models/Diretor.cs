namespace Cinema_Api.src.Models;

public record Diretor(int Id, DateOnly DataNasc, string Nome, string Biografia)
{
	public Diretor()
		: this(default, default, "", "") { }
}
