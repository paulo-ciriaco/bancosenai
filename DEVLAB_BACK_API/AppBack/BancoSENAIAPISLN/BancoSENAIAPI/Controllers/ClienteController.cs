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



    }
}