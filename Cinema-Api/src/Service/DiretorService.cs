using AutoMapper;
using Cinema_Api.src.Config.Mapper;
using Cinema_Api.src.Context;
using Cinema_Api.src.Exceptions;
using Cinema_Api.src.Models;
using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.Post;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Api.src.Service;

public class DiretorService(MasterContext masterContext)
{
	private readonly MasterContext _masterContext = masterContext;

	private readonly Mapper Mapper = new(new MapperConfiguration(AutoMapperConfig.Configurar));

	public DiretorGetDTO UmDiretor(int Id)
	{
		var diretor = _masterContext
			.Diretor
			.Where(d => d.Id == Id)
			.FirstOrDefault();

		return diretor is null
			? throw new EntityNotFoundException($"Uma entidade Diretor de Id {Id} não existe.")
			: Mapper.Map<Diretor, DiretorGetDTO>(diretor);
	}

	public Diretor NovoDiretor(DiretorPostDTO diretorDto)
	{
		var existe = _masterContext
			.Diretor.AsEnumerable()
			.Where(diretorBd =>
				diretorBd.Nome.Equals(diretorDto.Nome, StringComparison.OrdinalIgnoreCase)
				&& diretorBd.DataNasc == diretorDto.DataNasc
			)
			.Any();

		if (existe)
			throw new AlreadyExistsException(
				"Um Diretor com nome e data de Nascimento iguais aos fornecidos já existe."
			);

		var diretor = Mapper.Map<DiretorPostDTO, Diretor>(diretorDto);

		_masterContext.Diretor.Add(diretor);

		_masterContext.SaveChanges();

		return diretor;
	}

	public List<DiretorGetDTO> TodosOsDiretores()
	{
		var diretores = _masterContext
			.Diretor.Select(d => Mapper.Map<Diretor, DiretorGetDTO>(d))
			.ToList();

		return diretores;
	}

	public void DeletarDiretor(int Id)
	{
		var diretor =
			_masterContext.Diretor.FirstOrDefault(d => d.Id == Id)
			?? throw new EntityNotFoundException($"Uma entidade Diretor de id {Id} não existe.");

		_masterContext.Diretor.Remove(_masterContext.Diretor.First(d => d.Id == Id));
		_masterContext.SaveChanges();
	}

	public Diretor GetExistenteOuCriar(DiretorPostDTO dto)
	{
		var diretor = SingleByNomeAndDataNasc(dto.Nome, dto.DataNasc);

		diretor ??= CriarDiretorSemVerificar(dto); // Se for nulo, cria um novo

		return diretor;
	}

	public Diretor? SingleByNomeAndDataNasc(string nome, DateOnly dataNasc)
	{
		return _masterContext
			.Diretor.AsEnumerable()
			.FirstOrDefault(d =>
				d.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)
				&& d.DataNasc.Equals(dataNasc)
			);
	}

	#region Métodos Privados

	private Diretor CriarDiretorSemVerificar(DiretorPostDTO diretor)
	{
		var novoDiretor = Mapper.Map<DiretorPostDTO, Diretor>(diretor);

		_masterContext.Diretor.Add(novoDiretor);
		_masterContext.SaveChanges();
		return novoDiretor;
	}

	#endregion
}
