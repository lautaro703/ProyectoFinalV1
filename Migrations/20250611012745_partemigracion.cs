using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class partemigracion : Migration
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

            migrationBuilder.RenameColumn(
                name: "Monto",
                table: "Transacciones",
                newName: "monto");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "Transacciones",
                newName: "fechaHora");

            migrationBuilder.RenameColumn(
                name: "CryptoCode",
                table: "Transacciones",
                newName: "cryptoCode");

            migrationBuilder.RenameColumn(
                name: "Accion",
                table: "Transacciones",
                newName: "accion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "monto",
                table: "Transacciones",
                newName: "Monto");

            migrationBuilder.RenameColumn(
                name: "fechaHora",
                table: "Transacciones",
                newName: "FechaHora");

            migrationBuilder.RenameColumn(
                name: "cryptoCode",
                table: "Transacciones",
                newName: "CryptoCode");

            migrationBuilder.RenameColumn(
                name: "accion",
                table: "Transacciones",
                newName: "Accion");

            migrationBuilder.AddColumn<int>(
                name: "ClientesId",
                table: "Transacciones",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_ClientesId",
                table: "Transacciones",
                column: "ClientesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_ClientesId",
                table: "Transacciones",
                column: "ClientesId",
                principalTable: "Clientes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
