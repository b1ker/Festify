using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Festify.Promotion.Migrations
{
    /// <inheritdoc />
    public partial class AddPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    CreditCardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Total = table.Column<decimal>(type: "decimal(19,4)", precision: 19, scale: 4, nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentGuid);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");
        }
    }
}
