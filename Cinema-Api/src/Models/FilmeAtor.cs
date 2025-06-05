using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Api.src.Models;

[PrimaryKey("FilmeId", "AtorId")]
public class FilmeAtor
{
	public required int FilmeId { get; set; }

	public required Filme Filme { get; set; }

	public required int AtorId { get; set; }

	public required Ator Ator { get; set; }

	public required string Papel { get; set; }
}
