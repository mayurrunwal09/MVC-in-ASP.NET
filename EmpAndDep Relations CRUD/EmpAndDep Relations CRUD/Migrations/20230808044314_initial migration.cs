using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpAndDep_Relations_CRUD.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "Varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Department_Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Employee_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Employee_Name = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Employee_Addr = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Employee_DOB = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Employee_Age = table.Column<string>(type: "varchar(50)", nullable: false),
                    Employee_Salary = table.Column<decimal>(type: "decimal(10,0)", nullable: false),
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Employee_Id);
                    table.ForeignKey(
                        name: "FK_Employees_Department_Department_Id",
                        column: x => x.Department_Id,
                        principalTable: "Department",
                        principalColumn: "Department_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Department_Id",
                table: "Employees",
                column: "Department_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Department");
        }
    }
}
