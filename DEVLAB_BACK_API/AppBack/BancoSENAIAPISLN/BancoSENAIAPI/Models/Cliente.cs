using System.ComponentModel.DataAnnotations;

namespace BancoSENAIAPI.Models
{
    public class Cliente
    {
        [Key]
        public int CodigoCliente { get; set; }

        public string NomeCliente { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public int NumeroAgencia { get; set; } = 10;

        public decimal SaldoTotal { get; set; } = 0;
    }
}