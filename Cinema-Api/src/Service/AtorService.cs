using AutoMapper;
using Cinema_Api.src.Config.Mapper;
using Cinema_Api.src.Context;
using Cinema_Api.src.Exceptions;
using Cinema_Api.src.Models;
using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.Post;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Api.src.Service;

public class AtorService(MasterContext masterContext)
{
	private readonly MasterContext _masterContext = masterContext;

	private readonly Mapper Mapper = new(new MapperConfiguration(AutoMapperConfig.Configurar));

	public List<AtorGetDTO> TodosOsAtores()
	{
		return [.. _masterContext.Ator.Select(a => Mapper.Map<Ator, AtorGetDTO>(a))];
	}

	public Ator NovoAtor(AtorPostDTO atorDto)
	{
		var existe = _masterContext
			.Ator.AsEnumerable()
			.Where(filmeBd =>
				filmeBd.Nome.Equals(atorDto.Nome, StringComparison.OrdinalIgnoreCase)
				&& filmeBd.DataNasc == atorDto.DataNasc
			)
			.Any();

		if (existe)
			throw new AlreadyExistsException(
				"Um Ator com nome e data de Nascimento iguais aos fornecidos já existe."
			);

		var ator = Mapper.Map<AtorPostDTO, Ator>(atorDto);

		_masterContext.Ator.Add(ator);

		_masterContext.SaveChanges();

		return ator;
	}

	public AtorGetDTO UmAtor(int Id)
	{
		var ator = _masterContext.Ator.Where(a => a.Id == Id).FirstOrDefault();

		return ator is null
			? throw new EntityNotFoundException($"Uma entidade Ator de Id {Id} não existe.")
			: Mapper.Map<Ator, AtorGetDTO>(ator);
	}

	public void DeletarAtor(int Id)
	{
		var ator =
			_masterContext.Ator.FirstOrDefault(a => a.Id == Id)
			?? throw new EntityNotFoundException($"Uma entidade Ator de id {Id} não existe.");

		_masterContext.Ator.Remove(_masterContext.Ator.First(a => a.Id == Id));
		_masterContext.SaveChanges();
	}

	public Ator GetExistenteOuCriar(AtorPostDTO dto)
	{
		var ator = SingleByNomeAndDataNasc(dto.Nome, dto.DataNasc);

		ator ??= CriarAtorSemVerificar(dto); // Se for nulo, cria um novo

		return ator;
	}

	private Ator? SingleByNomeAndDataNasc(string nome, DateOnly dataNasc)
	{
		return _masterContext
			.Ator.AsEnumerable()
			.FirstOrDefault(a =>
				a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)
				&& a.DataNasc.Equals(dataNasc)
			);
	}

	private Ator CriarAtorSemVerificar(AtorPostDTO dto)
	{
		var novoAtor = Mapper.Map<AtorPostDTO, Ator>(dto);

		_masterContext.Ator.Add(novoAtor);
		_masterContext.SaveChanges();

		return novoAtor;
	}
}
