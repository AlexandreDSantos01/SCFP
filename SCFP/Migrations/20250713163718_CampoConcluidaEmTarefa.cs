using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCFP.Migrations
{
    /// <inheritdoc />
    public partial class CampoConcluidaEmTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Concluida",
                table: "TarefasDiarias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concluida",
                table: "TarefasDiarias");
        }
    }
}
