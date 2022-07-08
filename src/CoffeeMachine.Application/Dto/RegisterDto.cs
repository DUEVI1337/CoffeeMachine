using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Not empty")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Not empty")]
        [MinLength(6, ErrorMessage = "Minimum length of password 6 characters")]
        [MaxLength(30, ErrorMessage = "Maximum length password 30 characters")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Not empty")]
        [Compare("Password", ErrorMessage = "Passwords must match ")]
        public string PasswordConfirm { get; set; }
    }
}
