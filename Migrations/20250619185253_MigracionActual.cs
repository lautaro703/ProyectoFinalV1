using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class MigracionActual : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_ClientesId",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_ClientesId",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "ClientesId",
                table: "Transacciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClientesId",
                table: "Transacciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_ClientesId",
                table: "Transacciones",
                column: "ClientesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_ClientesId",
                table: "Transacciones",
                column: "ClientesId",
                principalTable: "Clientes",
                principalColumn: "Id");
        }
    }
}
