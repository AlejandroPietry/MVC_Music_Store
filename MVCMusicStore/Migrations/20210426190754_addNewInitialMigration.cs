using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCMusicStore.Migrations
{
    public partial class addNewInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnlineLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUsuario = table.Column<int>(type: "INTEGER", nullable: false),
                    LastHeartBeat = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnlineLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Artist",
                columns: table => new
                {
                    ArtistId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Artist", x => x.ArtistId);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Genre", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Order",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Adress = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Order", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Usuario",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Login = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Nome = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Usuario", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "Tab_Album",
                columns: table => new
                {
                    AlbumId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    ArtistId = table.Column<int>(type: "INTEGER", nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 160, nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AlbumArtUrl = table.Column<string>(type: "TEXT", maxLength: 1024, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "Tab_Cart",
                columns: table => new
                {
                    RecordId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CartId = table.Column<string>(type: "TEXT", nullable: true),
                    AlbumId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_Cart", x => x.RecordId);
                    table.ForeignKey(
                        name: "FK_Tab_Cart_Tab_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Tab_Album",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tab_OrderDetail",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    AlbumId = table.Column<int>(type: "INTEGER", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    GenreId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tab_OrderDetail", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_Tab_OrderDetail_Tab_Album_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Tab_Album",
                        principalColumn: "AlbumId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tab_OrderDetail_Tab_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Tab_Genre",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tab_OrderDetail_Tab_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Tab_Order",
                        principalColumn: "OrderId",
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

            migrationBuilder.CreateIndex(
                name: "IX_Tab_Cart_AlbumId",
                table: "Tab_Cart",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_OrderDetail_AlbumId",
                table: "Tab_OrderDetail",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_OrderDetail_GenreId",
                table: "Tab_OrderDetail",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Tab_OrderDetail_OrderId",
                table: "Tab_OrderDetail",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnlineLogs");

            migrationBuilder.DropTable(
                name: "Tab_Cart");

            migrationBuilder.DropTable(
                name: "Tab_OrderDetail");

            migrationBuilder.DropTable(
                name: "Tab_Usuario");

            migrationBuilder.DropTable(
                name: "Tab_Album");

            migrationBuilder.DropTable(
                name: "Tab_Order");

            migrationBuilder.DropTable(
                name: "Tab_Artist");

            migrationBuilder.DropTable(
                name: "Tab_Genre");
        }
    }
}
