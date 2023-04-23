using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class OrderHistoryController : Controller
    {
        public IActionResult Index()
        {

            int? custId = HttpContext.Session.GetInt32("customerId");

            //retrieve all orders by customer
            List<OrderHistory> ordersByCustomer = OrderData.OrderList(custId);    
            
            foreach (OrderHistory order in ordersByCustomer)
            {

				//Each order has multiple OrderItemIDs. For each OrderItemID, retrieve a list of activation codes. 
				List<string> codesPerOrderItemId = OrderData.GetListOfCodes(order.OrderItemId);

                //Add the codes to the Activation_Code attribute
                foreach (string code in codesPerOrderItemId)
                {
                    order.Activation_Code.Add(code);
                }
				
            }

            ViewData["ordersbycustomer"] = ordersByCustomer;
            

            return View();
        }
        



    }
}
