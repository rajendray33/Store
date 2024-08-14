using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.Models
{
    [Table("Tbl_Add_Product")]
    public class AddProduct
    {
        [Key]
        public int P_Id { get; set; }

        [Column("Product_Category", TypeName = "varchar(150)")]
        public string? Product_Category { get; set; }

        [Column("CreateDated", TypeName = "datetime")]
        public DateTime? CreateDate { get; set; }

        [Column("UpdatedDate", TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }

        // Navigation property to reference sub-products
        public ICollection<Add_Sub_ProductModel> SubProducts { get; set; }
        public ICollection<Add_ItemModel> AddItems { get; set; }

        // Constructor to initialize the SubProducts and item property
        public AddProduct()
        {
            SubProducts = new List<Add_Sub_ProductModel>();
            AddItems = new List<Add_ItemModel>();
        }
    }




}
