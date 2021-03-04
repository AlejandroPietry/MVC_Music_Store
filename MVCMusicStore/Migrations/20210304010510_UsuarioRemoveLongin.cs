using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMusicStore.Migrations
{
    public partial class UsuarioRemoveLongin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Tab_Usuario",
                newName: "Senha");

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "Tab_Usuario",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Login",
                table: "Tab_Usuario");

            migrationBuilder.RenameColumn(
                name: "Senha",
                table: "Tab_Usuario",
                newName: "Password");
        }
    }
}
