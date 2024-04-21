using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MobyLabWebProgramming.Infrastructure.Migrations
{
    public partial class initial7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturi_ShopCart_UserId",
                table: "Facturi");

            migrationBuilder.DropIndex(
                name: "IX_Facturi_UserId",
                table: "Facturi");

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippingTime",
                table: "Facturi",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Facturi",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Facturi_CartId",
                table: "Facturi",
                column: "CartId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Facturi_UserId",
                table: "Facturi",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facturi_ShopCart_CartId",
                table: "Facturi",
                column: "CartId",
                principalTable: "ShopCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facturi_ShopCart_CartId",
                table: "Facturi");

            migrationBuilder.DropIndex(
                name: "IX_Facturi_CartId",
                table: "Facturi");

            migrationBuilder.DropIndex(
                name: "IX_Facturi_UserId",
                table: "Facturi");

            migrationBuilder.DropColumn(
                name: "ShippingTime",
                table: "Facturi");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Facturi");

            migrationBuilder.CreateIndex(
                name: "IX_Facturi_UserId",
                table: "Facturi",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Facturi_ShopCart_UserId",
                table: "Facturi",
                column: "UserId",
                principalTable: "ShopCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
