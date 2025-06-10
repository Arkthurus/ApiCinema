using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.Post;

namespace Cinema_Api.src.Models.DTOs;

public class AtorPapel
{
	public required AtorPostDTO Ator { get; set; }

	public required string Papel { get; set; }
}
