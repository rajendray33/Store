using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static StoreManagment.Models.Common;

namespace StoreManagment.Models
{
    [Table("Tbl_Registration")]
    public class RegistrationModel
    {

        [Key]
        public int UserId { get; set; }


        [Column("Name", TypeName = "varchar(100)")]
        //[Required]
        public string? Name { get; set; }

        [Column("Email", TypeName = "varchar(100)")]
        //[Required]
        public string? Email { get; set; }

        [Column("Password", TypeName = "varchar(50)")]
        //[Required]
        public string? Password { get; set; }


        [Column("Phone_Num", TypeName = "bigint")]
        //[Required]
        public long? Phone_Num { get; set; }


        [Column("Role", TypeName = "varchar(50)")]
        //[Required]
        public UserRole Role { get; set; }

        //[DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
  
}
