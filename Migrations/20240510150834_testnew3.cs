using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace APITEST.Migrations
{
    public partial class testnew3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProfilePicture = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    UserRating = table.Column<float>(nullable: false),
                    Type = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    CallOutFee = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    TotalPhotos = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTable", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TypeOfService = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessHoursTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Monday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    Sunday = table.Column<bool>(nullable: false),
                    WorkHours = table.Column<string>(nullable: true),
                    mainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessHoursTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusinessHoursTable_MainTable_mainTableFKId",
                        column: x => x.mainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactDetailsTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Phone = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    mainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactDetailsTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactDetailsTable_MainTable_mainTableFKId",
                        column: x => x.mainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    MainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequests_MainTable_MainTableFKId",
                        column: x => x.MainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhotosTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Image1 = table.Column<string>(nullable: true),
                    Image2 = table.Column<string>(nullable: true),
                    Image3 = table.Column<string>(nullable: true),
                    Image4 = table.Column<string>(nullable: true),
                    Image5 = table.Column<string>(nullable: true),
                    mainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotosTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhotosTable_MainTable_mainTableFKId",
                        column: x => x.mainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SumOfTotalRatings = table.Column<int>(nullable: false),
                    SumOfTotalUserRated = table.Column<int>(nullable: false),
                    mainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewTable_MainTable_mainTableFKId",
                        column: x => x.mainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    isCertified = table.Column<bool>(nullable: false),
                    proofOfCertification = table.Column<string>(nullable: true),
                    mainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTable_MainTable_mainTableFKId",
                        column: x => x.mainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkLocationTable",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    workInCountry = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Suburb = table.Column<string>(nullable: true),
                    mainTableFKId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLocationTable", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkLocationTable_MainTable_mainTableFKId",
                        column: x => x.mainTableFKId,
                        principalTable: "MainTable",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessHoursTable_mainTableFKId",
                table: "BusinessHoursTable",
                column: "mainTableFKId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactDetailsTable_mainTableFKId",
                table: "ContactDetailsTable",
                column: "mainTableFKId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_MainTableFKId",
                table: "JobRequests",
                column: "MainTableFKId");

            migrationBuilder.CreateIndex(
                name: "IX_PhotosTable_mainTableFKId",
                table: "PhotosTable",
                column: "mainTableFKId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTable_mainTableFKId",
                table: "ReviewTable",
                column: "mainTableFKId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTable_mainTableFKId",
                table: "UserTable",
                column: "mainTableFKId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkLocationTable_mainTableFKId",
                table: "WorkLocationTable",
                column: "mainTableFKId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessHoursTable");

            migrationBuilder.DropTable(
                name: "ContactDetailsTable");

            migrationBuilder.DropTable(
                name: "JobRequests");

            migrationBuilder.DropTable(
                name: "PhotosTable");

            migrationBuilder.DropTable(
                name: "ReviewTable");

            migrationBuilder.DropTable(
                name: "ServiceList");

            migrationBuilder.DropTable(
                name: "UserTable");

            migrationBuilder.DropTable(
                name: "WorkLocationTable");

            migrationBuilder.DropTable(
                name: "MainTable");
        }
    }
}
