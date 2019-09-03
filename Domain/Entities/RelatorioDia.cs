using System;

namespace ReportsSystemApi.Domain.Entities {
    public class RelatorioDia : Relatorio {
        public string descricao { get; set; }
        public DateTime dataAtividade { get; set; }
        public DateTime dataCadastro { get; set; }
     
    }
}