using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.DTO;
using CoffeeMachine.Domain.Entities;

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
        private readonly ICoffeeService _coffeeService;

        public CoffeeController(ICoffeeService coffeeService)
        {
            _coffeeService = coffeeService;
        }

        /// <summary>
        /// get coffee by id from database
        /// </summary>
        /// <param name="id"></param>
        /// <returns><see cref="CoffeeDto"/> or null</returns>
        /// <response code="200">return coffee from database</response>
        [HttpGet]
        [Route("Coffee/{id}")]
        public async Task<CoffeeDto> GetCoffeeDtoById(string id)
        {
            if (!Guid.TryParse(id, out var idGuid))
                return null;
            return await _coffeeService.GetCoffeeDtoByIdAsync(idGuid);
        }

        /// <summary>
        /// get list coffee from database
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="Coffee"/></returns>
        /// <response code="200">return list of coffee from database</response>
        [HttpGet]
        [Route("ListCoffee")]
        public async Task<List<CoffeeDto>> GetListCoffeeDto()
        {
            return await _coffeeService.GetListCoffeeDtoAsync();
        }
    }
}