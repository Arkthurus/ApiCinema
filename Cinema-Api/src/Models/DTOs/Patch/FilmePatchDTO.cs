using Cinema_Api.src.Models.DTOs.Get;

namespace Cinema_Api.src.Models.DTOs.HttpPatch;

/// <summary>
///	DTO Utilizada apenas para operações de HTTP Patch.
/// <br/>
/// Sua diferença para a DTO Normal é que todos os seus
/// campos são opcionais para permitir que requisições
/// alterem apenas os campos que desejarem.
/// </summary>
public class FilmePatchDTO
{
	public string? Titulo { get; set; }

	public int? AnoLancamento { get; set; }

	public string? Sinopse { get; set; }

	public float? NotaIMDB { get; set; }

	public List<string>? Generos { get; set; }

	public DiretorGetDTO? Diretor { get; set; }

	/// <summary>
	/// Se verdadeiro, indica que o atributo Diretor é uma
	/// pessoa diferente do Diretor presente no banco de dados.
	/// <br/>
	/// Se falso, indica que o atributo Diretor se refere
	/// ao diretor já existente.
	/// </summary>
	public bool? IsNovoDiretor { get; set; }
}
