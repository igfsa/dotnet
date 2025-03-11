# Controllers # 

&xrArr; Os controladores são responsáveis por processar e responder as solicitações recebidas. Dessa forma, as ações HTTP são inseridas na classe do controller. 

&xrArr; Em uma WebAPI, os controladores são classes derivadas da classe ControllerBase.

```csharp
[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    ...
    
    // GET api/<ValuesController>/5
    [HttpGet("{id:int}", Name = "ObterProdutoId")] 

    ...
}
```
>Acima o exemplo básico para o controller de Produtos. 
><br>
>O atributo `[ApiController]` permite decorar os controladores, habilitando recursos como requisito de roteamento de atributo, respostas HTTP 400 automáticas, inferência de parâmetro de origem de associação, inferência de solicitação de de dados de várias partes/formulário e uso de Problem Details para códigos de status de erro.
> <br>
> O atributo `[Route("[controller]")]` especifica um padrão de URL para acessar o controlador. Desta forma, todo método HTTP passa a ser acessado na mesma URL. O valor `"[controller]"` fornecido busca o nome do controller para usar como parâmetro.

&xrArr; O uso de controllers precisa de adição e mapeamento, feitos em Program.cs:

```csharp
... 

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().

...

var app = builder.Build();

...

app.MapControllers();

app.Run()

```

&xrArr; A relação entre o banco de dados e o mapeamento das entidades ao controller são feitas através do construtor da classe do controller, através de uma ingestão de dependência com a classe AppDbContext, que já possui a relação e o mapeamento com o banco.

```csharp
[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly AppDbContext _context;
    public ProdutosController(AppDbContext context)
    {
        _context = context;
    }
    ...
}
```

## Definindo as funções para cada método HTTP ##

&xrArr; A estrutura básica utilizada para cada função consiste no decorador com o método, o tipo de retorno, o nome da função, a definição das variáveis, a ação e o retorno.

```csharp
    [HttpMetodo]
    public ActionResult<tipo> Nome()
    {
        var item;

        valor = item.ação(); 

        return valor;
    }
```
>É utilizada uma `ActionResult` como tipo de retorno pois caso o processo realizado retorne uma mensagem HTTP, como uma mensagem de erro, tanto o resultado quanto a mensagem de erro podem ser aceitos como retorno. Funções como `NotFound()` ou `BadRequest()` não podem ser usadas caso o tipo de retorno seja apenas o tipo do dado. 

### GET sem parâmetro ###

&xrArr; O uso do método GET sem um valor como parâmetro realiza a busca no banco de dados de todos os valores de determinado elemento, no caso, todos valores de Produtos. 

```csharp
    // GET: api/<ValuesController>
    [HttpGet]
    public ActionResult<IEnumerable<Produto>> Get()
    {
        var produtos = _context.Produtos.AsNoTracking().Take(10).ToList();
        if (produtos is null) 
        {
            return NotFound();
        }
        return produtos;
    }
```
> É utilizado `IEnumerable` por ser uma interface somente leitura, trabalha por demanda e não precisa ter toda a coleção na memória. 
> <br>
>Por padrão o ambiente da WebApi faz as consultas de forma rastreada, mapeando as entidades e armazenando em cache. Este recurso é muito pesado. Para consultas que não serão manipulados dados, uma vez que não é necessário visualizar o estado da aplicação, pode-se desativar o rastreamento através de `AsNoTracking()`.
><br>
>A função `Take()` limita o número de resultados da consulta. É utilizado para evitar sobrecargas na aplicação. 

### GET com parâmetro ###

&xrArr; O uso do método GET com um valor como parâmetro realiza a busca no banco de dados de valor com correspondência para o parâmetro informado. No caso é utilizado o parâmetro id como referência.

