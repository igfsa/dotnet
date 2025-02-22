using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var config = builder.Configuration;

// Configurando string de conexao. Usadas variáveis de ambiente locais.

string mySqlConnectionStr = (
    "Server=" + Environment.GetEnvironmentVariable("DB_WEBAPP__SERVER") +
    "; DataBase=" + Environment.GetEnvironmentVariable("DB_WEBAPP__NAME") +
    ";Uid=" + Environment.GetEnvironmentVariable("DB_WEBAPP__USER") +
    ";Pwd=" + Environment.GetEnvironmentVariable("DB_WEBAPP__PASSWORD")
);

builder.Services.AddDbContext<AppDbContext>(options =>
                              options.UseMySql(mySqlConnectionStr,
                                 ServerVersion.AutoDetect(mySqlConnectionStr)));

builder.Services.AddControllers().AddJsonOptions(options => 
                                {
                                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                                    // ReferenceHandler define como o JsonSerialazer lida com referências sobre serialização e desserialização
                                    // IgnoreCycles ignora o objeto quando um ciclo de referência é detectado durante a serialização
                                });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

