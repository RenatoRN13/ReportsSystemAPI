namespace ReportsSystemApi.Domain.Entities {
    public class UsuarioPerfil {
        public int id { get; set; }
        public Usuario usuario { get; set; }
        public Perfil perfil { get; set; }
    }
}