using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Application.Dto
{
    /// <summary>
    /// 
    /// </summary>
    public class SignInDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Not empty")]
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Not empty")]
        public string Password { get; set; }
    }
}
