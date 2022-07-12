using System.ComponentModel.DataAnnotations;

namespace CoffeeMachine.Application.Dto
{
    /// <summary>
    /// For login user
    /// </summary>
    public class SignInDto
    {
        [Required(ErrorMessage = "Not empty")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Not empty")]
        public string Username { get; set; }
    }
}