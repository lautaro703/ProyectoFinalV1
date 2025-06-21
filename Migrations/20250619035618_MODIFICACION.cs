using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class MODIFICACION : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones");
        }
    }
}
