using Microsoft.AspNetCore.Mvc;

namespace Team6.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            //return view of the individual product based on product Id or name
            return View();
        }





        public IActionResult CreateReview()
        {
            //take in rating and message(optional) to create a Review object. Display should be at the bottom of the product page
            return View();
        }
    }
}
