using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagment.Migrations
{
    public partial class AddCreateDateAndUpdateDateToAddProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Tbl_Add_Product",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "Tbl_Add_Product",
                type: "datetime",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Tbl_Add_Product");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "Tbl_Add_Product");
        }
    }
}
