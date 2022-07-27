using Microsoft.AspNetCore.Mvc;

namespace ClientesApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {

        [HttpGet("listar")]
        public async Task<ActionResult<List<Cliente>>> Get()
        {   
            Cliente cliente = new Cliente();

            return Ok(cliente.listarCliente());
        }

        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> GetByName([FromQuery] Cliente parameters)
        {
            Cliente cliente = new Cliente();

            var getClientByName = cliente.listarCliente().Where(
                (p) => string.Equals(p.Nome, parameters.Nome,
                StringComparison.OrdinalIgnoreCase) || string.Equals(p.CPF, parameters.CPF,
                StringComparison.OrdinalIgnoreCase)
                );

            if (getClientByName == null)
                return BadRequest("Cliente não encontrado.");

            return Ok(getClientByName);
        }

        [HttpPost("criar")]
        public async Task<ActionResult<List<Cliente>>> AddClient(Cliente client)
        {
            Cliente _cliente = new Cliente();
            _cliente.Inserir(client);
        
            return Ok(_cliente.listarCliente());
        }

        [HttpPut("atualizar/{id}")]
        public async Task<ActionResult<List<Cliente>>> UpdateClient(Cliente cliente)
        {
            Cliente _cliente = new Cliente();
            var getClientById = _cliente.listarCliente().Find(client => client.Id == cliente.Id);

            if (getClientById == null)
                return BadRequest("Cliente não encontrado.");

            return Ok(_cliente.Atualizar(cliente.Id, cliente));
        }

        [HttpDelete("remover/{id}")]
        public async Task<ActionResult<List<Cliente>>> DeleteClient(int id)
        {
            Cliente _cliente = new Cliente();
            var getClientById = _cliente.listarCliente().Find(cliente => cliente.Id == id);

            if (getClientById == null)
                return BadRequest("Cliente não encontrado.");

            return Ok(_cliente.Deletar(id));
        }
    }
}
