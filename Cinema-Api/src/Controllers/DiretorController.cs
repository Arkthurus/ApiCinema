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
public class DiretorController(DiretorService service) : ControllerBase
{
	private DiretorService DiretorService { get; } = service;

	[HttpGet("{id}")]
	public ActionResult<DiretorGetDTO> UmDiretor([FromRoute(Name = "Id")] int Id)
	{
		var diretor = DiretorService.UmDiretor(Id);

		return diretor is null ? NotFound() : Ok(diretor);
	}

	[HttpPost]
	public ActionResult<DiretorGetDTO> NovoDiretor([FromBody] DiretorPostDTO diretor)
	{
		var diretorCriado = DiretorService.NovoDiretor(diretor);

		if (diretorCriado is null)
		{
			return Conflict("O Diretor já existe no banco de dados.");
		}

		return CreatedAtAction(nameof(UmDiretor), new { Id = diretorCriado.Id }, diretor);
	}

	[HttpDelete("{Id}")]
	public ActionResult<List<DiretorGetDTO>> DeletarDiretor(int Id)
	{
		DiretorService.DeletarDiretor(Id);

		return NoContent();
	}

	[HttpGet]
	public ActionResult<List<DiretorGetDTO>> TodosOsDiretores()
	{
		var diretores = DiretorService.TodosOsDiretores();

		return Ok(diretores);
	}
}
