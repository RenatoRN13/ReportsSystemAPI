using Microsoft.EntityFrameworkCore;
using ReportsSystemApi.Domain.Entities;

namespace ReportsSystemApi.Infra {
    public class RSApiContext : DbContext{
        public RSApiContext(DbContextOptions<RSApiContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<AtividadeUsuario> AtividadeUsuarios { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}