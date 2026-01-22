using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsAndLinks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Links",
                columns: new[] { "Id", "Description", "InterestId", "PersonId", "Url" },
                values: new object[,]
                {
                    { 1, "Recipes", 1, 1, "https://example.com/recipes" },
                    { 2, "C# tutorials", 3, 1, "https://example.com/csharp" },
                    { 3, "Art inspiration", 2, 2, "https://example.com/art" }
                });

            migrationBuilder.InsertData(
                table: "PersonInterests",
                columns: new[] { "InterestId", "PersonId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Links",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PersonInterests",
                keyColumns: new[] { "InterestId", "PersonId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "PersonInterests",
                keyColumns: new[] { "InterestId", "PersonId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "PersonInterests",
                keyColumns: new[] { "InterestId", "PersonId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "PersonInterests",
                keyColumns: new[] { "InterestId", "PersonId" },
                keyValues: new object[] { 3, 3 });
        }
    }
}
