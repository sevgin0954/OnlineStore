using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class Update_User_DeliveryInfo_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 536, DateTimeKind.Local).AddTicks(1301),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 555, DateTimeKind.Local).AddTicks(3582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 524, DateTimeKind.Local).AddTicks(86),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 546, DateTimeKind.Local).AddTicks(5271));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 494, DateTimeKind.Local).AddTicks(3261),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 517, DateTimeKind.Local).AddTicks(8645));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 31, 23, 22, 13, 496, DateTimeKind.Local).AddTicks(6200),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 29, 16, 1, 6, 519, DateTimeKind.Local).AddTicks(3336));

            migrationBuilder.AddColumn<string>(
                name: "PopulatedPlaceId",
                table: "DeliverysInfos",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 455, DateTimeKind.Local).AddTicks(8611),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 481, DateTimeKind.Local).AddTicks(6408));

            migrationBuilder.CreateIndex(
                name: "IX_DeliverysInfos_PopulatedPlaceId",
                table: "DeliverysInfos",
                column: "PopulatedPlaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliverysInfos_PopulatedPlaces_PopulatedPlaceId",
                table: "DeliverysInfos",
                column: "PopulatedPlaceId",
                principalTable: "PopulatedPlaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliverysInfos_PopulatedPlaces_PopulatedPlaceId",
                table: "DeliverysInfos");

            migrationBuilder.DropIndex(
                name: "IX_DeliverysInfos_PopulatedPlaceId",
                table: "DeliverysInfos");

            migrationBuilder.DropColumn(
                name: "PopulatedPlaceId",
                table: "DeliverysInfos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 555, DateTimeKind.Local).AddTicks(3582),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 536, DateTimeKind.Local).AddTicks(1301));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 546, DateTimeKind.Local).AddTicks(5271),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 524, DateTimeKind.Local).AddTicks(86));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 517, DateTimeKind.Local).AddTicks(8645),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 494, DateTimeKind.Local).AddTicks(3261));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 29, 16, 1, 6, 519, DateTimeKind.Local).AddTicks(3336),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 31, 23, 22, 13, 496, DateTimeKind.Local).AddTicks(6200));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 481, DateTimeKind.Local).AddTicks(6408),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 28, 23, 22, 13, 455, DateTimeKind.Local).AddTicks(8611));
        }
    }
}
