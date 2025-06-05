namespace Cinema_Api.src.Models.DTOs.Get;

public record DiretorGetDTO(string Nome, DateOnly DataNasc, string? Biografia)
{
	public DiretorGetDTO()
		: this("", default, null) { }
}
