using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp2.Migrations
{
    public partial class initbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StduentInfo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    StudentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StduentInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StduentInfo");

            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
