using System.ComponentModel.DataAnnotations;

namespace StoreManagment.ViewModel
{
    public class LoginVM
    {

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? Name { get; set; }

    }
}
