using System;

namespace ReportsSystemApi.Domain.Entities {
    public class Atividade {
        public int id { get; set; }
        public string descricao { get; set; }
        public DateTime dataInicio { get; set; }
        public DateTime dataFim { get; set; }
        public int idUsuario { get; set; }
        
    }
}