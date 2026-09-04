using BancoSENAIAPI.Models;

namespace BancoSENAIAPI.Repositories
{
    public class ClienteRepository
    {
        private static List<Cliente> _clientes = new List<Cliente>();

        public List<Cliente> ListarTodos()
        {
            return _clientes;
        }

        public Cliente Cadastrar(Cliente cliente)
        {
            cliente.CodigoCliente = _clientes.Count + 1;

            _clientes.Add(cliente);

            return cliente;
        }
    }
}