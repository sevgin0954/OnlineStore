using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddFullNamePropToUserMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 854, DateTimeKind.Local).AddTicks(6105),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 536, DateTimeKind.Local).AddTicks(1301));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 841, DateTimeKind.Local).AddTicks(6031),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 524, DateTimeKind.Local).AddTicks(86));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 805, DateTimeKind.Local).AddTicks(9323),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 494, DateTimeKind.Local).AddTicks(3261));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 27, 6, 807, DateTimeKind.Local).AddTicks(6161),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 31, 23, 22, 13, 496, DateTimeKind.Local).AddTicks(6200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 762, DateTimeKind.Local).AddTicks(6371),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 455, DateTimeKind.Local).AddTicks(8611));

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 536, DateTimeKind.Local).AddTicks(1301),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 854, DateTimeKind.Local).AddTicks(6105));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 524, DateTimeKind.Local).AddTicks(86),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 841, DateTimeKind.Local).AddTicks(6031));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 494, DateTimeKind.Local).AddTicks(3261),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 805, DateTimeKind.Local).AddTicks(9323));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 31, 23, 22, 13, 496, DateTimeKind.Local).AddTicks(6200),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 27, 6, 807, DateTimeKind.Local).AddTicks(6161));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 455, DateTimeKind.Local).AddTicks(8611),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 2, 16, 27, 6, 762, DateTimeKind.Local).AddTicks(6371));
        }
    }
}
