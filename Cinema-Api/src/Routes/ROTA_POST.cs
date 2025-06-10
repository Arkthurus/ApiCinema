using Cinema_Api.src.Models;
using Cinema_Api.src.Models.DTOs.Post;
using Cinema_Api.src.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.Results;

namespace Cinema_Api.src.Routes;

public class ROTA_POST
{
    private const string ROTA_BASE = "/api/v1";

    public static void MapPostRoutes(WebApplication app)
    {
        MapearGeneros(app);
        MapearDiretores(app);
        MapearAtores(app);
        MapearFilmes(app);
    }

    private static void MapearGeneros(WebApplication app)
    {
        const string ROTA_GENEROS = $"{ROTA_BASE}/Generos";

        app.MapPost($"{ROTA_GENEROS}", ([FromBody]GeneroPostDTO generoPostDto, GeneroService generoService) =>
        {
            var genero = generoService.NovoGenero(generoPostDto);
            return Created($"{ROTA_GENEROS}/{genero.Id}", genero);
        });
    }
    private static void MapearDiretores(WebApplication app)
    {
        const string ROTA_DIRETORES = $"{ROTA_BASE}/Diretores";

        app.MapPost($"{ROTA_DIRETORES}", ([FromBody]DiretorPostDTO diretorPostDto, DiretorService diretorService) =>
        {
            var diretor = diretorService.NovoDiretor(diretorPostDto);
            return Created($"{ROTA_DIRETORES}/{diretor.Id}", diretor);
        });
    }
    private static void MapearAtores(WebApplication app)
    {
        const string ROTA_ATORES = $"{ROTA_BASE}/Atores";

        app.MapPost($"{ROTA_ATORES}", ([FromBody]AtorPostDTO atorPostDto, AtorService atorService) =>
        {
            var ator = atorService.NovoAtor(atorPostDto);
            return Created($"{ROTA_ATORES}/{ator.Id}", ator);
        });
    }
    private static void MapearFilmes(WebApplication app)
    {
        const string ROTA_FILMES = $"{ROTA_BASE}/Filmes";

        app.MapPost($"{ROTA_FILMES}", ([FromBody]FilmePostDTO filmePostDto, FilmeService filmeService) =>
        {
            var filme = filmeService.AddFilme(filmePostDto);
            return Created($"{ROTA_FILMES}/{filme.Id}", filme);
        });
    }
}