using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            //return view of the individual product based on product Id or name
            return View();
        }


        


        public IActionResult CreateReview(string? submitReviewButton, int ratingStars, string? reviewDescription, int OrderItemId, string customerName) 
        {
            //sample test to display customerName
            customerName = "Johnny Walked";
            ViewData["customerName"] = customerName;
            //when "submit" button page is clicked on the create review page
            if (submitReviewButton == null)
            {
                return View();
            }
            else
            {
                //test sample update
                OrderItemId = 20081;
                ProductData.submitReview(ratingStars, reviewDescription, OrderItemId);

                // Redirect back to Order History after review has been submitted
                // return RedirectToAction("Index", "OrderHistory");

                // test redirect to 
                return RedirectToAction("ProductReview", "Home");
            }
        }
    }
}
