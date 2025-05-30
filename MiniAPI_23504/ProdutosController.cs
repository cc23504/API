using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProdutosController : ControllerBase
	{
		// GET: api/<ValuesController>
		[HttpGet]
		public IEnumerable<Produto>? Get()
		{

			if ((RepositorioDeProduto.Produtos == null) &&
					(RepositorioDeProduto.Produtos.Count == 0))

				Response.StatusCode = StatusCodes.Status404NotFound;
			else
				Response.StatusCode = StatusCodes.Status200OK;

			return RepositorioDeProduto.PegarTodos();
		}

		// GET api/<ValuesController>/5
		[HttpGet("{codigo}")]
		public Produto? Get(int codigo)
		{
			Produto prod = RepositorioDeProduto.PegarPorCodigo(codigo.ToString());
			/* if (prod == null) {
				 Response.StatusCode = StatusCodes.Status404NotFound;
			 }
			 else
			 {
				 Response.StatusCode = StatusCodes.Status200OK;
			 }
			*/

			Response.StatusCode = (prod == null) ? StatusCodes.Status404NotFound : StatusCodes.Status200OK;

			return prod;

		}

		// POST api/<ValuesController>
		[HttpPost]
		public void Post([FromBody] Produto prod)
		{
			if ((prod == null) || (prod.Codigo == String.Empty))
			{

				Response.StatusCode = StatusCodes.Status400BadRequest;
			}
			else
			{
				RepositorioDeProduto.Adicionar(prod);
				Response.StatusCode = StatusCodes.Status201Created;
			}

		}

		// PUT api/<ValuesController>/5
		[HttpPut("{p}")]
		public void Put([FromBody] Produto p)
		{
			Produto prod = RepositorioDeProduto.PegarPorCodigo(p.Codigo);
			Response.StatusCode = (prod == null) ? StatusCodes.Status404NotFound : StatusCodes.Status202Accepted;
			if (prod != null)
			{
				prod.Nome = p.Nome;
			}
		}

		// DELETE api/<ValuesController>/5
		[HttpDelete("{codgo}")]
		public void Delete(int codigo)
		{
			Produto prod = RepositorioDeProduto.PegarPorCodigo(codigo.ToString());
			Response.StatusCode = (prod == null) ? StatusCodes.Status404NotFound : StatusCodes.Status200OK;
			if (prod != null)
			{
				RepositorioDeProduto.Remover(prod);
			}
		}
	}
}