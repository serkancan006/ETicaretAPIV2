using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderPayments_OrderPaymentId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderPayments");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderAddressBillingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderAddressShippingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderPaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderPaymentId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "NameSurname",
                table: "AspNetUsers",
                newName: "Surname");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "SubCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderAddressShippingId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderAddressBillingId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConversationId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaidPrice",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "MainCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderAddressBillingId",
                table: "Orders",
                column: "OrderAddressBillingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderAddressShippingId",
                table: "Orders",
                column: "OrderAddressShippingId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderAddressBillingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderAddressShippingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ConversationId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaidPrice",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "AspNetUsers",
                newName: "NameSurname");

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "SubCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PaymentType",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderAddressShippingId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderAddressBillingId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderPaymentId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ImagePath",
                table: "MainCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "OrderPayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CardToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPayments", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderAddressBillingId",
                table: "Orders",
                column: "OrderAddressBillingId",
                unique: true,
                filter: "[OrderAddressBillingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderAddressShippingId",
                table: "Orders",
                column: "OrderAddressShippingId",
                unique: true,
                filter: "[OrderAddressShippingId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderPaymentId",
                table: "Orders",
                column: "OrderPaymentId",
                unique: true,
                filter: "[OrderPaymentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderPayments_OrderPaymentId",
                table: "Orders",
                column: "OrderPaymentId",
                principalTable: "OrderPayments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
