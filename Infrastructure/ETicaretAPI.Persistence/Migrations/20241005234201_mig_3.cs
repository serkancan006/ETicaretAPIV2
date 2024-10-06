using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ETicaretAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Products_ProductId",
                table: "Files");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Files",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Files");

            migrationBuilder.RenameTable(
                name: "Files",
                newName: "ProductImageFiles");

            migrationBuilder.RenameIndex(
                name: "IX_Files_ProductId",
                table: "ProductImageFiles",
                newName: "IX_ProductImageFiles_ProductId");

            migrationBuilder.AlterColumn<bool>(
                name: "Showcase",
                table: "ProductImageFiles",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ProductImageFiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductImageFiles",
                table: "ProductImageFiles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImageFiles_Products_ProductId",
                table: "ProductImageFiles",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImageFiles_Products_ProductId",
                table: "ProductImageFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductImageFiles",
                table: "ProductImageFiles");

            migrationBuilder.RenameTable(
                name: "ProductImageFiles",
                newName: "Files");

            migrationBuilder.RenameIndex(
                name: "IX_ProductImageFiles_ProductId",
                table: "Files",
                newName: "IX_Files_ProductId");

            migrationBuilder.AlterColumn<bool>(
                name: "Showcase",
                table: "Files",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "Files",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Files",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Files",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Files",
                table: "Files",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Products_ProductId",
                table: "Files",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
