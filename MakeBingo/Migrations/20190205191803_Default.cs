using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MakeBingo.Migrations
{
    public partial class Default : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Maker = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CellsStr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Boards_Boards_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BoardId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CellsStr = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Results_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Boards_ParentId",
                table: "Boards",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Results_BoardId",
                table: "Results",
                column: "BoardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Boards");
        }
    }
}
