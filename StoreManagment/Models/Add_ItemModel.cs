using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.Models
{
    [Table("Tbl_AddItem")]
    public class Add_ItemModel
    {
        [Key]
        public int ItemId { get; set; }

        [Column("Item_Name", TypeName = "varchar(100)")]
        public string? Item_Name { get; set; }

        [Column("Item_SerialNumber", TypeName = "varchar(100)")]
        public string? Item_SerialNumber { get; set; }

        [Column("Item_Quantity", TypeName = "int")] // Assuming quantity is numeric
        public int? Item_Quantity { get; set; }

        [Column("Item_Price")]
        public int? Item_Price { get; set; }

        [Column("Item_Selling_Price")]
        public int? Item_Selling_Price { get; set; }

        [Column("Item_Expiry_Date")]
        public DateTime? Item_Expiry_Date { get; set; }

        [Column("Image", TypeName = "nvarchar(255)")]
        public string? Image { get; set; }

        [Column("CreatedDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [Column("UpdatedDate", TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        // Foreign Key for Sub Product
        public int S_P_Id { get; set; }

        [ForeignKey("S_P_Id")]
        public Add_Sub_ProductModel? Add_Sub_ProductModel { get; set; }

        // Foreign Key for Product Category
        public int? P_Id { get; set; }

        [ForeignKey("P_Id")]
        public AddProduct? AddProduct { get; set; }



        [Column("Gst_No", TypeName = "nvarchar(50)")]
        public string? Gst_No { get; set; }
        //Forgin Key for Party
        public int? PartyId { get; set; }

        [ForeignKey("PartyId")]
        public PartiesModel? PartiesModel { get; set; }

    }

}
