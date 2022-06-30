
using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Serilog;


namespace CoffeeMachine.Web.Controllers
{
    /// <summary>
    /// work with entity <see cref="Coffee"/> in database.
    /// </summary>
    [Route("coffee/v1")]
    [ApiController]
    public class CoffeeController : Controller
    {
        private readonly ICoffeeService _coffeeService;

        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        /// <summary>
        /// checking possibility of buying coffee
        /// </summary>
        /// <param name="order">order person</param>
        /// <response code="200">return deal</response>
        /// <response code="400">it is impossible to give change</response>
        /// <response code="404">not found coffee</response>
        [HttpPost]
        [Route("BuyCoffee")]
        public async Task<ActionResult<List<BanknoteDto>>> BuyCoffee([FromBody] OrderDto order)
        {
            var coffee = await _coffeeService.GetCoffeeDtoByIdAsync(order.CoffeeId);
            if (coffee == null)
            {
                Log.Information($"Object 'Coffee' by id: {order.CoffeeId}, not found");
                return NotFound(order.CoffeeId);
            }

            var deal = await _coffeeService.BuyCoffeeAsync(coffee, order.Banknotes, (TypeDeal)order.TypeDeal);
            if (deal == null)
            {
                Log.Information("impossible to give deal");
                return BadRequest(order.Banknotes);
            }
            return Ok(deal);
        }

        /// <summary>
        /// get coffee by id from database
        /// </summary>
        /// <param name="id">id coffee</param>
        /// <returns><see cref="CoffeeDto"/> or null</returns>
        /// <response code="200">return coffee from database</response>
        /// <response code="404">not found coffee</response>
        [HttpGet]
        [Route("Coffee/{id}")]
        public async Task<ActionResult<CoffeeDto>> GetCoffeeDtoById(string id)
        {
            var coffee = await _coffeeService.GetCoffeeDtoByIdAsync(id);
            if (coffee == null)
            {
                Log.Information($"Object 'Coffee' by id: {id}, not found");
                return NotFound(id);
            }

            return Ok(coffee);
        }

        /// <summary>
        /// get list coffee from database
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="Coffee"/></returns>
        /// <response code="200">return list of coffee from database</response>
        [HttpGet]
        [Route("MenuCoffee")]
        public async Task<ActionResult<List<CoffeeDto>>> GetListCoffeeDto()
        {
            return Ok(await _coffeeService.GetListCoffeeDtoAsync());
        }
    }
}