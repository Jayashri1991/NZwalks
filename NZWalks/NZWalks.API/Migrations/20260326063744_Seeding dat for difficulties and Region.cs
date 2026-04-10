using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingdatfordifficultiesandRegion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { new Guid("574c0dcc-d832-4617-b096-db152effd90f"), "Easy" },
                    { new Guid("628dd14a-a0aa-4283-baf0-a9b4e8b0a16d"), "Medium" },
                    { new Guid("c19aed72-0048-4740-9f04-14f46338b1bf"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "ID", "Code", "Name", "regionImageUrl" },
                values: new object[,]
                {
                    { new Guid("642ee894-7bb4-4950-a429-a837ed9d5386"), "IN-GA", "Goa", null },
                    { new Guid("7ee26529-e3d8-40e8-9263-9fbbef958aef"), "IN-AP", "Andhra Pradesh", null },
                    { new Guid("8a58170e-d4ed-4c19-b24e-e5c341be474f"), "IN-OD", "Odisha", null },
                    { new Guid("df363e96-9143-4939-b463-67d5a3f40984"), "IN-BR", "Bihār", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "ID",
                keyValue: new Guid("574c0dcc-d832-4617-b096-db152effd90f"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "ID",
                keyValue: new Guid("628dd14a-a0aa-4283-baf0-a9b4e8b0a16d"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "ID",
                keyValue: new Guid("c19aed72-0048-4740-9f04-14f46338b1bf"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("642ee894-7bb4-4950-a429-a837ed9d5386"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("7ee26529-e3d8-40e8-9263-9fbbef958aef"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("8a58170e-d4ed-4c19-b24e-e5c341be474f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "ID",
                keyValue: new Guid("df363e96-9143-4939-b463-67d5a3f40984"));
        }
    }
}
