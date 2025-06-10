namespace Cinema_Api.src.Models.DTOs.Get;

public record DiretorGetDTO(int Id, string Nome, DateOnly DataNasc, string? Biografia)
{
	public DiretorGetDTO()
		: this(default, "", default, null) { }
}
