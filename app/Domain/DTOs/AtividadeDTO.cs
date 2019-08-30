using System;

namespace app.Domain.DTOs {
    public class AtividadeDTO {
        public int id { get; set; }
        public string descricao { get; set; }
        public DateTime dataAtividade { get; set; }
        public DateTime dataCadastro { get; set; }
    }
}