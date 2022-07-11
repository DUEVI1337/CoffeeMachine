using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Dto
{
    /// <summary>
    /// For login user
    /// </summary>
    public class SignInDto
    {

        [Required(ErrorMessage = "Not empty")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Not empty")]
        public string Password { get; set; }
    }
}
