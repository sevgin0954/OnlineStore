using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddedProductCategoriesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 708, DateTimeKind.Local).AddTicks(837),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 854, DateTimeKind.Local).AddTicks(6105));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 696, DateTimeKind.Local).AddTicks(7975),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 841, DateTimeKind.Local).AddTicks(6031));

            migrationBuilder.AddColumn<string>(
                name: "SubCategoryId",
                table: "Products",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 647, DateTimeKind.Local).AddTicks(8004),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 805, DateTimeKind.Local).AddTicks(9323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 19, 59, 18, 650, DateTimeKind.Local).AddTicks(2314),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 27, 6, 807, DateTimeKind.Local).AddTicks(6161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 592, DateTimeKind.Local).AddTicks(3745),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 762, DateTimeKind.Local).AddTicks(6371));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CategoryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products",
                column: "SubCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_SubCategories_SubCategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Products_SubCategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SubCategoryId",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 854, DateTimeKind.Local).AddTicks(6105),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 708, DateTimeKind.Local).AddTicks(837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 841, DateTimeKind.Local).AddTicks(6031),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 696, DateTimeKind.Local).AddTicks(7975));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 805, DateTimeKind.Local).AddTicks(9323),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 647, DateTimeKind.Local).AddTicks(8004));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 27, 6, 807, DateTimeKind.Local).AddTicks(6161),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 19, 59, 18, 650, DateTimeKind.Local).AddTicks(2314));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 762, DateTimeKind.Local).AddTicks(6371),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 592, DateTimeKind.Local).AddTicks(3745));
        }
    }
}
