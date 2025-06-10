namespace Cinema_Api.src.Models.DTOs.Get;

public record AtorGetDTO(int Id, string Nome, DateOnly DataNasc)
{
	public AtorGetDTO()
		: this(default, "", default) { }
}
