using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreManagment.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.ViewModel
{
    public class Add_ItemVM
    {
        public int ItemId { get; set; }
        [Required]
        public string? Item_Name { get; set; }
        [Required]
        public string? Item_SerialNumber { get; set; }
        [Required]
        public int? Item_Quantity { get; set; }
        [Required]
        public int? Item_Price { get; set; }
        [Required]
        public int? Item_Selling_Price { get; set; }
        [Required]
        public DateTime? Item_Expiry_Date { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }



        // Foreign Key for Sub Product
        public int S_P_Id { get; set; }
        public List<SelectListItem>? SubProductList { get; set; }
        public Add_Sub_ProductModel? Add_Sub_ProductModel { get; set; }





        // Foreign Key for Product Category
        public int? P_Id { get; set; }
        public List<SelectListItem>? categoryList { get; set; }
        public string? ProductCategory { get; set; }
        public AddProduct? AddProduct { get; set; }
    }
}
