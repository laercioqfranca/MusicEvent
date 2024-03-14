using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Log.Infra.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogHistorico",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsuarioId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EntidadeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TipoLog = table.Column<short>(type: "smallint", nullable: false),
                    NomeEntidade = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogHistorico", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogHistorico");
        }
    }
}
