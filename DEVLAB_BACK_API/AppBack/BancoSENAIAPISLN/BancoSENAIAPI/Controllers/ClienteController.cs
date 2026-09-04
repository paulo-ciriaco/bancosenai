using BancoSENAIAPI.Models;
using BancoSENAIAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BancoSENAIAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ClienteService _service;

        public ClienteController()
        {
            _service = new ClienteService();
        }

        [HttpGet]
        public IActionResult ListarTodos()
        {
            return Ok(_service.ListarTodos());
        }

        [HttpPost]
        public IActionResult Cadastrar([FromBody] Cliente cliente)
        {
            return Created("", _service.Cadastrar(cliente));
        }

        [HttpPut("{codigo}")]
        public IActionResult Alterar(int codigo, [FromBody] Cliente cliente)
        {
            var clienteAtualizado = _service.Alterar(codigo, cliente);

            if (clienteAtualizado == null)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{codigo}")]
        public IActionResult Excluir(int codigo)
        {
            var excluido = _service.Excluir(codigo);

            if (!excluido)
                return NotFound();

            return Ok(new { message = "Cliente excluído com sucesso." });
        }



    }
}