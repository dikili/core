using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreApplication.Data.Migrations
{
    public partial class LikeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    LikeeId = table.Column<int>(type: "int", nullable: false),
                    LikerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => new { x.LikeeId, x.LikerId });
                    table.ForeignKey(
                        name: "FK_Likes_LoginUsers_LikeeId",
                        column: x => x.LikeeId,
                        principalTable: "LoginUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Likes_LoginUsers_LikerId",
                        column: x => x.LikerId,
                        principalTable: "LoginUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Likes_LikerId",
                table: "Likes",
                column: "LikerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Likes");
        }
    }
}
