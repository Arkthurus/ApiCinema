namespace Cinema_Api.src.Models.DTOs.Get;

public class FilmeAtorGetDTO
{
	public required AtorGetDTO Ator { get; set; }

	public required string Papel { get; set; }
}
