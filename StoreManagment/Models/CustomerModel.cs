using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.Models
{
    [Table("Tbl_Customer")]
    public class CustomerModel
    {
        [Key]
        public int Customer_Id { get; set; }

        [Column("Cust_Name", TypeName = "varchar(60)")]
        public string? Cust_Name { get; set; }

        public long? Cust_Mobile_No { get; set; }

        [Column("Cust_Email", TypeName = "varchar(60)")]
        public string? Cust_Email { get; set; }

        [Column("Cust_Gstin_No", TypeName = "varchar(20)")]
        public string? Cust_Gstin_No { get; set; }

        [Column("Cust_City", TypeName = "varchar(50)")]
        public string? Cust_City { get; set; }

        [Column("Created_Date", TypeName = "datetime")]
        public DateTime? Created_Date { get; set; }

        [Column("Updated_Date", TypeName = "datetime")]
        public DateTime? Updated_Date { get; set; }

    }
}