```csharp
    // GET api/<ValuesController>/5
    [HttpGet("{id:int}", Name = "ObterProdutoId")] 
    public ActionResult<Produto> Get(int id)
    {
        var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
        if (produto is null) 
        {
            return NotFound();
        }
        return produto;
    }
```
>O parâmetro e seu tipo são informados ao lado do método entre parênteses e aspas. 
> <br>
> Como parâmetro da função, é passado o valor de id.
> <br>
> A função `FirstOrDefault` permite retornar o primeiro registro encontrado e, caso não exista retorno, retorna null. 
> <br>
> Com name indicamos um nome para a rota, permitindo acesso através deste nome. No caso, como existem dois métodos GET para categorias, o uso de nome para um deles se torna adequado para diferenciar. 

### POST ###

&xrArr; O método POST realiza a criação de um novo elemento de acordo com os dados fornecidos como parâmetro e a entidade do contexto que ele está associado. 

```csharp
    // POST api/<ValuesController>
    [HttpPost]
    public ActionResult Post(Produto produto) 
    {
        if (produto is null)
            return BadRequest();

        _context.Produtos.Add(produto);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterProdutoId", new {id = produto.ProdutoId}, produto);
    }
```
>Um objeto do tipo produto, representado em um JSON é solicitado como parâmetro para a função. 
>A função `Add()` adiciona o objeto na memória em cache. A função `SaveChanges()` é utilizada para persistir os dados no banco.  

### PUT ###

&xrArr; O método PUT realiza a atualização de um elemento de acordo com os dados fornecidos como parâmetro e a entidade do contexto que ele está associado. Um id é informado no método. Já na função, um id e os valores a serem atualizados. 

```csharp
    // PUT api/<ValuesController>/5
    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Produto produto)
    {
        if(id != produto.ProdutoId)
            return BadRequest();
        
        _context.Entry(produto).State = EntityState.Modified; 

        _context.SaveChanges();

        return Ok(produto);
    }
```
>Assim como em POST, um objeto do tipo produto, representado em um JSON é solicitado como parâmetro para a função. Aqui também é solicitado o id do objeto já existente. O informado como parâmetro do produto e o informado no método POST devem ser iguais. 
><br>
>`_context.Entry(produto).State = EntityState.Modified;` marca que a informação está sendo modificada na aplicação. Também será necessário persistir os dados no banco com `SaveChanges()`.

### DELETE ###

&xrArr; O método DELETE realiza a exclusão de um elemento de acordo com o valor de id fornecido como parâmetro. 

```csharp
    // DELETE api/<ValuesController>/5
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

        if(produto is null)
            return NotFound();

        _context.Produtos.Remove(produto);
        
        _context.SaveChanges();

        return Ok(produto);

    }
```
>`Remove()` indica na memória que o objeto deverá ser excluído, também sendo necessário persistir os dados posteriormente.

### GET com serialização ###

&xrArr; O método GET pode ser usado com serialização, buscando uma entidade e recursos associados a ela.

```csharp
[HttpGet("CategoriasProdutos")] 
public ActionResult<IEnumerable<Categoria>> GetCategoriasProduto()
{
    return _context.Categorias.AsNoTracking().Include(c => c.Produtos).Where(c => c.CategoriaId <= 10).ToList();
}
```
>>No caso, uma serialização é utilizada para buscar todos produtos de uma categoria, com `Include()` realizando a busca da entidade associada. 
><br>
>`Where()` aplica um filtro na busca, evitando sobrecargas na aplicação. 

## Tratamento de Erros ## 

&xrArr; Um tratamento de erros simples pode ser realizado através de blocos try catch nos métodos HTTP.  


```csharp
[HttpGet]
public ActionResult<IEnumerable<Categoria>> Get()
{
    try
    {
        // throw new Exception();
        var categorias = _context.Categorias.AsNoTracking().Take(10).ToList();
        
        if (categorias is null) 
        {
            return NotFound("Categoria não encontrada");
        }
        return categorias;
    }
    catch(Exception)
    {
        return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um erro ao realizar a busca...");
    }
}
```




