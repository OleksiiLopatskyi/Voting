using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voting.DAL.Migrations
{
    public partial class changepairpropertis : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_AspNetUsers_AccountId1",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_AccountId1",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "AccountId1",
                table: "Pairs");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "Pairs",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pairs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_AccountId",
                table: "Pairs",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_AspNetUsers_AccountId",
                table: "Pairs",
                column: "AccountId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_AspNetUsers_AccountId",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_AccountId",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Pairs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountId1",
                table: "Pairs",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_AccountId1",
                table: "Pairs",
                column: "AccountId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_AspNetUsers_AccountId1",
                table: "Pairs",
                column: "AccountId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
