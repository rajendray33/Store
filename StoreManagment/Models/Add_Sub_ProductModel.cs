
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.Models
{
    [Table("Tbl_Add_SubProduct")]
    public class Add_Sub_ProductModel
    {
        [Key]
        public int Sub_P_Id { get; set; }

        [Column("Sub_Product_Name", TypeName = "varchar(150)")]
        public string? Sub_Product_Name { get; set; }

        [Column("CreatedDate", TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [Column("UpdatedDate", TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        // Foreign key to AddProduct
        public int P_Id { get; set; }

        [ForeignKey("P_Id")]
        public AddProduct? AddProduct { get; set; }

        // Navigation property to reference sub-products
        public ICollection<Add_ItemModel> AddItems { get; set; }

        // Constructor to initialize the Item property
        public Add_Sub_ProductModel()
        {
            AddItems = new List<Add_ItemModel>();
        }
    }



}
