using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ReportsSystemAPI.Migrations
{
    public partial class RSmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Atividades",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    descricao = table.Column<string>(nullable: true),
                    dataInicio = table.Column<DateTime>(nullable: false),
                    dataFim = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atividades", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Perfis",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    descricao = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Perfis", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nome = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    senha = table.Column<string>(nullable: true),
                    perfilid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Perfis_perfilid",
                        column: x => x.perfilid,
                        principalTable: "Perfis",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AtividadeUsuarios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    atividadeid = table.Column<int>(nullable: true),
                    usuarioid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AtividadeUsuarios", x => x.id);
                    table.ForeignKey(
                        name: "FK_AtividadeUsuarios_Atividades_atividadeid",
                        column: x => x.atividadeid,
                        principalTable: "Atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AtividadeUsuarios_Usuarios_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    acao = table.Column<string>(nullable: true),
                    paginaAcessada = table.Column<string>(nullable: true),
                    data = table.Column<DateTime>(nullable: false),
                    usuarioid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Logs_Usuarios_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Relatorios",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    nota = table.Column<int>(nullable: false),
                    dataEnvio = table.Column<DateTime>(nullable: false),
                    usuarioid = table.Column<int>(nullable: true),
                    atividadeid = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relatorios", x => x.id);
                    table.ForeignKey(
                        name: "FK_Relatorios_Atividades_atividadeid",
                        column: x => x.atividadeid,
                        principalTable: "Atividades",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relatorios_Usuarios_usuarioid",
                        column: x => x.usuarioid,
                        principalTable: "Usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeUsuarios_atividadeid",
                table: "AtividadeUsuarios",
                column: "atividadeid");

            migrationBuilder.CreateIndex(
                name: "IX_AtividadeUsuarios_usuarioid",
                table: "AtividadeUsuarios",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Logs_usuarioid",
                table: "Logs",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Relatorios_atividadeid",
                table: "Relatorios",
                column: "atividadeid");

            migrationBuilder.CreateIndex(
                name: "IX_Relatorios_usuarioid",
                table: "Relatorios",
                column: "usuarioid");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_perfilid",
                table: "Usuarios",
                column: "perfilid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AtividadeUsuarios");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "Relatorios");

            migrationBuilder.DropTable(
                name: "Atividades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Perfis");
        }
    }
}
