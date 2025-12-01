using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pizzeria_Toscana.Migrations
{
    /// <inheritdoc />
    public partial class ingredientLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Ingredient_Produse_COD_Produs",
                table: "Produs_Ingredient");

            migrationBuilder.AlterColumn<string>(
                name: "Cantitate_Ingredient",
                table: "Produs_Ingredient",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Ingredient_Produse_COD_Produs",
                table: "Produs_Ingredient",
                column: "COD_Produs",
                principalTable: "Produse",
                principalColumn: "COD_Produs",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produs_Ingredient_Produse_COD_Produs",
                table: "Produs_Ingredient");

            migrationBuilder.AlterColumn<int>(
                name: "Cantitate_Ingredient",
                table: "Produs_Ingredient",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(16)",
                oldMaxLength: 16,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Produs_Ingredient_Produse_COD_Produs",
                table: "Produs_Ingredient",
                column: "COD_Produs",
                principalTable: "Produse",
                principalColumn: "COD_Produs");
        }
    }
}
