using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddedRegisterDateToUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 219, DateTimeKind.Local).AddTicks(1543),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 708, DateTimeKind.Local).AddTicks(837));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 210, DateTimeKind.Local).AddTicks(9306),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 696, DateTimeKind.Local).AddTicks(7975));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 180, DateTimeKind.Local).AddTicks(7630),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 647, DateTimeKind.Local).AddTicks(8004));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 8, 16, 54, 11, 182, DateTimeKind.Local).AddTicks(2863),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 7, 19, 59, 18, 650, DateTimeKind.Local).AddTicks(2314));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 151, DateTimeKind.Local).AddTicks(7118),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 592, DateTimeKind.Local).AddTicks(3745));

            migrationBuilder.AddColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 135, DateTimeKind.Local).AddTicks(8681));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterDate",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 708, DateTimeKind.Local).AddTicks(837),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 219, DateTimeKind.Local).AddTicks(1543));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 696, DateTimeKind.Local).AddTicks(7975),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 210, DateTimeKind.Local).AddTicks(9306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 647, DateTimeKind.Local).AddTicks(8004),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 180, DateTimeKind.Local).AddTicks(7630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 7, 19, 59, 18, 650, DateTimeKind.Local).AddTicks(2314),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 8, 16, 54, 11, 182, DateTimeKind.Local).AddTicks(2863));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 4, 19, 59, 18, 592, DateTimeKind.Local).AddTicks(3745),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 151, DateTimeKind.Local).AddTicks(7118));
        }
    }
}
