using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoFinal.Migrations
{
    /// <inheritdoc />
    public partial class nuevamigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Criptomonedas_CriptomonedaCodigo",
                table: "Transacciones");

            migrationBuilder.DropTable(
                name: "Criptomonedas");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_CriptomonedaCodigo",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "CriptomonedaCodigo",
                table: "Transacciones");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CriptomonedaCodigo",
                table: "Transacciones",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Criptomonedas",
                columns: table => new
                {
                    Codigo = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Criptomonedas", x => x.Codigo);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_CriptomonedaCodigo",
                table: "Transacciones",
                column: "CriptomonedaCodigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Criptomonedas_CriptomonedaCodigo",
                table: "Transacciones",
                column: "CriptomonedaCodigo",
                principalTable: "Criptomonedas",
                principalColumn: "Codigo");
        }
    }
}
