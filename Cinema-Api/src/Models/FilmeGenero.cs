using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Api.src.Models;

[PrimaryKey("FilmeId", "GeneroId")]
public class FilmeGenero
{
	public int FilmeId { get; set; }

	[JsonIgnore]
	public Filme Filme { get; set; } = null!;

	public int GeneroId { get; set; }

	[JsonIgnore]
	public Genero Genero { get; set; } = null!;
}
