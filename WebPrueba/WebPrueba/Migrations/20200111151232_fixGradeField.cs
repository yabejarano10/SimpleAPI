using Microsoft.EntityFrameworkCore.Migrations;

namespace WebPrueba.Migrations
{
    public partial class fixGradeField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "Subjects");

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "StudentSubject",
                type: "nvarchar(3)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Grade",
                table: "StudentSubject");

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "Subjects",
                type: "nvarchar(3)",
                nullable: false,
                defaultValue: "");
        }
    }
}
