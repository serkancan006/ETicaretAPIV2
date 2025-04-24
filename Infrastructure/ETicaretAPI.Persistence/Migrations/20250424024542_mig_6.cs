using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderAddresses_OrderAddressBillingId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderAddresses_OrderAddressShippingId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderAddresses");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderAddressBillingId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderAddressShippingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderAddressBillingId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderAddressShippingId",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "BillingOrderAdres",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingOrderAdresTitle",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingOrderApartmentNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingOrderBuildingNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingOrderCity",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingOrderFloor",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingOrderNeighbourHood",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BillingOrderStreet",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderAdres",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderAdresTitle",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderApartmentNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderBuildingNumber",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderCity",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderFloor",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderNeighbourHood",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ShippingOrderStreet",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingOrderAdres",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingOrderAdresTitle",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingOrderApartmentNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingOrderBuildingNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingOrderCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingOrderFloor",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingOrderNeighbourHood",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BillingOrderStreet",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderAdres",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderAdresTitle",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderApartmentNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderBuildingNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderFloor",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderNeighbourHood",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingOrderStreet",
                table: "Orders");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderAddressBillingId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "OrderAddressShippingId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "OrderAddresses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderAdres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderAdresTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderApartmentNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderBuildingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderFloor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderNeighbourHood = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAddresses", x => x.Id);
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderAddresses_OrderAddressBillingId",
                table: "Orders",
                column: "OrderAddressBillingId",
                principalTable: "OrderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderAddresses_OrderAddressShippingId",
                table: "Orders",
                column: "OrderAddressShippingId",
                principalTable: "OrderAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
