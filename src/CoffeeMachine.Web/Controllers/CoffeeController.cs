using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachine.Web.Controllers
{
    public class CoffeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
