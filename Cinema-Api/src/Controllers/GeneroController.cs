using Cinema_Api.src.Models.DTOs.Filter;
using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.HttpPatch;
using Cinema_Api.src.Models.DTOs.Post;
using Cinema_Api.src.Service;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Mvc.ModelBinding.EmptyBodyBehavior;

namespace Cinema_Api.src.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class GeneroController(GeneroService service) : ControllerBase
{
	private GeneroService GeneroService { get; } = service;

	[HttpGet]
	public ActionResult<List<GeneroGetDTO>> TodosOsGeneros()
	{
		var generos = GeneroService.TodosOsGeneros();

		return Ok(generos);
	}

	[HttpGet("{id}")]
	public ActionResult<GeneroGetDTO> UmGenero([FromRoute(Name = "id")] int id)
	{
		var genero = GeneroService.UmGenero(id);

		return genero is null ? NotFound() : Ok(genero);
	}

	[HttpPost]
	public ActionResult<GeneroGetDTO> NovoGenero([FromBody] GeneroPostDTO genero)
	{
		var generoCriado = GeneroService.NovoGenero(genero);

		if (generoCriado is null)
		{
			return Conflict("O genero já existe no banco de dados.");
		}

		return CreatedAtAction(nameof(UmGenero), new { id = generoCriado.Id }, genero);
	}

	[HttpDelete]
	public ActionResult DeletarGenero(int Id)
	{
		GeneroService.DeletarGenero(Id);

		return NoContent();
	}
}
