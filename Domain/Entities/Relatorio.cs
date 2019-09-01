using System;

namespace ReportsSystemApi.Domain.Entities {
    public class Relatorio {
        public int id { get; set; }
        public string descricao { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }
        public Usuario usuario { get; set; }
        public Atividade atividade { get; set; }
    }
}