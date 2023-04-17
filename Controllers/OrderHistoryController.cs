using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {
			//List<Dictionary<Order, OrderItem>> orders = OrderData.PurchaseHistory();

			List<Order> Orders = new List<Order> {
				new Order { CustomerID = 1, OrderID = 1, OrderDate = new DateTime(2022, 9, 23, 10, 00, 00) }
			};

			List<OrderItem> OrderItems = new List<OrderItem>
			{
			new OrderItem { OrderID = 1, ProductID = 1, Quantity = 2, Price = 1.5},
			new OrderItem { OrderID = 1, ProductID = 2, Quantity = 3, Price = 69.00},
			new OrderItem { OrderID = 1, ProductID = 3, Quantity = 4, Price = 299.00},

			};

			ViewData["orders"]= OrderItems;
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
