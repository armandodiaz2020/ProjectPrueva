using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Practica_API.Migrations
{
    /// <inheritdoc />
    public partial class alimentar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "Amount", "CreatedAt", "DeletedAt", "Description", "Name", "Price", "UpdatedAt" },
                values: new object[] { 1, 50, new DateTime(2023, 9, 19, 13, 33, 39, 363, DateTimeKind.Local).AddTicks(1259), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cerveza de botella", "tecate", 100.0, new DateTime(2023, 9, 19, 13, 33, 39, 363, DateTimeKind.Local).AddTicks(1273) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
