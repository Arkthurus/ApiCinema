using AutoMapper;
using Cinema_Api.src.Models;
using Cinema_Api.src.Models.DTOs;
using Cinema_Api.src.Models.DTOs.Get;
using Cinema_Api.src.Models.DTOs.Post;

namespace Cinema_Api.src.Config.Mapper;

public class AutoMapperConfig
{
	public static void Configurar(IMapperConfigurationExpression cfg)
	{
		// Diretor
		cfg.CreateMap<Diretor, DiretorGetDTO>();
		cfg.CreateMap<DiretorGetDTO, Diretor>();
		cfg.CreateMap<DiretorPostDTO, Diretor>();

		// Ator
		cfg.CreateMap<Ator, AtorGetDTO>();
		cfg.CreateMap<AtorGetDTO, Ator>();
		cfg.CreateMap<AtorPostDTO, Ator>();

		// Genero
		cfg.CreateMap<Genero, GeneroGetDTO>();
		cfg.CreateMap<GeneroPostDTO, Genero>();

		// Filme
		cfg.CreateMap<FilmePostDTO, Filme>();
		ConfigurarFilmeParaDTO(cfg);
	}

	private static void ConfigurarFilmeParaDTO(IMapperConfigurationExpression cfg)
	{
		cfg.CreateMap<Filme, FilmeGetDTO>()
			// Transforma entidades Generos em uma lista de strings
			.ForMember(
				dest => dest.Generos,
				opt => opt.MapFrom(src => src.FilmesGeneros.Select(fg => fg.Genero.Nome).ToList())
			)
			// Transforma FilmesAtores em Atores
			.ForMember(
				dest => dest.Atores,
				opt =>
					opt.MapFrom(src =>
						src.FilmesAtores.Select(fa => new AtorPapel
							{
								Ator = new(fa.Ator.Nome, fa.Ator.DataNasc),
								Papel = fa.Papel,
							})
							.ToList()
					)
			);
	}
}
