using System;

namespace Domain.Entities {
    public class Atividade {
        public int id { get; set; }
        public string descricao { get; set; }
        public DateTime dataAtividade { get; set; }
        public DateTime dataCadastro { get; set; }
        public int idUsuario { get; set; }
        public int idRelatorio { get; set; }
    }
}