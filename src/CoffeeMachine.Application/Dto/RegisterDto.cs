using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Application.Dto
{
    /// <summary>
    /// To register new user
    /// </summary>
    public class RegisterDto
    {
        [Required(ErrorMessage = "Not empty")]
        [MinLength(6, ErrorMessage = "Minimum length of password 6 characters")]
        [MaxLength(30, ErrorMessage = "Maximum length password 30 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Not empty")]
        [Compare("Password", ErrorMessage = "Passwords must match ")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "Not empty")]
        public string Username { get; set; }
    }
}