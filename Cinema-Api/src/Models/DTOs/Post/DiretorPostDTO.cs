using Cinema_Api.src.Models.DTOs.Get;

namespace Cinema_Api.src.Models.DTOs.Post;

public class DiretorPostDTO
{
	public required DateOnly DataNasc { get; set; }

	public required string Nome { get; set; }

	public required string Biografia { get; set; }
}
