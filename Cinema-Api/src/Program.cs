using Cinema_Api.src.Config;
using Cinema_Api.src.Context;
using Cinema_Api.src.Routes;
using Cinema_Api.src.Service;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options => {
	options.AddPolicy("AllowAll", builder =>
	{
		builder
		.AllowAnyHeader()
		.AllowAnyOrigin()
		.AllowAnyMethod();
	});
});

// Services
builder.Services.AddScoped<FilmeService>();
builder.Services.AddScoped<GeneroService>();
builder.Services.AddScoped<DiretorService>();
builder.Services.AddScoped<AtorService>();

// Add jsonConversion
builder.Services.Configure<JsonOptions>(options =>
{
	options.SerializerOptions.Converters.Add(new DateOnlyJsonConverter());
});

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

ROTA_GET.MapGetRoutes(app);

ROTA_POST.MapPostRoutes(app);

ROTA_DELETE.MapDeleteRoutes(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.Run();
