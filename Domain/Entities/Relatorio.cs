using System;

namespace ReportsSystemApi.Domain.Entities {
    public class Relatorio {
        public int id { get; set; }
        public int nota { get; set; }
        public DateTime dataEnvio { get; set; }
        public int idUsuario { get; set; }
        public int idAtividade { get; set; }
    }
}