using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkShopI2.Migrations
{
    /// <inheritdoc />
    public partial class addMesure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Mesures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Humidite = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateHeure = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ParkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mesures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mesures_Parks_ParkId",
                        column: x => x.ParkId,
                        principalTable: "Parks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mesures_ParkId",
                table: "Mesures",
                column: "ParkId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mesures");
        }
    }
}
