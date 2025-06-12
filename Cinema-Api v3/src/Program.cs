using Cinema_Api.src.Config;
using Cinema_Api.src.Context;
using Cinema_Api.src.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(config =>
	config.AddPolicy(
		"AllowAll",
		policy => policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
	)
);

// Services
builder.Services.AddScoped<FilmeService>();
builder.Services.AddScoped<GeneroService>();
builder.Services.AddScoped<DiretorService>();
builder.Services.AddScoped<AtorService>();

// Add Controllers to the container.
builder
	.Services.AddControllers()
	.AddJsonOptions(options =>
		options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter())
	);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database
builder.Services.AddDbContext<MasterContext>(options =>
{
	options.UseSqlite("Data Source=database/filmes.db");
});

var app = builder.Build();
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
