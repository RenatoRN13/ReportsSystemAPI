using System.Collections.Generic;

namespace ReportsSystemApi.Domain.Entities {
    public class Usuario {
        public int id { get; set; }
        public string nome { get; set; }
        public string login { get; set; }
        public string senha { get; set; }
        public int idPerfil { get; set; }
        public Perfil perfil { get; set; }
    }
}