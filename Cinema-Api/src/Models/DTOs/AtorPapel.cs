using Cinema_Api.src.Models.DTOs.Get;

namespace Cinema_Api.src.Models.DTOs;

public class AtorPapel
{
	public required AtorGetDTO Ator { get; set; }

	public required string Papel { get; set; }
}
