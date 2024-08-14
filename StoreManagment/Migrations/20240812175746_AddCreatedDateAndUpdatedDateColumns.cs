using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagment.Migrations
{
    public partial class AddCreatedDateAndUpdatedDateColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Tbl_AddItem",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Tbl_AddItem",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Tbl_AddItem");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Tbl_AddItem");
        }
    }
}
