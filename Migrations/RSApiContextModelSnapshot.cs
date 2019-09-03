﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ReportsSystemApi.Infra;

namespace ReportsSystemAPI.Migrations
{
    [DbContext(typeof(RSApiContext))]
    partial class RSApiContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.Atividade", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("dataFim");

                    b.Property<DateTime>("dataInicio");

                    b.Property<string>("descricao");

                    b.HasKey("id");

                    b.ToTable("Atividades");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.AtividadeUsuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("atividadeid");

                    b.Property<int?>("usuarioid");

                    b.HasKey("id");

                    b.HasIndex("atividadeid");

                    b.HasIndex("usuarioid");

                    b.ToTable("AtividadeUsuarios");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.Log", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("acao");

                    b.Property<DateTime>("data");

                    b.Property<string>("paginaAcessada");

                    b.Property<int?>("usuarioid");

                    b.HasKey("id");

                    b.HasIndex("usuarioid");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.Perfil", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("descricao");

                    b.HasKey("id");

                    b.ToTable("Perfis");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.Relatorio", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("atividadeid");

                    b.Property<DateTime>("dataEnvio");

                    b.Property<int>("nota");

                    b.Property<int?>("usuarioid");

                    b.HasKey("id");

                    b.HasIndex("atividadeid");

                    b.HasIndex("usuarioid");

                    b.ToTable("Relatorios");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("login");

                    b.Property<string>("nome");

                    b.Property<int?>("perfilid");

                    b.Property<string>("senha");

                    b.HasKey("id");

                    b.HasIndex("perfilid");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.AtividadeUsuario", b =>
                {
                    b.HasOne("ReportsSystemApi.Domain.Entities.Atividade", "atividade")
                        .WithMany()
                        .HasForeignKey("atividadeid");

                    b.HasOne("ReportsSystemApi.Domain.Entities.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.Log", b =>
                {
                    b.HasOne("ReportsSystemApi.Domain.Entities.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.Relatorio", b =>
                {
                    b.HasOne("ReportsSystemApi.Domain.Entities.Atividade", "atividade")
                        .WithMany()
                        .HasForeignKey("atividadeid");

                    b.HasOne("ReportsSystemApi.Domain.Entities.Usuario", "usuario")
                        .WithMany()
                        .HasForeignKey("usuarioid");
                });

            modelBuilder.Entity("ReportsSystemApi.Domain.Entities.Usuario", b =>
                {
                    b.HasOne("ReportsSystemApi.Domain.Entities.Perfil", "perfil")
                        .WithMany()
                        .HasForeignKey("perfilid");
                });
#pragma warning restore 612, 618
        }
    }
}
