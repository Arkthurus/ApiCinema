using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.Post;
using Cinema_Api.src.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cinema_Api.src.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AtorController(AtorService service) : ControllerBase
{
	private AtorService AtorService { get; } = service;

	public ActionResult<List<AtorGetDTO>> TodosOsAtores()
	{
		return AtorService.TodosOsAtores();
	}

	[HttpGet("{id}")]
	public ActionResult<AtorGetDTO> UmAtor([FromRoute(Name = "Id")] int Id)
	{
		var ator = AtorService.UmAtor(Id);

		return ator is null ? NotFound() : Ok(ator);
	}

	[HttpPost]
	public ActionResult<AtorGetDTO> NovoAtor([FromBody] AtorPostDTO ator)
	{
		var atorCriado = AtorService.NovoAtor(ator);

		if (atorCriado is null)
		{
			return Conflict("O Ator já existe no banco de dados.");
		}

		return CreatedAtAction(nameof(UmAtor), new { Id = atorCriado.Id }, ator);
	}

	[HttpDelete("{Id}")]
	public ActionResult<List<AtorGetDTO>> DeletarAtor(int Id)
	{
		AtorService.DeletarAtor(Id);

		return NoContent();
	}
}
