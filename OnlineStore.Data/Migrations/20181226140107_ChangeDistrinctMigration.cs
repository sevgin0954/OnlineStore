using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineStore.Data.Migrations
{
    public partial class ChangeDistrinctMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PopulatedPlace",
                table: "Districts",
                newName: "Name");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 555, DateTimeKind.Local).AddTicks(3582),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 17, 19, 22, 19, 755, DateTimeKind.Local).AddTicks(7175));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 546, DateTimeKind.Local).AddTicks(5271),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 17, 19, 22, 19, 729, DateTimeKind.Local).AddTicks(5648));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 517, DateTimeKind.Local).AddTicks(8645),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 17, 19, 22, 19, 636, DateTimeKind.Local).AddTicks(7552));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 29, 16, 1, 6, 519, DateTimeKind.Local).AddTicks(3336),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 20, 19, 22, 19, 646, DateTimeKind.Local).AddTicks(2354));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 481, DateTimeKind.Local).AddTicks(6408),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 17, 19, 22, 19, 528, DateTimeKind.Local).AddTicks(4764));

            migrationBuilder.CreateTable(
                name: "PopulatedPlaces",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DistrictId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PopulatedPlaces", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PopulatedPlaces_Districts_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "Districts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PopulatedPlaces_DistrictId",
                table: "PopulatedPlaces",
                column: "DistrictId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PopulatedPlaces");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Districts",
                newName: "PopulatedPlace");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 17, 19, 22, 19, 755, DateTimeKind.Local).AddTicks(7175),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 555, DateTimeKind.Local).AddTicks(3582));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Questions",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 17, 19, 22, 19, 729, DateTimeKind.Local).AddTicks(5648),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 546, DateTimeKind.Local).AddTicks(5271));

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 17, 19, 22, 19, 636, DateTimeKind.Local).AddTicks(7552),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 517, DateTimeKind.Local).AddTicks(8645));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DeliveryExpectedTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 20, 19, 22, 19, 646, DateTimeKind.Local).AddTicks(2354),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 29, 16, 1, 6, 519, DateTimeKind.Local).AddTicks(3336));

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostDate",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(2018, 12, 17, 19, 22, 19, 528, DateTimeKind.Local).AddTicks(4764),
                oldClrType: typeof(DateTime),
                oldDefaultValue: new DateTime(2018, 12, 26, 16, 1, 6, 481, DateTimeKind.Local).AddTicks(6408));
        }
    }
}
