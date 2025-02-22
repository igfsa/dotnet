// Esta classe cria o contexto de conexão para o Dapper

using System.Data;

namespace WebApi_minimalApi_Dapper.Data
{
    public class TarefaContext
    {
        public delegate Task<IDbConnection> GetConnection();
    }
}