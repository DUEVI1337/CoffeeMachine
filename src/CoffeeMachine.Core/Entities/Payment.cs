using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace CoffeeMachine.Core.Entities
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PaymentId { get; set; }

        [Required]
        public int ContributedMoney { get; set; }

        [Required]
        public int CashDepositAmount { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
