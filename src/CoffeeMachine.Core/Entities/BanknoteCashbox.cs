using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.Core.Entities
{
    public class BanknoteCashbox
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BanknoteId { get; set; }
        [Required]
        public int Denomination { get; set; }
        [Required]
        public int CountBanknote { get; set; }
    }
}
