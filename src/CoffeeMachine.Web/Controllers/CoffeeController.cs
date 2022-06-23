using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Core.Entities;
using CoffeeMachine.Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Web.Controllers
{
    /// <summary>
    /// create order with coffee
    /// </summary>
    [Route("coffee/v1")]
    [ApiController]
    public class CoffeeController : Controller
    {
        private readonly UnitOfWork _uow;

        public CoffeeController(UnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// get list coffee from database
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="Coffee"/></returns>
        [HttpGet]
        [Route("ListCoffee")]
        public async Task<ActionResult> GetListCoffee()
        {
            return Ok(await _uow.CoffeeRepo.GetAllAsync());
        }
    }
}