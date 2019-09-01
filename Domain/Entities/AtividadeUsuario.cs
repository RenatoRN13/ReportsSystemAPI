using System;

namespace ReportsSystemApi.Domain.Entities {
    public class AtividadeUsuario {
        public int id { get; set; }
        public Atividade atividade { get; set; }
        public Usuario Usuario { get; set; }
    }
}