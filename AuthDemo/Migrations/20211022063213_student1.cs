using Microsoft.EntityFrameworkCore.Migrations;

namespace AuthDemo.Migrations
{
    public partial class student1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    sRoll = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    sName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sStd = table.Column<int>(type: "int", nullable: false),
                    sDiv = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.sRoll);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
