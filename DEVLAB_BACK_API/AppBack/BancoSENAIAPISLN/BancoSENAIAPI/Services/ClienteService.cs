using BancoSENAIAPI.Models;
using BancoSENAIAPI.Repositories;

namespace BancoSENAIAPI.Services
{
    public class ClienteService
    {
        private readonly ClienteRepository _repository;

        public ClienteService()
        {
            _repository = new ClienteRepository();
        }

        public List<Cliente> ListarTodos()
        {
            return _repository.ListarTodos();
        }

        public Cliente Cadastrar(Cliente cliente)
        {
            return _repository.Cadastrar(cliente);
        }
    }
}