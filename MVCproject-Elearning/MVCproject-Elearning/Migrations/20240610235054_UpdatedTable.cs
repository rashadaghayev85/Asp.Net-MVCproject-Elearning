using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCproject_Elearning.Migrations
{
    public partial class UpdatedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_CoursesId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Course",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "CoursesId",
                table: "Students");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Course",
                table: "Students",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CoursesId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_CoursesId",
                table: "Students",
                column: "CoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Courses_CoursesId",
                table: "Students",
                column: "CoursesId",
                principalTable: "Courses",
                principalColumn: "Id");
        }
    }
}
