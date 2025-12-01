using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria_Toscana.Migrations
{
    /// <inheritdoc />
    public partial class Create4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cantitate",
                table: "Comanda_Produs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cantitate",
                table: "Comanda_Produs");
        }
    }
}
