using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SionBooks.Migrations
{
    public partial class changeimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Book",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Book");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Book",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
