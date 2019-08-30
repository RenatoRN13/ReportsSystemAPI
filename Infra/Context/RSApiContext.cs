using app.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context {
    public class RSApiContext : DbContext
    {
        public RSApiContext(DbContextOptions<RSApiContext> options) : base(options)
        {
        }

        public DbSet<Atividade> Atividades { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Relatorio> Relatorio { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Vinculo> Vinculo { get; set; }
        public DbSet<Log> Log { get; set; }
        public DbSet<UsuarioAtividade> UsuarioAtividade { get; set; }
        public DbSet<UsuarioPerfil> UsuarioPerfil { get; set; }

    //     protected override void OnModelCreating(ModelBuilder modelBuilder)
    //     {
    //         modelBuilder.Entity<Atividade>(entity =>
    //         {
    //             entity.ToTable("atividade");
    //             entity.HasKey(r => r.id);
    //             entity.Property(r => r.descricao);
    //             entity.Property(r => r.dataAtividade);
    //             entity.Property(r => r.dataCadastro);
    //         });

    //         modelBuilder.Entity<Relatorio>(entity =>
    //         {
    //             entity.ToTable("relatorio");
    //             entity.HasKey(r => r.id);
    //             entity.Property(r => r.descricao);
    //             entity.Property(r => r.dataInicio);
    //             entity.Property(r => r.dataFim);
    //             entity.HasMany(r => r.atividade)
    //         });
    //     }
    }
}