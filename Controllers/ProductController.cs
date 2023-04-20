using Microsoft.AspNetCore.Mvc;
using Team6.Data;
using Team6.Models;

namespace Team6.Controllers
{
    public class ProductController : Controller
    {



        public IActionResult Product(int Id)

        {
            Product product = ProductData.GetProductById(Id);
            int averageRating = ProductData.AverageRating(Id);
            int ratingCounts = ProductData.CountRating(Id);
            List<ProductReview> reviewDetails = ProductData.ReviewDetails(Id);
            ViewBag.product = product;
            ViewData["avgRtg"] = averageRating;
            ViewData["rtgCnts"] = ratingCounts;
            ViewData["rvDts"] = reviewDetails;
            return View();
        }
        
         

        public IActionResult CreateReview(string? submitReviewButton, int ratingStars, string? reviewDescription, int OrderItemId, string productName) 
        {
            
            //when "submit" button page is clicked on the create review page
            if (submitReviewButton == null)
            {
                ViewData["orderItemId"] = OrderItemId;
                ViewData["productName"] = productName;
                return View();
            }
            else
            {
                //set message if user did not write a review
                reviewDescription = reviewDescription == null ? "User chose not to leave a review." : reviewDescription;

                ProductData.submitReview(ratingStars, reviewDescription, OrderItemId);

                // Redirect back to Order History after review has been submitted
                return RedirectToAction("Index", "OrderHistory");

            }
        }

    }
}
