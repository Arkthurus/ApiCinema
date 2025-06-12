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
public class FilmesController(FilmeService service) : ControllerBase
{
	private FilmeService FilmesService { get; } = service;

	[HttpGet]
	public ActionResult<List<FilmeGetDTO>> TodosOsFilmes()
	{
		var filmes = FilmesService.TodosOsFilmes();

		return Ok(filmes);
	}

	[HttpGet("{id}")]
	public ActionResult<FilmeGetDTO> UmFilme([FromRoute(Name = "id")] int id)
	{
		var filme = FilmesService.UmFilme(id);

		return filme is null ? NotFound() : Ok(filme);
	}

	[HttpGet("Filtrar")]
	public ActionResult<List<FilmeGetDTO>> Filtrar(
		[FromBody(EmptyBodyBehavior = Disallow)] FilmeFilterDTO filtro
	)
	{
		var filmes = FilmesService.Filtrar(filtro);

		return Ok(filmes);
	}

	[HttpPost]
	public ActionResult<FilmeGetDTO> NovoFilme([FromBody] FilmePostDTO filme)
	{
		var filmeCriado = FilmesService.AddFilme(filme);

		if (filmeCriado is null)
		{
			return Conflict("O filme já existe no banco de dados.");
		}

		return CreatedAtAction(nameof(UmFilme), new { id = filmeCriado.Id }, filme);
	}

	// [HttpPatch("{id}")]
	// public ActionResult<FilmeGetDTO> ModificarFilme(int id, FilmePatchDTO patchDTO)
	// {
	// 	var modificado = FilmesService.ModificarFilme(id, patchDTO);

	// 	return Ok(modificado);
	// }

	[HttpDelete("{id}")]
	public ActionResult DeletarFilme([FromRoute(Name = "id")] int id)
	{
		FilmesService.DeletarFilme(id);

		return NoContent();
	}
}
