using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMusicStore.Migrations
{
    public partial class UsuarioTrocandoSenhaPorPassword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Tab_Usuario",
                newName: "Password");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Tab_Usuario",
                newName: "Senha");
        }
    }
}
