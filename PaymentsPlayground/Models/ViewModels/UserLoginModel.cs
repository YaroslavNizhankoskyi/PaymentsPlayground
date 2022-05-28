using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace PaymentsPlayground.Models.ViewModels
{
    public class UserLoginModel
    {
        [Email]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }           
    }
}
