using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagment.ViewModel
{
    public class AddPartiesVM
    {
        public int PartyId { get; set; }

        [Required]
        public string? PartyName { get; set; }
        [Required]
        public string? GstNo { get; set; }
        [Required]
        public long? PhoneNo { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? StoreName { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
