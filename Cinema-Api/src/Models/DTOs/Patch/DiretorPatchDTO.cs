namespace Cinema_Api.src.Models.DTOs.HttpPatch;

public class DiretorPatchDTO
{
	public string? Nome { get; set; }

	public DateOnly? DataNasc { get; set; }

	public string? Biografia { get; set; }
}
