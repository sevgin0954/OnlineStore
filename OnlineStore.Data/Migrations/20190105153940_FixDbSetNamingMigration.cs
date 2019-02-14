using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class FixDbSetNamingMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliverysInfos_DeliveryInfoId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliverysTypes_DeliveryTypeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrdersStatuses_OrderStatusId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_PaymentTypes_PaymentTypeId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Order_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_PaymentTypeId",
                table: "Orders",
                newName: "IX_Orders_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_OrderStatusId",
                table: "Orders",
                newName: "IX_Orders_OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryTypeId",
                table: "Orders",
                newName: "IX_Orders_DeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_DeliveryInfoId",
                table: "Orders",
                newName: "IX_Orders_DeliveryInfoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 981, DateTimeKind.Local).AddTicks(9093),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 219, DateTimeKind.Local).AddTicks(1543));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 973, DateTimeKind.Local).AddTicks(7103),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 210, DateTimeKind.Local).AddTicks(9306));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 909, DateTimeKind.Local).AddTicks(2936),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 151, DateTimeKind.Local).AddTicks(7118));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 894, DateTimeKind.Local).AddTicks(332),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 135, DateTimeKind.Local).AddTicks(8681));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 943, DateTimeKind.Local).AddTicks(198),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 180, DateTimeKind.Local).AddTicks(7630));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Orders",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 8, 17, 39, 39, 944, DateTimeKind.Local).AddTicks(4648),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 8, 16, 54, 11, 182, DateTimeKind.Local).AddTicks(2863));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliverysInfos_DeliveryInfoId",
                table: "Orders",
                column: "DeliveryInfoId",
                principalTable: "DeliverysInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliverysTypes_DeliveryTypeId",
                table: "Orders",
                column: "DeliveryTypeId",
                principalTable: "DeliverysTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrdersStatuses_OrderStatusId",
                table: "Orders",
                column: "OrderStatusId",
                principalTable: "OrdersStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_PaymentTypes_PaymentTypeId",
                table: "Orders",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_Orders_OrderId",
                table: "OrderProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliverysInfos_DeliveryInfoId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliverysTypes_DeliveryTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrdersStatuses_OrderStatusId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_PaymentTypes_PaymentTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_PaymentTypeId",
                table: "Order",
                newName: "IX_Order_PaymentTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_OrderStatusId",
                table: "Order",
                newName: "IX_Order_OrderStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryTypeId",
                table: "Order",
                newName: "IX_Order_DeliveryTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveryInfoId",
                table: "Order",
                newName: "IX_Order_DeliveryInfoId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 219, DateTimeKind.Local).AddTicks(1543),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 981, DateTimeKind.Local).AddTicks(9093));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 210, DateTimeKind.Local).AddTicks(9306),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 973, DateTimeKind.Local).AddTicks(7103));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 151, DateTimeKind.Local).AddTicks(7118),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 909, DateTimeKind.Local).AddTicks(2936));

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegisterDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 135, DateTimeKind.Local).AddTicks(8681),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 894, DateTimeKind.Local).AddTicks(332));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 5, 16, 54, 11, 180, DateTimeKind.Local).AddTicks(7630),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 5, 17, 39, 39, 943, DateTimeKind.Local).AddTicks(198));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2019, 1, 8, 16, 54, 11, 182, DateTimeKind.Local).AddTicks(2863),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2019, 1, 8, 17, 39, 39, 944, DateTimeKind.Local).AddTicks(4648));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliverysInfos_DeliveryInfoId",
                table: "Order",
                column: "DeliveryInfoId",
                principalTable: "DeliverysInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliverysTypes_DeliveryTypeId",
                table: "Order",
                column: "DeliveryTypeId",
                principalTable: "DeliverysTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrdersStatuses_OrderStatusId",
                table: "Order",
                column: "OrderStatusId",
                principalTable: "OrdersStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_PaymentTypes_PaymentTypeId",
                table: "Order",
                column: "PaymentTypeId",
                principalTable: "PaymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_Order_OrderId",
                table: "OrderProducts",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
