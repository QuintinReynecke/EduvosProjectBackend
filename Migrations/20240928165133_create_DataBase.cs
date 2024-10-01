using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APITEST.Migrations
{
    public partial class create_DataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FAQTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    question = table.Column<string>(nullable: true),
                    answer = table.Column<string>(nullable: true),
                    department = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupsTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    group_name = table.Column<string>(nullable: true),
                    department = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProfilePicture = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    PasswordResetCode = table.Column<string>(nullable: true),
                    PasswordResetExpiration = table.Column<DateTime>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true),
                    RefreshTokenExpiration = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupMessageTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    group_id = table.Column<int>(nullable: false),
                    message = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    mainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupMessageTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupMessageTable_MainTable_mainTableFKId",
                        column: x => x.mainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalChatsTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    facultyType = table.Column<string>(nullable: true),
                    question = table.Column<string>(nullable: true),
                    answer = table.Column<string>(nullable: true),
                    department = table.Column<string>(nullable: true),
                    DateAdded = table.Column<DateTime>(nullable: true),
                    mainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalChatsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalChatsTable_MainTable_mainTableFKId",
                        column: x => x.mainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupMessageTable_mainTableFKId",
                table: "GroupMessageTable",
                column: "mainTableFKId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalChatsTable_mainTableFKId",
                table: "PersonalChatsTable",
                column: "mainTableFKId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FAQTable");

            migrationBuilder.DropTable(
                name: "GroupMessageTable");

            migrationBuilder.DropTable(
                name: "GroupsTable");

            migrationBuilder.DropTable(
                name: "PersonalChatsTable");

            migrationBuilder.DropTable(
                name: "MainTable");
        }
    }
}
