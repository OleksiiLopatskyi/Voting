using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voting.DAL.Migrations
{
    public partial class ratingreadonly : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Models");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "Models",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
