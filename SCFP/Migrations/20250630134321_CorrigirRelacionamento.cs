using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SCFP.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Categorias");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Categorias");

            migrationBuilder.RenameColumn(
                name: "AccessFailedCount",
                table: "Transacoes",
                newName: "Tipo");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categorias",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Transacoes",
                newName: "AccessFailedCount");

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Transacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Transacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Transacoes",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Transacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Transacoes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Transacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Categorias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Categorias",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Categorias",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Categorias",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
