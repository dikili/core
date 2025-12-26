using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApplication.Data.Migrations
{
    public partial class twitterandfacebook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "face",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "twit",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "face",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "twit",
                table: "AspNetUsers");
        }
    }
}
