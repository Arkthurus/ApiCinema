using AutoMapper;
using Cinema_Api.src.Config.Mapper;
using Cinema_Api.src.Context;
using Cinema_Api.src.Exceptions;
using Cinema_Api.src.Models;
using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.Post;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Api.src.Service;

public class GeneroService(MasterContext masterContext)
{
	private readonly MasterContext _masterContext = masterContext;

	private readonly Mapper Mapper = new(new MapperConfiguration(AutoMapperConfig.Configurar));

	/// <summary>
	/// Cria um novo gênero, se o gênero fornecido ainda não
	/// existir no banco de dados.
	/// </summary>
	/// <param name="nomeGenero">O nome do gênero a ser criado</param>
	/// <returns>O gênero criado</returns>
	/// <exception cref="Quando o gênero fornecido já existe - AlreadyExistsException"></exception>
	public Genero NovoGenero(string nomeGenero)
	{
		var existe =
			_masterContext.Genero.FirstOrDefault(g => g.Nome.Equals(nomeGenero)) is not null;

		if (existe)
			throw new AlreadyExistsException(
				$"O gênero de nome {nomeGenero} já existe no banco de dados."
			);

		Genero genero = new() { Nome = nomeGenero };
		_masterContext.Genero.Add(genero);
		_masterContext.SaveChanges();

		return genero;
	}

	public Genero GetExistenteOuCriar(string nome)
	{
		var genero = SingleByNome(nome);

		genero ??= CriarGeneroSemVerificar(nome); // Se for nulo, cria um novo

		return genero;
	}

	private Genero? SingleByNome(string nome)
	{
		return _masterContext
			.Genero.AsEnumerable()
			.FirstOrDefault(g => g.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
	}

	private Genero CriarGeneroSemVerificar(string nome)
	{
		var novoGenero = new Genero() { Nome = nome };

		_masterContext.Genero.Add(novoGenero);
		_masterContext.SaveChanges();
		return novoGenero;
	}

	public List<GeneroGetDTO> TodosOsGeneros()
	{
		return _masterContext.Genero.Select(g => Mapper.Map<Genero, GeneroGetDTO>(g)).ToList();
	}

	public GeneroGetDTO UmGenero(int id)
	{
		var genero = _masterContext
			.Genero.Where(g => g.Id == id)
			.Select(g => Mapper.Map<Genero, GeneroGetDTO>(g))
			.FirstOrDefault();

		return genero ?? throw new EntityNotFoundException("Genero não encontrado.");
	}

	public Genero NovoGenero(GeneroPostDTO generoDto)
	{
		var existe = _masterContext
			.Genero.AsEnumerable()
			.Where(generoBd =>
				generoBd.Nome.Equals(generoDto.Nome, StringComparison.OrdinalIgnoreCase)
			)
			.Any();

		if (existe)
			throw new AlreadyExistsException("Um Genero com título igual ao fornecido já existe.");

		var genero = Mapper.Map<GeneroPostDTO, Genero>(generoDto);

		_masterContext.Genero.Add(genero);

		_masterContext.SaveChanges();

		return genero;
	}

	public void DeletarGenero(int id)
	{
		_masterContext.Genero.Remove(_masterContext.Genero.First(f => f.Id == id));
		_masterContext.SaveChanges();
	}
}
