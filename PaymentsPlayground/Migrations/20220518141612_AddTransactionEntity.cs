using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentsPlayground.Migrations
{
    public partial class AddTransactionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_RecieverId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_SenderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_RecieverId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_SenderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "RecieverId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Transactions",
                newName: "UserPaymentId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "RegisterTime");

            migrationBuilder.AddColumn<string>(
                name: "ErrorDescription",
                table: "Transactions",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "FinishTime",
                table: "Transactions",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "UserPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SenderId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RecieverId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserPayments_AspNetUsers_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserPayments_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserPaymentId",
                table: "Transactions",
                column: "UserPaymentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_RecieverId",
                table: "UserPayments",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPayments_SenderId",
                table: "UserPayments",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_UserPayments_UserPaymentId",
                table: "Transactions",
                column: "UserPaymentId",
                principalTable: "UserPayments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_UserPayments_UserPaymentId",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "UserPayments");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserPaymentId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ErrorDescription",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "UserPaymentId",
                table: "Transactions",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "RegisterTime",
                table: "Transactions",
                newName: "Date");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Transactions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RecieverId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SenderId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_RecieverId",
                table: "Transactions",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_SenderId",
                table: "Transactions",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_RecieverId",
                table: "Transactions",
                column: "RecieverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_SenderId",
                table: "Transactions",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
