using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace CoreApplication.Data.Migrations
{
    public partial class MessageEntityCorrected : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_LoginUsers_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_LoginUsers_SenderId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "AllMessages");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "AllMessages",
                newName: "IX_AllMessages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverId",
                table: "AllMessages",
                newName: "IX_AllMessages_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AllMessages",
                table: "AllMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AllMessages_LoginUsers_ReceiverId",
                table: "AllMessages",
                column: "ReceiverId",
                principalTable: "LoginUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AllMessages_LoginUsers_SenderId",
                table: "AllMessages",
                column: "SenderId",
                principalTable: "LoginUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AllMessages_LoginUsers_ReceiverId",
                table: "AllMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_AllMessages_LoginUsers_SenderId",
                table: "AllMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AllMessages",
                table: "AllMessages");

            migrationBuilder.RenameTable(
                name: "AllMessages",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_AllMessages_SenderId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_AllMessages_ReceiverId",
                table: "Messages",
                newName: "IX_Messages_ReceiverId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_LoginUsers_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "LoginUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_LoginUsers_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "LoginUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
