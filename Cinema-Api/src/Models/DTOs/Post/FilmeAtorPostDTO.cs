namespace Cinema_Api.src.Models.DTOs.Post;

public class FilmeAtorPostDTO
{
	public required AtorPostDTO Ator { get; set; }

	public required string Papel { get; set; }
}
