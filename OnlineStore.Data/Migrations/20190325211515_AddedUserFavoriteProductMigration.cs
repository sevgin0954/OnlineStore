using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddedUserFavoriteProductMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 25, 23, 15, 15, 37, DateTimeKind.Local).AddTicks(7782),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 260, DateTimeKind.Local).AddTicks(7393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 25, 23, 15, 15, 30, DateTimeKind.Local).AddTicks(3602),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 250, DateTimeKind.Local).AddTicks(529));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 25, 23, 15, 15, 0, DateTimeKind.Local).AddTicks(2391),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 211, DateTimeKind.Local).AddTicks(2731));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 28, 23, 15, 15, 1, DateTimeKind.Local).AddTicks(9238),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 5, 21, 1, 18, 213, DateTimeKind.Local).AddTicks(72));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 25, 23, 15, 14, 970, DateTimeKind.Local).AddTicks(732),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 174, DateTimeKind.Local).AddTicks(6125));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 25, 21, 15, 14, 952, DateTimeKind.Utc).AddTicks(7851),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 1, 18, 157, DateTimeKind.Utc).AddTicks(7736));

            migrationBuilder.CreateTable(
                name: "UsersFavoriteProducts",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    ProductId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersFavoriteProducts", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersFavoriteProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersFavoriteProducts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersFavoriteProducts_UserId",
                table: "UsersFavoriteProducts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersFavoriteProducts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 260, DateTimeKind.Local).AddTicks(7393),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 25, 23, 15, 15, 37, DateTimeKind.Local).AddTicks(7782));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 250, DateTimeKind.Local).AddTicks(529),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 25, 23, 15, 15, 30, DateTimeKind.Local).AddTicks(3602));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 211, DateTimeKind.Local).AddTicks(2731),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 25, 23, 15, 15, 0, DateTimeKind.Local).AddTicks(2391));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 5, 21, 1, 18, 213, DateTimeKind.Local).AddTicks(72),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 28, 23, 15, 15, 1, DateTimeKind.Local).AddTicks(9238));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 174, DateTimeKind.Local).AddTicks(6125),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 25, 23, 15, 14, 970, DateTimeKind.Local).AddTicks(732));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 1, 18, 157, DateTimeKind.Utc).AddTicks(7736),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 25, 21, 15, 14, 952, DateTimeKind.Utc).AddTicks(7851));
        }
    }
}
