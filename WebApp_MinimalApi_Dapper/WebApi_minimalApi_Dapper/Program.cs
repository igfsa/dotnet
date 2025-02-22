using WebApi_minimalApi_Dapper.Endpoints;
using WebApi_minimalApi_Dapper.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Classe crida para o registro do serviço de conexão
builder.AddPersistence();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapTarefasEndpoints();

app.Run();
