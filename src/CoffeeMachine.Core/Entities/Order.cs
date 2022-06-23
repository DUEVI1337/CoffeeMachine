using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Core.Entities
{
    [Index (nameof(PaymentId), IsUnique = true)]
    public class Order
    {
        [ForeignKey("Coffee")]
        public int CoffeeId { get; set; }
        public Coffee Coffee { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}
