using AutoMapper;
using Cinema_Api.src.Config.Mapper;
using Cinema_Api.src.Context;
using Cinema_Api.src.Exceptions;
using Cinema_Api.src.Models;
using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.Post;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Api.src.Service;

public class FilmeService(
	MasterContext masterContext,
	GeneroService generoService,
	DiretorService diretorService
)
{
	private readonly MasterContext _masterContext = masterContext;

	private readonly GeneroService _generoService = generoService;

	private readonly DiretorService _diretorService = diretorService;

	// Ferramenta que transforma objetos de um tipo para objetos de outro
	private readonly Mapper Mapper = new(new MapperConfiguration(AutoMapperConfig.Configurar));

	public List<FilmeGetDTO> TodosOsFilmes()
	{
		var filmes = FilmesComInclude().Select(f => Mapper.Map<Filme, FilmeGetDTO>(f)).ToList();

		return filmes;
	}

	public FilmeGetDTO UmFilme(int id)
	{
		var filme = FilmesComInclude().Where(f => f.Id == id).FirstOrDefault();

		return filme is null
			? throw new EntityNotFoundException($"Uma entidade Filme de Id {id} não existe.")
			: Mapper.Map<Filme, FilmeGetDTO>(filme);
	}

	public Filme NovoFilme(FilmePostDTO filmeDto)
	{
		var filme = AddFilme(filmeDto);

		_masterContext.Filme.Add(filme);
		_masterContext.SaveChanges();

		return filme;
	}

	public void DeletarFilme(int id)
	{
		_masterContext.Filme.Remove(_masterContext.Filme.First(f => f.Id == id));
		_masterContext.SaveChanges();
	}

	#region Métodos Privados

	private Filme AddFilme(FilmePostDTO filmeDto)
	{
		foreach (string dtoGenero in filmeDto.Generos)
		{
			try
			{
				_generoService.NovoGenero(dtoGenero);
			}
			catch (AlreadyExistsException)
			{
				continue;
			}
		}

		// Verifica se um filme com mesmo título e
		// ano de lançamento já existe no banco de dados
		var existe = _masterContext
			.Filme.AsEnumerable()
			.Where(filmeBd =>
				filmeBd.Titulo.Equals(filmeDto.Titulo, StringComparison.OrdinalIgnoreCase)
				&& filmeBd.AnoLancamento == filmeDto.AnoLancamento
			)
			.Any();

		if (existe)
			throw new AlreadyExistsException(
				"Um Filme com título e data de lançamento iguais aos fornecidos já existe."
			);

		var diretor = _diretorService.GetExistenteOuCriar(filmeDto.Diretor);

		var filme = Mapper.Map<FilmePostDTO, Filme>(filmeDto);

		return filme;
	}

	private Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Filme, Ator> FilmesComInclude()
	{
		return _masterContext
			.Filme.Include(f => f.FilmesGeneros)
			.ThenInclude(fg => fg.Genero)
			.Include(f => f.Diretor)
			.Include(f => f.FilmesAtores)
			.ThenInclude(fa => fa.Ator);
	}

	#endregion
}
