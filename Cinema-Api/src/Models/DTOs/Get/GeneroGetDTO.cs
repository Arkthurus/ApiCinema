namespace Cinema_Api.src.Models.DTOs.Get;

public record GeneroGetDTO(int Id, string Nome)
{
	public GeneroGetDTO()
		: this(default, "") { }
}
