using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class AddedNavigationPropertiesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 260, DateTimeKind.Local).AddTicks(7393),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 302, DateTimeKind.Local).AddTicks(241));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 250, DateTimeKind.Local).AddTicks(529),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 293, DateTimeKind.Local).AddTicks(6078));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 211, DateTimeKind.Local).AddTicks(2731),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 260, DateTimeKind.Local).AddTicks(131));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 5, 21, 1, 18, 213, DateTimeKind.Local).AddTicks(72),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 5, 19, 59, 7, 261, DateTimeKind.Local).AddTicks(6824));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 174, DateTimeKind.Local).AddTicks(6125),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 223, DateTimeKind.Local).AddTicks(327));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 1, 18, 157, DateTimeKind.Utc).AddTicks(7736),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 17, 59, 7, 211, DateTimeKind.Utc).AddTicks(6268));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 302, DateTimeKind.Local).AddTicks(241),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 260, DateTimeKind.Local).AddTicks(7393));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 293, DateTimeKind.Local).AddTicks(6078),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 250, DateTimeKind.Local).AddTicks(529));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 260, DateTimeKind.Local).AddTicks(131),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 211, DateTimeKind.Local).AddTicks(2731));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 5, 19, 59, 7, 261, DateTimeKind.Local).AddTicks(6824),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 5, 21, 1, 18, 213, DateTimeKind.Local).AddTicks(72));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 19, 59, 7, 223, DateTimeKind.Local).AddTicks(327),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 21, 1, 18, 174, DateTimeKind.Local).AddTicks(6125));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 17, 59, 7, 211, DateTimeKind.Utc).AddTicks(6268),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 19, 1, 18, 157, DateTimeKind.Utc).AddTicks(7736));
        }
    }
}
