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
            //View: have 5 buttons, each represented by a star without fill. On clicking one button, the value (eg. 4 ) should be recorded
            //and the icons for buttons 1-4 should switch to filled 
            return View();
        }
    }
}
