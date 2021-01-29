using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMusicStore.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tab_Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Artist", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Album",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlbumArtUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Album", x => x.AlbumId);
                    table.ForeignKey(
                        name: "FK_Tab_Album_Tab_Artist_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Tab_Artist",
                        principalColumn: "ArtistId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tab_Album_Tab_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Tab_Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tab_Album_ArtistId",
                table: "Tab_Album",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_Album_GenreId",
                table: "Tab_Album",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tab_Album");

            migrationBuilder.DropTable(
                name: "Tab_Artist");

            migrationBuilder.DropTable(
                name: "Tab_Genre");
        }
    }
}
