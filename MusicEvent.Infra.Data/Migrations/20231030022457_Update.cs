using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MusicEvent.Infra.Data.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RedefinirSenha",
                table: "Usuario");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RedefinirSenha",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
