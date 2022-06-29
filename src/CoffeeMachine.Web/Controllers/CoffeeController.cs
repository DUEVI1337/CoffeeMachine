using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Application.Service;
using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.DTO;
using CoffeeMachine.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

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
        private readonly IBanknoteCashboxService _banknoteCashboxService;

        public CoffeeController(ICoffeeService coffeeService, IBanknoteCashboxService banknoteCashboxService)
        {
            _coffeeService = coffeeService;
            _banknoteCashboxService = banknoteCashboxService;
        }

        /// <summary>
        /// checking possibility of buying coffee
        /// </summary>
        /// <param name="order">order person</param>
        [HttpPost]
        [Route("BuyCoffee")]
        public async Task<List<BanknoteDto>> BuyCoffee([FromBody] OrderDto order)
        {
            var coffee = await _coffeeService.GetCoffeeDtoByIdAsync(order.CoffeeId);
            if (coffee == null)
                return null;

            List<BanknoteDto> deal = await _coffeeService.BuyCoffee(coffee, order.Banknotes,(TypeDeal)order.TypeDeal);
            return deal ?? null;
        }

        /// <summary>
        /// get coffee by id from database
        /// </summary>
        /// <param name="id">id coffee</param>
        /// <returns><see cref="CoffeeDto"/> or null</returns>
        /// <response code="200">return coffee from database</response>
        [HttpGet]
        [Route("Coffee/{id}")]
        public async Task<CoffeeDto> GetCoffeeDtoById(string id)
        {
            return await _coffeeService.GetCoffeeDtoByIdAsync(id);
        }

        /// <summary>
        /// get list coffee from database
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="Coffee"/></returns>
        /// <response code="200">return list of coffee from database</response>
        [HttpGet]
        [Route("MenuCoffee")]
        public async Task<List<CoffeeDto>> GetListCoffeeDto()
        {
            return await _coffeeService.GetListCoffeeDtoAsync();
        }
    }
}