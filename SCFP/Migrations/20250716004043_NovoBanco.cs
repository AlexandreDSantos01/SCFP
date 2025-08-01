using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCFP.Migrations
{
    /// <inheritdoc />
    public partial class NovoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "TarefasDiarias");

            migrationBuilder.DropColumn(
                name: "Principal",
                table: "TarefasDiarias");

            migrationBuilder.AddColumn<int>(
                name: "TarefaId",
                table: "TarefasDiarias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tarefas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Principal = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tarefas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TarefasDiarias_TarefaId",
                table: "TarefasDiarias",
                column: "TarefaId");

            migrationBuilder.AddForeignKey(
                name: "FK_TarefasDiarias_Tarefas_TarefaId",
                table: "TarefasDiarias",
                column: "TarefaId",
                principalTable: "Tarefas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TarefasDiarias_Tarefas_TarefaId",
                table: "TarefasDiarias");

            migrationBuilder.DropTable(
                name: "Tarefas");

            migrationBuilder.DropIndex(
                name: "IX_TarefasDiarias_TarefaId",
                table: "TarefasDiarias");

            migrationBuilder.DropColumn(
                name: "TarefaId",
                table: "TarefasDiarias");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "TarefasDiarias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Principal",
                table: "TarefasDiarias",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
