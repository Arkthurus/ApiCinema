using AutoMapper;
using Cinema_Api.src.Config.Mapper;
using Cinema_Api.src.Context;
using Cinema_Api.src.Exceptions;
using Cinema_Api.src.Models;
using Cinema_Api.src.Models.DTOs.Filter;
using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.HttpPatch;
using Cinema_Api.src.Models.DTOs.Post;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Api.src.Service;

public class FilmeService(
	MasterContext masterContext,
	GeneroService generoService,
	DiretorService diretorService,
	AtorService atorService
)
{
	private readonly MasterContext _masterContext = masterContext;

	private readonly GeneroService _generoService = generoService;

	private readonly DiretorService _diretorService = diretorService;

	private readonly AtorService _atorService = atorService;

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

	public List<FilmeGetDTO> Filtrar(FilmeFilterDTO filtro)
	{
		var filmes = FilmesComInclude()
			.AsEnumerable()
			.Select(f => Mapper.Map<Filme, FilmeGetDTO>(f));

		if (filtro.Titulo is not null)
		{
			filmes = filmes.Where(f =>
				f.Titulo.Contains(filtro.Titulo, StringComparison.OrdinalIgnoreCase)
			);
		}

		if (filtro.AnoLancamento is not null)
		{
			filmes = filmes.Where(f => f.AnoLancamento == filtro.AnoLancamento);
		}

		if (filtro.Sinopse is not null)
		{
			filmes = filmes.Where(f =>
				f.Sinopse.Contains(filtro.Sinopse, StringComparison.OrdinalIgnoreCase)
			);
		}

		if (filtro.Generos is not null && filtro.Generos.Count > 0)
		{
			foreach (var genero in filtro.Generos)
			{
				filmes = filmes.Where(f =>
					f.Generos.Any(g => g.Equals(genero, StringComparison.OrdinalIgnoreCase))
				);
			}
		}

		if (filtro.Diretor is not null)
		{
			filmes = filmes.Where(f =>
				f.Diretor.Nome.Equals(filtro.Diretor, StringComparison.OrdinalIgnoreCase)
			);
		}

		return [.. filmes];
	}

	public Filme AddFilme(FilmePostDTO filmeDto)
	{
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

		var filme = Mapper.Map<FilmePostDTO, Filme>(filmeDto);

		_masterContext.Filme.Add(filme);
		_masterContext.SaveChanges();

		// Gêneros
		foreach (string dtoGenero in filmeDto.Generos)
		{
			Genero genero = _generoService.GetExistenteOuCriar(dtoGenero);

			_masterContext.FilmeGenero.Add(new() { Filme = filme, Genero = genero });
		}

		// Atores
		foreach (FilmeAtorPostDTO dtoPapel in filmeDto.Papeis)
		{
			var ator = _atorService.GetExistenteOuCriar(dtoPapel.Ator);

			_masterContext.FilmeAtor.Add(
				new()
				{
					Filme = filme,
					Ator = ator,
					Papel = dtoPapel.Papel,
				}
			);
		}

		_masterContext.SaveChanges();

		return filme;
	}

	// public FilmeGetDTO ModificarFilme(int id, FilmePatchDTO patchDto)
	// {
	// 	var filme = _masterContext
	// 		.Filme.Include(filme => filme.FilmesGeneros)
	// 		.ThenInclude(fg => fg.Genero)
	// 		.Include(filme => filme.Diretor)
	// 		.First(filme => filme.Id == id);

	// 	if (patchDto.Titulo is not null)
	// 		filme.Titulo = patchDto.Titulo;

	// 	if (patchDto.AnoLancamento is not null)
	// 		filme.AnoLancamento = patchDto.AnoLancamento.Value;

	// 	if (patchDto.NotaIMDB is not null)
	// 		filme.NotaIMDB = patchDto.NotaIMDB.Value;

	// 	if (patchDto.Generos is not null)
	// 	{
	// 		// Remove os filmesGeneros do objeto filme para substituí-los
	// 		filme.FilmesGeneros.Clear();

	// 		foreach (var nomeGenero in patchDto.Generos) // Efetivamente Adiciona os novos gêneros ao filme..,
	// 		{
	// 			var genero = _generoService.GetExistenteOuCriar(nomeGenero);
	// 			filme.FilmesGeneros.Add(new() { FilmeId = filme.Id, GeneroId = genero.Id });
	// 		}
	// 	}

	// 	if (patchDto.Diretor is null)
	// 	{
	// 		_masterContext.SaveChanges();
	// 		return Mapper.Map<Filme, FilmeGetDTO>(filme); // Não há necessidade de modificar o diretor, então retorna
	// 	}

	// 	if (patchDto.IsNovoDiretor is null) // Diretor foi fornecido, então IsNovoDiretor deve ser fornecido também
	// 	{
	// 		throw new BadHttpRequestException(
	// 			"Se um diretor for fornecido, é necessário especificar se "
	// 				+ "ele deve ser interpretado como um diretor novo através da "
	// 				+ "propriedade \"IsNovoDiretor\" (true ou false)"
	// 		);
	// 	}

	// 	if (patchDto.IsNovoDiretor.Value)
	// 	{
	// 		// É um novo diretor, então adiciona ele à database
	// 		var diretor = _diretorService.NovoDiretor(patchDto.Diretor);

	// 		filme.Diretor = diretor;
	// 	}
	// 	else
	// 	{
	// 		var diretor = _diretorService.SingleByNomeAndDataNasc(
	// 			patchDto.Diretor.Nome,
	// 			patchDto.Diretor.DataNasc
	// 		);

	// 		filme.Diretor = diretor ?? filme.Diretor;
	// 	}

	// 	_masterContext.SaveChanges();
	// 	return Mapper.Map<Filme, FilmeGetDTO>(filme);
	// }

	public void DeletarFilme(int id)
	{
		_masterContext.Filme.Remove(_masterContext.Filme.First(f => f.Id == id));
		_masterContext.SaveChanges();
	}

	#region Métodos Privados

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
