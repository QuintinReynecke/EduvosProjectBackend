using Microsoft.EntityFrameworkCore.Migrations;

namespace APITEST.Migrations
{
    public partial class testnew4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Suburb",
                table: "WorkLocationTable",
                newName: "suburb");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "WorkLocationTable",
                newName: "province");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "suburb",
                table: "WorkLocationTable",
                newName: "Suburb");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "WorkLocationTable",
                newName: "Province");
        }
    }
}
