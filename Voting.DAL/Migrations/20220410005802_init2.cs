using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Voting.DAL.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ModelsPair",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstModelId = table.Column<int>(type: "int", nullable: true),
                    SecondModelId = table.Column<int>(type: "int", nullable: true),
                    IsVoted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelsPair", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ModelsPair_Models_FirstModelId",
                        column: x => x.FirstModelId,
                        principalTable: "Models",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ModelsPair_Models_SecondModelId",
                        column: x => x.SecondModelId,
                        principalTable: "Models",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ModelsPair_FirstModelId",
                table: "ModelsPair",
                column: "FirstModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ModelsPair_SecondModelId",
                table: "ModelsPair",
                column: "SecondModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ModelsPair");
        }
    }
}
