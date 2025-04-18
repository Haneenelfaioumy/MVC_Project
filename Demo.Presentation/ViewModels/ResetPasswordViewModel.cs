using System.ComponentModel.DataAnnotations;

namespace Demo.Presentation.ViewModels
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password Is Required")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Confirm Password Is Required")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
