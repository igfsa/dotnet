Para Criar uma Minimal Api via shell deve ser rodado o seguinte comando: 

$ dotnet new web -o WebApi_minimalApi


Pacotes necessários para o funcionamento da Api: 

$ dotnet add package Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore --prerelease
$ dotnet add package Microsoft.EntityFrameworkCore.InMemory --prerelease
$ dotnet add package Microsoft.AspNetCore.OpenApi
$ dotnet add package Swashbuckle.AspNetCore

O pacote Microsoft.EntityFrameworkCore.InMemory é necessário para a execução da aplicação na memória
Os pacotes Microsoft.AspNetCore.OpenApi e Swashbuckle.AspNetCore são necessários para uso da interface Swagger


Script base do arquivo program.cs

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();


