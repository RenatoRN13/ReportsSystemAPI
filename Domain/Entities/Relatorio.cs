using System;

namespace ReportsSystemApi.Domain.Entities {
    public class Relatorio {
        public int id { get; set; }
        public int nota { get; set; }
        public DateTime dataEnvio { get; set; }
        public Usuario usuario { get; set; }
        public Atividade atividade { get; set; }
    }
}