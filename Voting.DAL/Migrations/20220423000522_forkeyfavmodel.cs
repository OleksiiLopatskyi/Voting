using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voting.DAL.Migrations
{
    public partial class forkeyfavmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FavoriteModelId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FavoriteModelId",
                table: "AspNetUsers",
                column: "FavoriteModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Models_FavoriteModelId",
                table: "AspNetUsers",
                column: "FavoriteModelId",
                principalTable: "Models",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Models_FavoriteModelId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FavoriteModelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteModelId",
                table: "AspNetUsers");
        }
    }
}
