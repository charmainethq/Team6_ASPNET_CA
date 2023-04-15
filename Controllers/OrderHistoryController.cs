using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {
            List<Tuple<Order, OrderItem>> orders = OrderHistoryData.PurchaseHistory();
            ViewData["orders"]= orders;

			return View();
        }
    }
}
