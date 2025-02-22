using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Context;
using WebApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

// As operações com DataDeletado e DataAlteracao não estão incluídas no curso e avalia sua implementação por conta própria
// Sugestão: Criar actions para selecionar incluindo todos os registros ou apenas os registros selecionados
//   '-> Avaliar a opção de fazer apenas uma action que recebe um campo de true or false para pesquisar com ou sem os registros deletados

// *** Avaliar ao final do curso se os métodos de post e put foram otimizados 
//   '-> Verificar a inclusão de horários automáticos de criação e edição de registro (incluir dtAlterado)

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet] // A solicitação feita com Get sem parâmetro busca todos os registros
        // ActionResult permite retornar um método action ou o resultado especificado. Actions possuuem este método pois assim é possível retornar 
        // o resultado especificado ou uma método action. No caso, estamos utilizando pois caso o retorno de produtos seja null, queremos mostrar 
        // uma mensagem de NotFound
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _context.Produtos.AsNoTracking().Take(10).ToList();
            // Por padrão o ambiente da WebApi faz as consultas de forma rastreada, mapeando as entidades e armazenando em cache. Este recurso é muito pesado
            // Para consultas que não serão manipulados dados, uma vez que não é necessário visualizar o estado da aplicação, pode-se desativar 
            // o rastreamento através de AsNoTracking
            if (produtos is null) 
            {
                return NotFound();
            }
            return produtos;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}", Name = "ObterProdutoId")] 
        // Ao inserir um parâmetro para a solicitação get, ocorre a busca com o valor passado para a função utilizando o parâmetro indicado
        // Com name indicamos um nome para a rota, permitindo acesso através deste nome 

        public ActionResult<Produto> Get(int id)
        {
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null) 
            {
                return NotFound();
            }
            return produto;
        }


        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult Post(Produto produto) // Aqui o produto é recebido 
        {
            if (produto is null)
                return BadRequest();

            _context.Produtos.Add(produto);
            // Adicionando o produto ao contexto
            _context.SaveChanges();
            // Salvando o contexto com as alterações

            return new CreatedAtRouteResult("ObterProdutoId", new {id = produto.ProdutoId}, produto);
            // O método CreatedAtRouteResult cria uma resposta 201 created utilizando o valor informado com base em uma rota e um valor de referência. 
            // No caso, estamos utilizando a rota do método get por id para pesquisar se o produto existe no banco e assim retornar a mensagem de sucesso
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id:int}")] // Ao utilizarmos put com um parâmetro o valor informado no parâmetro é utilizado para identificar no modelo, através do 
                              // objeto que possua o mesmo valor no mesmo parâmetro, o objeto que a solicitação ira interagir  
        public ActionResult Put(int id, Produto produto) // A solicitação recebe o campo id e o campo produto
        {
            // Caso o id indicado e o valor produtoId de Produto sejam diferentes, precisamos informar uma mensagem de erro
            if(id != produto.ProdutoId)
                return BadRequest();
            

            _context.Entry(produto).State = EntityState.Modified; 
            // Ao inidicar o estado de uma entidade como modificado, a aplicação entende que será necessário persistir os dados 
            _context.SaveChanges();

            return Ok(produto);

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.FirstOrDefault(p => p.ProdutoId == id);

            if(produto is null)
                return NotFound();

            // método do curso 
            _context.Produtos.Remove(produto);
            
            // método próprio, utilizando dt_registro deletado
            // produto.DataDeletado = DateTime.Now;
            
            _context.SaveChanges();

            return Ok(produto);

        }
    }
}
