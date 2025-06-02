using System;

namespace Despesas_23504.Models
{
    public class Despesa
    {
        public int Id { get; set; }
        public required string Nome { get; set; } // Adicionado 'required'
        public decimal Valor { get; set; }
        public DateTime Data { get; set; } = DateTime.Now;

        public string? Categoria { get; set; } // Tornado anulável
        public string? Local { get; set; }    // Tornado anulável
    }
}