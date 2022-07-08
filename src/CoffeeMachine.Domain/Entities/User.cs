using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Domain.Entities
{
    /// <summary>
    /// client of coffee machine
    /// </summary>
    public class User
    {
        /// <summary>
        /// id of client
        /// </summary>
        [Key]
        public Guid IdUser { get; set; }

        /// <summary>
        /// username of client
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// password of client
        /// </summary>
        [Required]
        public string Password { get; set; }
    }
}
