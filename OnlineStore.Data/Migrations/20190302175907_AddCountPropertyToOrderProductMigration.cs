using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddCountPropertyToOrderProductMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 302, DateTimeKind.Local).AddTicks(241),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 904, DateTimeKind.Local).AddTicks(8018));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 293, DateTimeKind.Local).AddTicks(6078),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 888, DateTimeKind.Local).AddTicks(9056));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 260, DateTimeKind.Local).AddTicks(131),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 847, DateTimeKind.Local).AddTicks(9509));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 5, 19, 59, 7, 261, DateTimeKind.Local).AddTicks(6824),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 5, 16, 40, 48, 849, DateTimeKind.Local).AddTicks(4439));

            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "OrderProducts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 223, DateTimeKind.Local).AddTicks(327),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 801, DateTimeKind.Local).AddTicks(7408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 17, 59, 7, 211, DateTimeKind.Utc).AddTicks(6268),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 14, 40, 48, 785, DateTimeKind.Utc).AddTicks(6054));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "OrderProducts");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 904, DateTimeKind.Local).AddTicks(8018),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 302, DateTimeKind.Local).AddTicks(241));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 888, DateTimeKind.Local).AddTicks(9056),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 293, DateTimeKind.Local).AddTicks(6078));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 847, DateTimeKind.Local).AddTicks(9509),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 260, DateTimeKind.Local).AddTicks(131));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 5, 16, 40, 48, 849, DateTimeKind.Local).AddTicks(4439),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 5, 19, 59, 7, 261, DateTimeKind.Local).AddTicks(6824));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 801, DateTimeKind.Local).AddTicks(7408),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 223, DateTimeKind.Local).AddTicks(327));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 14, 40, 48, 785, DateTimeKind.Utc).AddTicks(6054),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 17, 59, 7, 211, DateTimeKind.Utc).AddTicks(6268));
        }
    }
}
