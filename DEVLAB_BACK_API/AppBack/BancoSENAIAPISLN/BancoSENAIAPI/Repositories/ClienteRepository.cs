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

        public Cliente? Alterar(int codigo, Cliente clienteAtualizado)
        {
            var cliente = _clientes.FirstOrDefault(c => c.CodigoCliente == codigo);

            if (cliente == null)
                return null;

            cliente.NomeCliente = clienteAtualizado.NomeCliente;
            cliente.CPF = clienteAtualizado.CPF;
            cliente.NumeroAgencia = clienteAtualizado.NumeroAgencia;
            cliente.DataNascimento = clienteAtualizado.DataNascimento;
            cliente.Sexo = clienteAtualizado.Sexo;
            cliente.Endereco = clienteAtualizado.Endereco;
            cliente.Cidade = clienteAtualizado.Cidade;
            cliente.Estado = clienteAtualizado.Estado;

            return cliente;
        }
        public bool Excluir(int codigo)
        {
            var cliente = _clientes.FirstOrDefault(c => c.CodigoCliente == codigo);

            if (cliente == null)
                return false;

            _clientes.Remove(cliente);

            return true;
        }
    }
}