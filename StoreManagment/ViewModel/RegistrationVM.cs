using System.ComponentModel.DataAnnotations;
using static StoreManagment.Models.Common;
namespace StoreManagment.ViewModel
{
    public class RegistrationVM
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string? Password { get; set; }

        [Required]
        [RegularExpression(@"^[789]\d{9}$")]
        public long? Phone_Num { get; set; }

        [Required]
        //[Range(1, int.MaxValue)]
        public int Role { get; set; }


        ////[DataType(DataType.Date)]
        //public DateTime CreateDate { get; set; }
        //public DateTime UpdateDate { get; set; }

    }
}