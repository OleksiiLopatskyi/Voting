using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voting.DAL.Migrations
{
    public partial class forkeyfavmodel3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Models_FavoriteModelId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Models_FavoriteModelId",
                table: "AspNetUsers",
                column: "FavoriteModelId",
                principalTable: "Models",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Models_FavoriteModelId",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Models_FavoriteModelId",
                table: "AspNetUsers",
                column: "FavoriteModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
