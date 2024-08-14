using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagment.Migrations
{
    public partial class item : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_AddItem",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Item_Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Item_SerialNumber = table.Column<string>(type: "varchar(100)", nullable: true),
                    Item_Quantity = table.Column<int>(type: "int", nullable: true),
                    Item_Price = table.Column<int>(type: "int", nullable: true),
                    Item_Selling_Price = table.Column<int>(type: "int", nullable: true),
                    Item_Expiry_Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    S_P_Id = table.Column<int>(type: "int", nullable: false),
                    P_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_AddItem", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Tbl_AddItem_Tbl_Add_Product_P_Id",
                        column: x => x.P_Id,
                        principalTable: "Tbl_Add_Product",
                        principalColumn: "P_Id");
                    table.ForeignKey(
                        name: "FK_Tbl_AddItem_Tbl_Add_SubProduct_S_P_Id",
                        column: x => x.S_P_Id,
                        principalTable: "Tbl_Add_SubProduct",
                        principalColumn: "Sub_P_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_AddItem_P_Id",
                table: "Tbl_AddItem",
                column: "P_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_AddItem_S_P_Id",
                table: "Tbl_AddItem",
                column: "S_P_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_AddItem");
        }
    }
}
