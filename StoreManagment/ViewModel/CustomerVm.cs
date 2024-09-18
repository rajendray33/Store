using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.ViewModel
{
    public class CustomerVm
    {
        public int Customer_Id { get; set; }

        [Required]
        public string? Cust_Name { get; set; }
        [Required]
        public long? Cust_Mobile_No { get; set; }
        [Required]
        public string? Cust_Email { get; set; }

        public string? Cust_Gstin_No { get; set; }
        [Required]
        public string? Cust_City { get; set; }

        public DateTime? Created_Date { get; set; }

        public DateTime? Updated_Date { get; set; }
    }
}
