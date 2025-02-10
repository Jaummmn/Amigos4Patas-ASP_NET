using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Amigos4Patas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ajusterelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_UserCanils_CanilId1",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_CanilId1",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "CanilId1",
                table: "Pets");

            migrationBuilder.AlterColumn<int>(
                name: "CanilId",
                table: "Pets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_CanilId",
                table: "Pets",
                column: "CanilId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_UserCanils_CanilId",
                table: "Pets",
                column: "CanilId",
                principalTable: "UserCanils",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pets_UserCanils_CanilId",
                table: "Pets");

            migrationBuilder.DropIndex(
                name: "IX_Pets_CanilId",
                table: "Pets");

            migrationBuilder.AlterColumn<string>(
                name: "CanilId",
                table: "Pets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CanilId1",
                table: "Pets",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pets_CanilId1",
                table: "Pets",
                column: "CanilId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pets_UserCanils_CanilId1",
                table: "Pets",
                column: "CanilId1",
                principalTable: "UserCanils",
                principalColumn: "Id");
        }
    }
}
