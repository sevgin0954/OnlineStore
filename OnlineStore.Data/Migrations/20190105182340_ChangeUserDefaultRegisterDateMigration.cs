using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class ChangeUserDefaultRegisterDateMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 182, DateTimeKind.Local).AddTicks(67),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 981, DateTimeKind.Local).AddTicks(9093));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 153, DateTimeKind.Local).AddTicks(3745),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 973, DateTimeKind.Local).AddTicks(7103));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 5, DateTimeKind.Local).AddTicks(4806),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 943, DateTimeKind.Local).AddTicks(198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 8, 20, 23, 39, 10, DateTimeKind.Local).AddTicks(7118),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 8, 17, 39, 39, 944, DateTimeKind.Local).AddTicks(4648));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 20, 23, 38, 808, DateTimeKind.Local).AddTicks(1629),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 909, DateTimeKind.Local).AddTicks(2936));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 18, 23, 38, 760, DateTimeKind.Utc).AddTicks(8759),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 894, DateTimeKind.Local).AddTicks(332));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 981, DateTimeKind.Local).AddTicks(9093),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 182, DateTimeKind.Local).AddTicks(67));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 973, DateTimeKind.Local).AddTicks(7103),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 153, DateTimeKind.Local).AddTicks(3745));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 943, DateTimeKind.Local).AddTicks(198),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 20, 23, 39, 5, DateTimeKind.Local).AddTicks(4806));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 8, 17, 39, 39, 944, DateTimeKind.Local).AddTicks(4648),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 8, 20, 23, 39, 10, DateTimeKind.Local).AddTicks(7118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 909, DateTimeKind.Local).AddTicks(2936),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 20, 23, 38, 808, DateTimeKind.Local).AddTicks(1629));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 894, DateTimeKind.Local).AddTicks(332),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 18, 23, 38, 760, DateTimeKind.Utc).AddTicks(8759));
        }
    }
}
