using System;
using System.ComponentModel.DataAnnotations;

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
        /// password of client
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// username of client
        /// </summary>
        [Required]
        public string Username { get; set; }
    }
}