using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Employees.API.Migrations
{
    public partial class newmig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "EmployeeDb");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employees",
                newSchema: "EmployeeDb");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CPF",
                schema: "EmployeeDb",
                table: "Employees",
                column: "CPF");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Employees_CPF",
                schema: "EmployeeDb",
                table: "Employees");

            migrationBuilder.RenameTable(
                name: "Employees",
                schema: "EmployeeDb",
                newName: "Employees");
        }
    }
}
