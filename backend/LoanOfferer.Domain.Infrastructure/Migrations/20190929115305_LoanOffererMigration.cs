using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LoanOfferer.Domain.Infrastructure.Migrations
{
    public partial class LoanOffererMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoanOffers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Pesel = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    MaxLoanAmount = table.Column<int>(nullable: false),
                    RequestedLoanAmount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanOffers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanOffers");
        }
    }
}
