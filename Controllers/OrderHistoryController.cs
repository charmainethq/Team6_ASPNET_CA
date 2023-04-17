using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {
			List<OrderHistory> orders = OrderData.PurchaseHistory(1001);

			ViewData["orders"]= orders;
			return View();
        }

        [Route("OrderHistory/OrderItem/ActivationCode")]
        public IActionResult ActivationCode() 
        {
            //test edit 
            return View();
        }
    }
}
