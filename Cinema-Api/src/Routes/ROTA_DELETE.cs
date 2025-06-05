using Cinema_Api.src.Models;
using Cinema_Api.src.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.Results;

namespace Cinema_Api.src.Routes;

public class ROTA_DELETE
{
    private const string ROTA_BASE = "/api/v1";

    public static void MapDeleteRoutes(WebApplication app)
    {
        MapearGeneros(app);
        MapearDiretores(app);
        MapearAtores(app);
        MapearFilmes(app);
    }
    private static void MapearGeneros(WebApplication app)
    {
        const string ROTA_GENEROS = $"{ROTA_BASE}/Generos";

        app.MapDelete(
        $"{ROTA_GENEROS}/{{id}}",
        ([FromRoute(Name = "id")] int id, GeneroService generoService) =>
            {
                generoService.DeletarGenero(id);
                return NoContent();
            }
        );
    }
    private static void MapearDiretores(WebApplication app)
    {
        const string ROTA_DIRETORES = $"{ROTA_BASE}/Diretores";

        app.MapDelete(
        $"{ROTA_DIRETORES}/{{id}}",
        ([FromRoute(Name = "id")] int id, DiretorService diretorService) =>
            {
                diretorService.DeletarDiretor(id);
                return NoContent();
            }
        );
    }
    private static void MapearFilmes(WebApplication app)
    {
        const string ROTA_FILMES = $"{ROTA_BASE}/Filmes";

        app.MapDelete(
        $"{ROTA_FILMES}/{{id}}",
        ([FromRoute(Name = "id")] int id, FilmeService filmeService) =>
            {
                filmeService.DeletarFilme(id);
                return NoContent();
            }
        );
    }
        private static void MapearAtores(WebApplication app)
    {
        const string ROTA_ATORES = $"{ROTA_BASE}/Atores";

        app.MapDelete(
        $"{ROTA_ATORES}/{{id}}",
        ([FromRoute(Name = "id")] int id, AtorService atorService) =>
            {
                atorService.DeletarAtor(id);
                return NoContent();
            }
        );
    }
    
}