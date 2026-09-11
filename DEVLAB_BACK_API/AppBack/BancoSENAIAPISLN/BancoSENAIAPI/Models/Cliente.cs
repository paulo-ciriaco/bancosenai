namespace BancoSENAIAPI.Models
{
    public class Cliente
    {
        public int CodigoCliente { get; set; }

        public string NomeCliente { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public int NumeroAgencia { get; set; } = 10;

        public decimal SaldoTotal { get; set; } = 0;

        public DateTime DataNascimento { get; set; }

        public string Sexo { get; set; } = string.Empty;

        public string Endereco { get; set; } = string.Empty;

        public string Cidade { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;
    }
}