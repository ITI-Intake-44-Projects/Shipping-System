using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShippingSystem.Migrations
{
    /// <inheritdoc />
    public partial class yousefv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "Governate",
                table: "Merchants");

            migrationBuilder.AddColumn<int>(
                name: "City_Id",
                table: "Merchants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Governate_Id",
                table: "Merchants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "AspNetRoles",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SpecialPrices_City_Id",
                table: "SpecialPrices",
                column: "City_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_City_Id",
                table: "Merchants",
                column: "City_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Merchants_Governate_Id",
                table: "Merchants",
                column: "Governate_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchants_Cities_City_Id",
                table: "Merchants",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Merchants_Governates_Governate_Id",
                table: "Merchants",
                column: "Governate_Id",
                principalTable: "Governates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialPrices_Cities_City_Id",
                table: "SpecialPrices",
                column: "City_Id",
                principalTable: "Cities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Merchants_Cities_City_Id",
                table: "Merchants");

            migrationBuilder.DropForeignKey(
                name: "FK_Merchants_Governates_Governate_Id",
                table: "Merchants");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecialPrices_Cities_City_Id",
                table: "SpecialPrices");

            migrationBuilder.DropIndex(
                name: "IX_SpecialPrices_City_Id",
                table: "SpecialPrices");

            migrationBuilder.DropIndex(
                name: "IX_Merchants_City_Id",
                table: "Merchants");

            migrationBuilder.DropIndex(
                name: "IX_Merchants_Governate_Id",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "City_Id",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "Governate_Id",
                table: "Merchants");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Governate",
                table: "Merchants",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
