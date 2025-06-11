using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteAplicacao.Migrations
{
    /// <inheritdoc />
    public partial class mssqlonprem_migration_529 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    produto_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    data_compra = table.Column<DateTime>(type: "datetime2", nullable: true),
                    datahora_insercao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    raridade = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.produto_id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    usuario_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    senha = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    telefone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    datahora_insercao = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    datahora_desativacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.usuario_id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
