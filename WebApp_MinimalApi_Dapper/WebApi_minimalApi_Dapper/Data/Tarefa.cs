using Dapper.Contrib.Extensions;

namespace WebApi_minimalApi_Dapper.Data;

// [Table("Tarefas")]
// public record Tarefa(int  TarefaId, string Atividade, bool IsComplete);
// O recurso positional records faz com que ao criar o objeto não seja necessário declarar a propriedade que ele se refere,
// assim os valores são passados por referência a posição

// Foi necessário alterar o código acima para funcionamento dos endpoints. O using de Dapper.Contrib.Extensions ao 
// contrário de System.ComponentModel.DataAnnotations.Schema se fez necessário para as classes Table e ExplicitKey
    [Table("Tarefas")]
    public record Tarefa
    {
        [ExplicitKey]
        public int TarefaId { get; init; }
        public string? Atividade { get; init; }
        public bool IsComplete { get; init; }
    }