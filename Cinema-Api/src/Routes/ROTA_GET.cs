using Cinema_Api.src.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.Results;

namespace Cinema_Api.src.Routes;

public class ROTA_GET
{
	private const string ROTA_BASE = "/api/v1";

	public static void MapGetRoutes(WebApplication app)
	{
		app.MapGet("/", () => Redirect("/swagger"));

		MapearGeneros(app);
		MapearDiretores(app);
		MapearAtores(app);
		MapearFilmes(app);
	}

	private static void MapearGeneros(WebApplication app)
	{
		const string ROTA_GENEROS = $"{ROTA_BASE}/Generos";
		// Todos os Generos
		app.MapGet(
			ROTA_GENEROS,
			(GeneroService generoService) => Ok(generoService.TodosOsGeneros())
		);

		// Um Genero
		app.MapGet(
			$"{ROTA_GENEROS}/{{id}}",
			([FromRoute(Name = "id")] int id, GeneroService generoService) =>
				Ok(generoService.UmGenero(id))
		);
	}

	private static void MapearDiretores(WebApplication app)
	{
		const string ROTA_DIRETORES = $"{ROTA_BASE}/Diretores";

		// Todos os Diretores
		app.MapGet(
			ROTA_DIRETORES,
			(DiretorService diretorService) => Ok(diretorService.TodosOsDiretores())
		);

		// Um Diretor
		app.MapGet(
			$"{ROTA_DIRETORES}/{{id}}",
			([FromRoute(Name = "id")] int id, DiretorService diretorService) =>
				Ok(diretorService.UmDiretor(id))
		);
	}

	private static void MapearAtores(WebApplication app)
	{
		const string ROTA_ATORES = $"{ROTA_BASE}/Atores";

		// Todos os Atores
		app.MapGet(ROTA_ATORES, (AtorService atorService) => Ok(atorService.TodosOsAtores()));

		// Um Ator
		app.MapGet(
			$"{ROTA_ATORES}/{{id}}",
			([FromRoute(Name = "id")] int id, AtorService atorService) => Ok(atorService.UmAtor(id))
		);
	}

	private static void MapearFilmes(WebApplication app)
	{
		const string ROTA_FILMES = $"{ROTA_BASE}/Filmes";

		// Todos os Filmes
		app.MapGet(ROTA_FILMES, (FilmeService filmeService) => Ok(filmeService.TodosOsFilmes()));

		// Um Filme
		app.MapGet(
			$"{ROTA_FILMES}/{{id}}",
			([FromRoute(Name = "id")] int id, FilmeService filmeService) =>
				Ok(filmeService.UmFilme(id))
		);
	}
}
