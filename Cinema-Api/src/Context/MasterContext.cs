using Cinema_Api.src.Models;
using Microsoft.EntityFrameworkCore;

namespace Cinema_Api.src.Context;

public class MasterContext : DbContext
{
	public DbSet<Filme> Filme { get; set; }

	// public DbSet<Avaliacao> Avaliacao { get; set;}

	public DbSet<Ator> Ator { get; set; }

	public DbSet<Diretor> Diretor { get; set; }

	public DbSet<Genero> Genero { get; set; }

	public DbSet<FilmeGenero> FilmeGenero { get; set; }

	public DbSet<FilmeAtor> FilmeAtor { get; set; }

	public MasterContext(DbContextOptions options)
		: base(options) { }

	protected MasterContext() { }
}
