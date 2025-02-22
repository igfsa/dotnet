using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebApp.Models
{
    public class Produto
    {
        [Key]
        public int ProdutoId { get; set; }

        [Required]
        [StringLength(100)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(300)]
        public string? Descricao { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Preco { get; set; }

        [Required]
        [StringLength(300)]
        public string? ImagemUrl { get; set; }

        [Required]
        public float Estoque { get; set; }

        public DateTime DataCadastro { get; set; }


        public int CategoriaId { get; set; }

        [JsonIgnore] 
        public Categoria? Categoria { get; set; }
        // [JsonIgnore] permite ignorar propriedades na serialização e desserialização. 
        // Pode possuuir 4 condições 
        // [JsonIgnore(Condition = JsonIgnoreCondition.Always)] -> padrão, sempre ignora
        // [JsonIgnore(Condition = JsonIgnoreCondition.Never)] -> sempre serializa e desserializa a propriedade
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)] -> Será ignorada na serialziação se for um tipo de referência null, 
        // um tipo de valor nullable com null ou um tipo de valor padrão
        // [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] -> Será ignorada na serialziação se for um tipo de referência null ou 
        // um tipo de valor que pode ser anulado com valor null
        // Para ignorar todas as propriedades a configuração pode ser feita na classe program
        // Builder.Services.AddControllers()
        //    .AddJsonOptions(options => options.JsonSerializerOptions
        //        .DefaultIgnoreCondition = JsonIgnoreCondition.<condição>);

        // public DateTime? DataDeletado { get; set; } = null;

    }
}
