using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoreManagment.Migrations
{
    public partial class AddImageColumnToAddItemModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Tbl_AddItem",
                type: "nvarchar(255)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Tbl_AddItem");
        }
    }
}
