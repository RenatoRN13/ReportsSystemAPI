namespace Domain.Entities {
    public class Atividade {
        public int id { get; set; }
        public int descricao { get; set; }
        public int dataInicio { get; set; }
        public int dataFim { get; set; }
        public int idUsuario { get; set; }
    }
}