using Dapper;
using Dapper.Contrib.Extensions;
using WebApi_minimalApi_Dapper.Data;
using static WebApi_minimalApi_Dapper.Data.TarefaContext;

namespace WebApi_minimalApi_Dapper.Endpoints
{
    public static class TarefasEndpoints
    {
        public static void MapTarefasEndpoints(this WebApplication app)
        {
            app.MapGet("/", () => $"Bem Vindo a API Tarefas");

            app.MapGet("/Tarefas", async(GetConnection connectionGetter) => 
            {
                using var con = await connectionGetter();
                var tarefas = con.GetAll<Tarefa>().ToList();

                if(tarefas is null) return Results.NotFound();

                return Results.Ok(tarefas);
            });

            app.MapGet("/TarefasId/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var con = await connectionGetter();
                return con.Get<Tarefa>(id) is Tarefa tarefa ? Results.Ok(tarefa) : Results.NotFound($"Tarefa de id {id} não encontrada...");
            });

            app.MapPost("/Tarefas", async (GetConnection connectionGetter, Tarefa novaTarefa) => 
            {
                using var con = await connectionGetter();
                var id = con.Insert(novaTarefa);

                return Results.Created($"/TarefasId/{id}", novaTarefa);
            });

            app.MapPut("/Tarefas", async (GetConnection connectionGetter, Tarefa novaTarefa) => 
            {
                using var con = await connectionGetter();
                var id = con.Update(novaTarefa);

                return id is true ? Results.Ok($"/TarefasId/{id}") : Results.BadRequest("Verifique a tarefa informada"); 
            });

            app.MapDelete("/Tarefas/{id}", async (GetConnection connectionGetter, int id) =>
            {
                using var con =  await connectionGetter(); 
                var tarefa = con.Get<Tarefa>(id);

                if(tarefa is null) return Results.NotFound($"Tarefa de id {id} não encontrada...");

                con.Delete(tarefa);
                return Results.Ok(tarefa);
            });
        }        
    }
}