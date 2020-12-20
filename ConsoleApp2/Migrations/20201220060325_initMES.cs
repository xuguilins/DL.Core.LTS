using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp2.Migrations
{
    public partial class initMES : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedTime = table.Column<DateTime>(maxLength: 20, nullable: false),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    PassWord = table.Column<string>(maxLength: 50, nullable: true),
                    UserCount = table.Column<int>(maxLength: 10, nullable: false),
                    CreateUser = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");
        }
    }
}
