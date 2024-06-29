using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrashCollectionAPI.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRegistro",
                table: "Coleta",
                type: "TIMESTAMP(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataColeta",
                table: "Coleta",
                type: "TIMESTAMP(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataColeta",
                table: "Coleta");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataRegistro",
                table: "Coleta",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "TIMESTAMP(7)");
        }
    }
}
