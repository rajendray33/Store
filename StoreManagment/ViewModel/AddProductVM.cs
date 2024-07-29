using System.ComponentModel.DataAnnotations;

namespace StoreManagment.ViewModel
{
    public class AddProductVM
    {
        public int P_Id { get; set; }
        [Required]
        public string? Product_Category { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<AddProductVM>? ProductList { get; set; }
    }
}
