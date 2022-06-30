using System.Collections.Generic;
using System.Threading.Tasks;

using CoffeeMachine.Application.Service.Interfaces;
using CoffeeMachine.Domain.Dto;
using CoffeeMachine.Domain.Entities;

using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Web.Controllers
{
    /// <summary>
    /// work with entity <see cref="Balance"/> in database
    /// </summary>
    public class BalanceController : Controller
    {
        private readonly IBalanceService _balanceService;

        public BalanceController(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        /// <summary>
        /// Get earnings
        /// </summary>
        /// <returns><see cref="List{T}"/> where T <see cref="BalanceDto"/></returns>
        /// <response code="200">return balance from database</response>
        [HttpGet]
        [Route("Balance")]
        public async Task<ActionResult<List<BalanceDto>>> GetListBalanceDto()
        {
            return Ok(await _balanceService.GetBalancesDtoAsync());
        }
    }
}