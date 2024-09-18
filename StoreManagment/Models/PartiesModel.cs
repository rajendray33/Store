using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.Models
{
    [Table("Tbl_AddParties")]
    public class PartiesModel
    {
        [Key]
        public int PartyId { get; set; }

        [Column("PartyName", TypeName = "varchar(100)")]
        public string? PartyName { get; set; }

        [Column("GstNo", TypeName = "varchar(100)")]
        public string? GstNo { get; set; }

        [Column("PhoneNo", TypeName = "bigint")]
        public long? PhoneNo { get; set; }

        [Column("Email", TypeName = "varchar(100)")]
        public string? Email { get; set; }

        [Column("Address", TypeName = "varchar(255)")]
        public string? Address { get; set; }

        [Column("StoreName", TypeName = "varchar(100)")]
        public string? StoreName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        // Navigation property to reference sub-products
        public ICollection<Add_ItemModel> AddItems { get; set; }

        // Constructor to initialize the Item property
        public PartiesModel()
        {
            AddItems = new List<Add_ItemModel>();
        }
       

    }
}
