using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amigos4Patas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AjusteApplicationUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_UserCanil_CanilId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCanil_AspNetUsers_UserId",
                table: "UserCanil");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCanil",
                table: "UserCanil");

            migrationBuilder.DropIndex(
                name: "IX_UserCanil_UserId",
                table: "UserCanil");

            migrationBuilder.RenameTable(
                name: "UserCanil",
                newName: "UserCanils");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCanils",
                table: "UserCanils",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCanils_UserId",
                table: "UserCanils",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_UserCanils_CanilId",
                table: "Pets",
                column: "CanilId",
                principalTable: "UserCanils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCanils_AspNetUsers_UserId",
                table: "UserCanils",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_UserCanils_CanilId",
                table: "Pets");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCanils_AspNetUsers_UserId",
                table: "UserCanils");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserCanils",
                table: "UserCanils");

            migrationBuilder.DropIndex(
                name: "IX_UserCanils_UserId",
                table: "UserCanils");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "UserCanils",
                newName: "UserCanil");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Pets",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserCanil",
                table: "UserCanil",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserCanil_UserId",
                table: "UserCanil",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_UserCanil_CanilId",
                table: "Pets",
                column: "CanilId",
                principalTable: "UserCanil",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCanil_AspNetUsers_UserId",
                table: "UserCanil",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
