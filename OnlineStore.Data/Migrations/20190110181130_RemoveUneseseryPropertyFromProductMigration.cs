using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class RemoveUneseseryPropertyFromProductMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderQuantity",
                table: "Products");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 810, DateTimeKind.Local).AddTicks(5446),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 202, DateTimeKind.Local).AddTicks(6621));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 792, DateTimeKind.Local).AddTicks(2570),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 189, DateTimeKind.Local).AddTicks(7042));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 724, DateTimeKind.Local).AddTicks(4093),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 131, DateTimeKind.Local).AddTicks(203));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 13, 20, 11, 29, 728, DateTimeKind.Local).AddTicks(1815),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 12, 10, 133, DateTimeKind.Local).AddTicks(4929));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 635, DateTimeKind.Local).AddTicks(6523),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 50, DateTimeKind.Local).AddTicks(7921));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 18, 11, 29, 617, DateTimeKind.Utc).AddTicks(8521),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 18, 12, 10, 26, DateTimeKind.Utc).AddTicks(2808));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 202, DateTimeKind.Local).AddTicks(6621),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 810, DateTimeKind.Local).AddTicks(5446));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 189, DateTimeKind.Local).AddTicks(7042),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 792, DateTimeKind.Local).AddTicks(2570));

            migrationBuilder.AddColumn<int>(
                name: "OrderQuantity",
                table: "Products",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 131, DateTimeKind.Local).AddTicks(203),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 724, DateTimeKind.Local).AddTicks(4093));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 12, 10, 133, DateTimeKind.Local).AddTicks(4929),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 13, 20, 11, 29, 728, DateTimeKind.Local).AddTicks(1815));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 20, 12, 10, 50, DateTimeKind.Local).AddTicks(7921),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 635, DateTimeKind.Local).AddTicks(6523));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 18, 12, 10, 26, DateTimeKind.Utc).AddTicks(2808),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 18, 11, 29, 617, DateTimeKind.Utc).AddTicks(8521));
        }
    }
}
