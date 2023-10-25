using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkShopI2.Migrations
{
    /// <inheritdoc />
    public partial class fixVille : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Villes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Villes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Villes");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Villes");
        }
    }
}
