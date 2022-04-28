using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voting.DAL.Migrations
{
    public partial class changeuserclassname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_AspNetUsers_AccountId",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_AccountId",
                table: "Pairs");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Pairs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pairs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Pairs_UserId",
                table: "Pairs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pairs_AspNetUsers_UserId",
                table: "Pairs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pairs_AspNetUsers_UserId",
                table: "Pairs");

            migrationBuilder.DropIndex(
                name: "IX_Pairs_UserId",
                table: "Pairs");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Pairs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "AccountId",
                table: "Pairs",
                type: "nvarchar(450)",
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
    }
}
