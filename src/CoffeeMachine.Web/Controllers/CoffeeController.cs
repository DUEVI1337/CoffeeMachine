using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeMachine.Application.Mappers;
using CoffeeMachine.Domain.DTO;
using CoffeeMachine.Domain.Entities;
using CoffeeMachine.Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
        /// <response code="200">return list of coffee from database</response>
        [HttpGet]
        [Route("ListCoffee")]
        public async Task<List<CoffeeDto>> GetListCoffee() =>
            Mapper.CoffeeListMapper(await _uow.CoffeeRepo.GetAllAsync());
    }
}