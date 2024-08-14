using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagment.Migrations
{
    public partial class category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tbl_Add_Product",
                columns: table => new
                {
                    P_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Category = table.Column<string>(type: "varchar(150)", nullable: true),
                    CreateDated = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Add_Product", x => x.P_Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Registration",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(100)", nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", nullable: true),
                    Phone_Num = table.Column<long>(type: "bigint", nullable: true),
                    Role = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Registration", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Add_SubProduct",
                columns: table => new
                {
                    Sub_P_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sub_Product_Name = table.Column<string>(type: "varchar(150)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    P_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Add_SubProduct", x => x.Sub_P_Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Add_SubProduct_Tbl_Add_Product_P_Id",
                        column: x => x.P_Id,
                        principalTable: "Tbl_Add_Product",
                        principalColumn: "P_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Add_SubProduct_P_Id",
                table: "Tbl_Add_SubProduct",
                column: "P_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tbl_Add_SubProduct");

            migrationBuilder.DropTable(
                name: "Tbl_Registration");

            migrationBuilder.DropTable(
                name: "Tbl_Add_Product");
        }
    }
}
