using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TransactionEFCore.Migrations
{
    /// <inheritdoc />
    public partial class initialmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Branch = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AccountNo = table.Column<int>(type: "int", maxLength: 14, nullable: false),
                    Balance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IFSCode = table.Column<int>(type: "int", maxLength: 12, nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactioId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
