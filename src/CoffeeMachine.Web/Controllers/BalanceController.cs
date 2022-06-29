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
        /// <returns></returns>
        [HttpGet]
        [Route("Balance")]
        public async Task<List<BalanceDto>> GetListBalanceDto()
        {
            return await _balanceService.GetBalancesDtoAsync();
        }
    }
}