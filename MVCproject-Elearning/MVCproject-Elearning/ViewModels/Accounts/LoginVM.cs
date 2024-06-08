using System.ComponentModel.DataAnnotations;

namespace MVCproject_Elearning.ViewModels.Accounts
{
    public class LoginVM
    {
        [Required]
        public string EmailOrUsername { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
