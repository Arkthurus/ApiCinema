namespace Cinema_Api.src.Models.DTOs.Get;

public record AtorGetDTO(string Nome, DateOnly DataNasc)
{
	public AtorGetDTO()
		: this("", default) { }
}
