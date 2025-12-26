using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApplication.Data.Migrations
{
    public partial class BusinessNameProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "businessName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "businessName",
                table: "AspNetUsers");
        }
    }
}
