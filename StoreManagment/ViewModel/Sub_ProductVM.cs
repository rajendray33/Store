using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreManagment.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.ViewModel
{
    public class Sub_ProductVM
    {
        public int Sub_P_Id { get; set; }
        [Required]
        public string? Sub_Product_Name { get; set; }
        public string? ProductCategory {get; set; }
        public List<SelectListItem>? categoryList { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int P_Id { get; set; }

    }
}
