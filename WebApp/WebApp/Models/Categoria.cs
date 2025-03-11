using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Categoria
    {
        public Categoria() 
        { 
            Produtos = new Collection<Produto>();
        }
        // Boa Prática; declarar uma classe ao usar coleções
        
        [Key]
        public int CategoriaId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [MaxLength(300)]
        public string? ImageUrl { get; set; }


        // public DateTime? DataDeletado { get; set; } = null;

        /* 
         * StringLength e MaxLength realizam funções semelhantes, porém enquanto MaxLength determina apenas o valor máximo servindo de base para cálculo de armazenamento no banco,
         * StringLength permite informar também o valor mínimo, assim funcionando para validação de campo
        */

        public ICollection<Produto> Produtos { get; set; }
        // Ao definir uma coleção de produtos dentro de uma categoria, indicamos uma relação de um para muitos entre as entidades produtos e categorias
    }
}
