using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class RemovedDeliveryTypeModelMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliverysTypes_DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "DeliverysTypes");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryTypeId",
                table: "Orders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 904, DateTimeKind.Local).AddTicks(8018),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 810, DateTimeKind.Local).AddTicks(5446));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 888, DateTimeKind.Local).AddTicks(9056),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 792, DateTimeKind.Local).AddTicks(2570));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 847, DateTimeKind.Local).AddTicks(9509),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 724, DateTimeKind.Local).AddTicks(4093));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 5, 16, 40, 48, 849, DateTimeKind.Local).AddTicks(4439),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 13, 20, 11, 29, 728, DateTimeKind.Local).AddTicks(1815));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 801, DateTimeKind.Local).AddTicks(7408),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 635, DateTimeKind.Local).AddTicks(6523));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 3, 2, 14, 40, 48, 785, DateTimeKind.Utc).AddTicks(6054),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 10, 18, 11, 29, 617, DateTimeKind.Utc).AddTicks(8521));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 810, DateTimeKind.Local).AddTicks(5446),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 904, DateTimeKind.Local).AddTicks(8018));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 792, DateTimeKind.Local).AddTicks(2570),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 888, DateTimeKind.Local).AddTicks(9056));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 724, DateTimeKind.Local).AddTicks(4093),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 847, DateTimeKind.Local).AddTicks(9509));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 13, 20, 11, 29, 728, DateTimeKind.Local).AddTicks(1815),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 5, 16, 40, 48, 849, DateTimeKind.Local).AddTicks(4439));

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTypeId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 20, 11, 29, 635, DateTimeKind.Local).AddTicks(6523),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 16, 40, 48, 801, DateTimeKind.Local).AddTicks(7408));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 10, 18, 11, 29, 617, DateTimeKind.Utc).AddTicks(8521),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 3, 2, 14, 40, 48, 785, DateTimeKind.Utc).AddTicks(6054));

            migrationBuilder.CreateTable(
                name: "DeliverysTypes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverysTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliverysTypes_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId",
                principalTable: "DeliverysTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
