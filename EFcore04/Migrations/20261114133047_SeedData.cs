using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFcore04.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Managers",
                columns: new[] { "Id", "Email", "FullName", "HireDate", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "ahmed@bank.com", "Ahmed Hassan", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01111111111" },
                    { 2, "mona@bank.com", "Mona Ali", new DateTime(2021, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "01222222222" }
                });

            migrationBuilder.InsertData(
                table: "Branches",
                columns: new[] { "code", "Address", "ManagerId", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { "BR001", "Downtown Cairo", 1, "Cairo Main Branch", "01000000001" },
                    { "BR002", "Giza Square", 2, "Giza Branch", "01000000002" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "code",
                keyValue: "BR001");

            migrationBuilder.DeleteData(
                table: "Branches",
                keyColumn: "code",
                keyValue: "BR002");

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Managers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
