using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteAplicacao.Migrations
{
    /// <inheritdoc />
    public partial class mssqlonprem_migration_521 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    usuario_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    senha_hash = table.Column<byte[]>(type: "varbinary(64)", nullable: false),
                    senha_salt = table.Column<byte[]>(type: "varbinary(64)", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "conta",
                columns: table => new
                {
                    conta_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false),
                    numero = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    banco = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ativa = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    data_criacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    saldo_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    saldo_disponivel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    saldo_pendente = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conta", x => x.conta_id);
                    table.ForeignKey(
                        name: "FK_conta_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "usuario_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "produto",
                columns: table => new
                {
                    produto_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ativo = table.Column<bool>(type: "bit", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    data_compra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    datahora_insercao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    raridade = table.Column<long>(type: "bigint", nullable: false),
                    usuario_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produto", x => x.produto_id);
                    table.ForeignKey(
                        name: "FK_produto_usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "usuario",
                        principalColumn: "usuario_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_conta_usuario_id",
                table: "conta",
                column: "usuario_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_produto_usuario_id",
                table: "produto",
                column: "usuario_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "conta");

            migrationBuilder.DropTable(
                name: "produto");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
