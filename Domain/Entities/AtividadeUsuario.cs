using System;

namespace ReportsSystemApi.Domain.Entities {
    public class AtividadeUsuario {
        public int id { get; set; }
        public int idAtividade { get; set; }
        public int idUsuario { get; set; }
    }
}