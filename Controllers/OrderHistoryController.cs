using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {
			//List<OrderHistory> orders = OrderData.PurchaseHistory(1001);

            List<OrderHistory> ordersByCustomer = OrderData.OrderList(1001);

            foreach (OrderHistory order in ordersByCustomer)
            {
                List<string> codesPerOrderItemId = OrderData.GetListOfCodes(order.OrderItemId);

                foreach (string code in codesPerOrderItemId)
                {
                    order.Activation_Code.Add(code);
                }
				
            }

            ViewData["ordersbycustomer"] = ordersByCustomer;

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
