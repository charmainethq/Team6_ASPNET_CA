using Microsoft.AspNetCore.Mvc;

namespace Team6.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("OrderHistory/OrderItem/ActivationCode")]
        public IActionResult ActivationCode() 
        {
            return View();
        }
    }
}
