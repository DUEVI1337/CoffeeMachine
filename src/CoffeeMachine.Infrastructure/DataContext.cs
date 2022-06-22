using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoffeeMachine.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachine.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<Coffee> Coffees { get; set; }
    }
}
