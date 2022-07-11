using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Application.Dto;
using CoffeeMachine.Application.Services.Interfaces;
using CoffeeMachine.Domain.Entities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize()]
        public async Task<List<BanknoteDto>> BuyCoffee([FromBody] OrderDto order)
        {
            return await _coffeeService.BuyCoffeeAsync(order.CoffeeId, order.Banknotes, (TypeDeal)order.TypeDeal);
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
        [Route("CoffeeMenu")]
        public async Task<List<CoffeeDto>> GetListCoffeeDto()
        {
            return await _coffeeService.GetListCoffeeDtoAsync();
        }
    }
}