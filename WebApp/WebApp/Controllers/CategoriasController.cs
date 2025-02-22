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
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController (AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
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

        // GET api/<ValuesController>/5
        [HttpGet("{id:int}", Name = "ObterCategoriaId")] 
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);
                if (categoria is null) 
                {
                    return NotFound("Não foi possível encontrar a categoria com o Id indicado...");
                }
                return categoria;
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro interno desconhecido");
            }
        }

        [HttpGet("CategoriasProdutos")] 
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProduto()
        {
            return _context.Categorias.AsNoTracking().Include(c => c.Produtos).Where(c => c.CategoriaId <= 10).ToList();
        }

        // POST api/<ValuesController>
        [HttpPost]
        public ActionResult Post(Categoria categoria) 
        {
            if (categoria is null)
                return BadRequest();

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterCategoriaId", new {id = categoria.CategoriaId}, categoria);
            // O método CreatedAtRouteResult cria uma resposta 201 created utilizando o valor informado com base em uma rota e um valor de referência. 
            // No caso, estamos utilizando a rota do método get por id para pesquisar se o produto existe no banco e assim retornar a mensagem de sucesso
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id:int}")] 
        public ActionResult Put(int id, Categoria categoria) // A solicitação recebe o campo id e o campo produto
        {
            // Caso o id indicado e o valor produtoId de Produto sejam diferentes, precisamos informar uma mensagem de erro
            if(id != categoria.CategoriaId)
                return BadRequest();
            

            _context.Entry(categoria).State = EntityState.Modified; 

            return Ok(categoria);

        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);

            if(categoria is null)
                return NotFound($"Categoria de id {id} Não pode ser encontrada");

            // método do curso 
            _context.Categorias.Remove(categoria);
            
            // método próprio, utilizando dt_registro deletado
            // produto.DataDeletado = DateTime.Now;
            
            _context.SaveChanges();

            return Ok(categoria);

        }
    }
}
