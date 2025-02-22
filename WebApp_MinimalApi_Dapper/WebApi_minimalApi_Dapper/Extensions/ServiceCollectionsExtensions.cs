// Configura o serviço de conexão, a classe será utilizada em program.cs para registrar o serviço de conexão na aplicação

using MySqlConnector;
using static WebApi_minimalApi_Dapper.Data.TarefaContext;

namespace WebApi_minimalApi_Dapper.Extensions
{
    public static class ServiceCollectionsExtensions
    {
        public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
        {
            string mySqlConnectionStr = (
                "Server=" + Environment.GetEnvironmentVariable("DB_WEBAPP__SERVER") +
                "; DataBase=" + Environment.GetEnvironmentVariable("DB_WEBAPP_DAPPER__NAME") +
                ";Uid=" + Environment.GetEnvironmentVariable("DB_WEBAPP__USER") +
                ";Pwd=" + Environment.GetEnvironmentVariable("DB_WEBAPP__PASSWORD")
            );

            builder.Services.AddScoped<GetConnection>(sp =>
                async () => 
                {
                    var connection = new MySqlConnection(mySqlConnectionStr);

                    await connection.OpenAsync();
                    return connection;
                });
            return builder;
        }
    }
}