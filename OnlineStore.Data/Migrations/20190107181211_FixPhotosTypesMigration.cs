using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class FixPhotosTypesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewPhotos");

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 202, DateTimeKind.Local).AddTicks(6621),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 182, DateTimeKind.Local).AddTicks(67));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 189, DateTimeKind.Local).AddTicks(7042),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 153, DateTimeKind.Local).AddTicks(3745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 131, DateTimeKind.Local).AddTicks(203),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 5, DateTimeKind.Local).AddTicks(4806));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 12, 10, 133, DateTimeKind.Local).AddTicks(4929),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 8, 20, 23, 39, 10, DateTimeKind.Local).AddTicks(7118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 50, DateTimeKind.Local).AddTicks(7921),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 20, 23, 38, 808, DateTimeKind.Local).AddTicks(1629));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 18, 12, 10, 26, DateTimeKind.Utc).AddTicks(2808),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 18, 23, 38, 760, DateTimeKind.Utc).AddTicks(8759));

            migrationBuilder.AddColumn<string>(
                name: "PhotoId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Data = table.Column<byte[]>(nullable: false),
                    ReviewId = table.Column<string>(nullable: true),
                    ProductId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Photos_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                unique: true,
                filter: "[PhotoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ProductId",
                table: "Photos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_ReviewId",
                table: "Photos",
                column: "ReviewId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers",
                column: "PhotoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PhotoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 182, DateTimeKind.Local).AddTicks(67),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 202, DateTimeKind.Local).AddTicks(6621));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 153, DateTimeKind.Local).AddTicks(3745),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 189, DateTimeKind.Local).AddTicks(7042));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 5, DateTimeKind.Local).AddTicks(4806),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 131, DateTimeKind.Local).AddTicks(203));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 8, 20, 23, 39, 10, DateTimeKind.Local).AddTicks(7118),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 12, 10, 133, DateTimeKind.Local).AddTicks(4929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 20, 23, 38, 808, DateTimeKind.Local).AddTicks(1629),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 50, DateTimeKind.Local).AddTicks(7921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 18, 23, 38, 760, DateTimeKind.Utc).AddTicks(8759),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 18, 12, 10, 26, DateTimeKind.Utc).AddTicks(2808));

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReviewPhotos",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Data = table.Column<byte[]>(nullable: false),
                    ProductId = table.Column<string>(nullable: true),
                    ReviewId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewPhotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewPhotos_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewPhotos_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReviewPhotos_ProductId",
                table: "ReviewPhotos",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewPhotos_ReviewId",
                table: "ReviewPhotos",
                column: "ReviewId");
        }
    }
}
