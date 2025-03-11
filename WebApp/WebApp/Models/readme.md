# Domain Models #

&xrArr; Pasta para armazenar os arquivos relativos à domain models. 

&xrArr; Cada arquivo é relativo a uma entidade. 

&xrArr; Aqui são definidos os tipos e os getters e setters das entidades. 

## Recursos utilizados ##

### Identificadores e relacionamentos ###

&xrArr; Ao definir o nome de uma propriedade como `Id` ou `<entidade>ID`, o Entity Framework identifica essa entidade como identificador ou chave primaria. Ao mapear a entidade para o banco, sera enviada como PK.

&xrArr; Para indicar o relacionamento um para muitos entre categorias e produtos, a classe Categoria é criada com uma coleção Produtos, com tipo Produto e a classe Produto possui uma propriedade CategoriaId tipo int e uma propriedade Categoria tipo Categoria. 

```csharp
public class Produto
{
    ...
    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }
    ...
}
```
```csharp
public class Categoria
{
    ...
    public ICollection<Produto> Produtos { get; set; }
    ...
}
```
> As referências entre as classes (propriedade Categoria em Produto e coleção de Produto em Categoria) são ignoradas pelo banco de dados por referenciarem outras classes da aplicação e assim seus tipos não serem convencionais, não podendo ser migrados para o banco. A relação é indicada no banco com a propriedade CategoriaId da classe produto. 

### Data Annotations ###

&xrArr; Por padrão, o Entity Framework Core cria as propriedades com os dados ocupando o maior tamanho possível e isso é refletido ao gerar os elementos do banco de dados. No caso de um parâmetro tipo string em uma classe, no banco de dados MySql será criado uma coluna com o tipo longtext. 

&xrArr; A necessidade de cada caso deve ser bem avaliada, pois a forma padrão do EF ocupa muito espaço de armazenamento. 

&xrArr; A plataforma dotnet possui um recurso chamado Data Annotations, que permite sobrescrever as convenções do EF Core. Desta forma, é possível reduzir o tamanho das propriedades e consequentemente o tamanho. 

&xrArr; Outras tarefas que podem ser realizadas com Data Annotations são:

* Definir regras de validação para o modelo
* Definir como os dados devem ser exibidos na interface
* Especificar o relacionamento entre as entidades do modelo
* Sobrescrever as convenções padrão do EF Core

&xrArr; Para uso desse recurso devem ser incluídos os pacotes: 

* System.ComponentModel.DataAnnotations
* System.ComponentModel.DataAnnotations.Schema

&xrArr; No exemplo abaixo vemos exemplo do uso deste recurso na classe Produto. 

```csharp
[Table("Produtos")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }

    [Required]
    [StringLength(100)]
    public string? Nome { get; set; }

    ...

    [Required]
    [Column(TypeName = "decimal(10,2)")]
    public decimal Preco { get; set; }

    ... 

    [JsonIgnore] 
    public Categoria? Categoria { get; set; }
}
```
>O uso de `[Table()]` nesta aplicação não é necessário pois o mapeamento da classe com a tabela do banco já foi realizado na classe de contexto. 
><br>
>Também não é necessário o uso de `[Key]`, pois a propriedade já esta identificada como Id. 
><br>
>A anotação `[JsonIgnore]` atua nas saídas e entradas em JSON, removendo a propriedade do JSON. Nesse caso se faz necessário pois a biblioteca `System.Text.Json` serializa todas as propriedades públicas por padrão. Desta forma, ao utilizar os métodos HTTP, a propriedade é exibida no JSON. Assim, ao realizar um PUT ou um POST, por padrão, o corpo da solicitação apresenta estes parâmetros. `[JsonIgnore]` faz com que a propriedade não seja convertida para o JSON.

### Serialização ### 

&xrArr; Ao trabalhar com o relacionamento entre entidades, a serialização e a desserialização devem ser bem avaliadas. Estes processos impedem que, ao chamar um método HTTP, a aplicação solicite ou retorne entidades em série infinitamente. 

&xrArr; Como o relacionamento um para muitos entre categorias e produtos é indicado em Produto pelas propriedades CategoriaId e Categoria e pela coleção Produtos em Categoria, é necessário que seja quebrada na aplicação a relação em série (um Produto busca uma Categoria que busca uma coleção de produtos e assim infinitamente). 

&xrArr; Para impedir a serialização, é utilizada em Program.cs uma função para interromper ciclos na serialização JSON.   

```csharp
using System.Text.Json.Serialization;

...

builder.Services.AddControllers().AddJsonOptions(options => 
                                {
                                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                                });

...
```






